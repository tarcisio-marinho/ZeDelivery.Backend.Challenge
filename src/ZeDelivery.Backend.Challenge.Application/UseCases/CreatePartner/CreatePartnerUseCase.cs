using FluentValidation;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        private readonly IInsertNewPartnerQuery query;
        public CreatePartnerUseCase(ICreatePartnerOutputPort outputPort, IValidator<CreatePartnerInput> validator, ILogger<CreatePartnerUseCase> logger, IInsertNewPartnerQuery query)
        {
            this.outputPort = outputPort;
            this.validator = validator;
            this.logger = logger;
            this.query = query;
        }

        public async Task ExecuteAsync(CreatePartnerInput input)
        {
            var validated = await validator.ValidateAsync(input);

            if (!validated.IsValid)
            {
                var notification = new Notification(validated.Errors);
                // TODO: publish validation errors
               outputPort.PublishValidationErros(notification);
                return;
            }

            var AddressPoint = GeoJsonParser.ParsePoint(input.Address.Coordinates);
            var CoverageAreaPolygon = GeoJsonParser.ParsePolygon(input.CoverageArea.Coordinates);

            var partner = new Partner(
                input.Id,
                input.TradingName, 
                input.OwnerName, 
                input.Document,
                new CoverageArea(input.CoverageArea.Type, CoverageAreaPolygon),
                new Address(AddressPoint, input.Address.Type)
            );

            var ret = await query.ExecuteAsync(partner);


            outputPort.PublishPartnerCreated();
        }
    }
}
