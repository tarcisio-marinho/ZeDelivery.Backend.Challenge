using FluentValidation;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeDelivery.Backend.Challenge.Application.Services.Caching;
using ZeDelivery.Backend.Challenge.Application.Shared;
using ZeDelivery.Backend.Challenge.Domain.Entities;
using ZeDelivery.Backend.Challenge.Domain.Queries;
using ZeDelivery.Backend.Challenge.Domain.Services;

namespace ZeDelivery.Backend.Challenge.Application.UseCases.CreatePartner
{
    public class CreatePartnerUseCase : IUseCase<CreatePartnerInput>
    {
        private readonly ICreatePartnerOutputPort outputPort;
        private readonly IValidator<CreatePartnerInput> validator;
        private readonly ILogger<CreatePartnerUseCase> logger;
        private readonly IInsertNewPartnerQuery insertNewPartnerquery;
        private readonly ICheckIfPartnerExistsQuery checkIfPartnerExistsquery;
        private readonly ICacheService cacheService;
        public CreatePartnerUseCase(
            ICreatePartnerOutputPort outputPort,
            IValidator<CreatePartnerInput> validator,
            ILogger<CreatePartnerUseCase> logger,
            IInsertNewPartnerQuery insertNewPartnerquery,
            ICheckIfPartnerExistsQuery checkIfPartnerExistsquery, 
            ICacheService cacheService)
        {
            this.outputPort = outputPort;
            this.validator = validator;
            this.logger = logger;
            this.insertNewPartnerquery = insertNewPartnerquery;
            this.checkIfPartnerExistsquery = checkIfPartnerExistsquery;
            this.cacheService = cacheService;
        }

        public async Task ExecuteAsync(CreatePartnerInput input)
        {
            var validated = await validator.ValidateAsync(input);

            if (!validated.IsValid)
            {
                var notification = new Notification(validated.Errors);
                outputPort.PublishValidationErros(notification);
                return;
            }

            var partnerExists = await checkIfPartnerExistsquery.ExecuteAsync(input.Id); // TODO: check for Document also
            if (partnerExists)
            {
                logger.LogDebug($"Partner {input.Id} already registered !");
                outputPort.PublishDuplicatedPartner();
                return;
            }

            var AddressPoint = GeoJsonParser.ParsePoint(input.Address.Coordinates);
            var CoverageAreaPolygon = GeoJsonParser.ParsePolygon(input.CoverageArea.Coordinates);
           
            logger.LogInformation(CoverageAreaPolygon.ToGeometry());
            logger.LogInformation(AddressPoint.ToGeometry());
           
            var partner = new Partner(
                input.Id,
                input.TradingName, 
                input.OwnerName, 
                input.Document,
                new CoverageArea(input.CoverageArea.Type, CoverageAreaPolygon),
                new Address(AddressPoint, input.Address.Type)
            );

            var insertionSuccess = await insertNewPartnerquery.ExecuteAsync(partner);
            if (insertionSuccess)
            {
                _ = cacheService.TrySetAsync(input.Id, partner.ToDto());

                outputPort.PublishPartnerCreated();
                return;
            }

            outputPort.PublishInternalServerError();
        }
    }
}
