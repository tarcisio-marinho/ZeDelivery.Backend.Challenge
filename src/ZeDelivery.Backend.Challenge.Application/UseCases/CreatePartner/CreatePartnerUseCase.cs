using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeDelivery.Backend.Challenge.Application.Shared;

namespace ZeDelivery.Backend.Challenge.Application.UseCases.CreatePartner
{
    public class CreatePartnerUseCase : IUseCase<CreatePartnerInput>
    {
        private readonly ICreatePartnerOutputPort outputPort;
        private readonly IValidator<CreatePartnerInput> validator;
        public CreatePartnerUseCase(ICreatePartnerOutputPort outputPort, IValidator<CreatePartnerInput> validator)
        {
            this.outputPort = outputPort;
            this.validator = validator;
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

            // Tentar inserir novo partner


            outputPort.PublishPartnerCreated();
        }
    }
}
