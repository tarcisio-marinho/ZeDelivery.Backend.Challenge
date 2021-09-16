using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                // TODO: publish validation errors
               outputPort.PublishValidationErros();
            }

            outputPort.PublishPartnerCreated();
        }
    }
}
