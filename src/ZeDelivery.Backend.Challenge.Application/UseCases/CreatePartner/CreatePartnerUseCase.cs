using FluentValidation;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeDelivery.Backend.Challenge.Application.Shared;
using ZeDelivery.Backend.Challenge.Domain.Services;

namespace ZeDelivery.Backend.Challenge.Application.UseCases.CreatePartner
{
    public class CreatePartnerUseCase : IUseCase<CreatePartnerInput>
    {
        private readonly ICreatePartnerOutputPort outputPort;
        private readonly IValidator<CreatePartnerInput> validator;
        private readonly ILogger<CreatePartnerUseCase> logger;
        public CreatePartnerUseCase(ICreatePartnerOutputPort outputPort, IValidator<CreatePartnerInput> validator, ILogger<CreatePartnerUseCase> logger)
        {
            this.outputPort = outputPort;
            this.validator = validator;
            this.logger = logger;
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

            logger.LogInformation(AddressPoint.ToGeometry());
            logger.LogInformation(CoverageAreaPolygon.ToGeometry());


            outputPort.PublishPartnerCreated();
        }
    }
}
