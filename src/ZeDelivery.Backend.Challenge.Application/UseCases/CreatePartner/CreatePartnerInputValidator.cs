using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeDelivery.Backend.Challenge.Application.UseCases.CreatePartner
{
    public class CreatePartnerInputValidator : AbstractValidator<CreatePartnerInput>
    {
        //TODO: add validator logic
        public CreatePartnerInputValidator()
        {
            RuleFor(input => input)
                .NotNull()
                .NotEmpty()
                .WithMessage($"{nameof(CreatePartnerInput)} cannot be null");

            RuleFor(input => input.Id)
                .NotNull()
                .NotEmpty()
                .WithMessage($"{nameof(CreatePartnerInput.Id)} cannot be null");

            RuleFor(input => input.OwnerName)
                .NotNull()
                .NotEmpty()
                .WithMessage($"{nameof(CreatePartnerInput.OwnerName)} cannot be null")
                .MaximumLength(255)
                .WithMessage($"{nameof(CreatePartnerInput.OwnerName)} wrong length");


            RuleFor(input => input.Document)
                .NotNull()
                .NotEmpty()
                .WithMessage($"{nameof(CreatePartnerInput.OwnerName)} cannot be null")
                .MaximumLength(18)
                .WithMessage($"{nameof(CreatePartnerInput.Document)} must have maximum length of 18 characters");


            RuleFor(input => input.TradingName)
                .NotNull()
                .NotEmpty()
                .WithMessage($"{nameof(CreatePartnerInput.TradingName)} cannot be null")
                .MaximumLength(255)
                .WithMessage($"{nameof(CreatePartnerInput.TradingName)} wrong length");

            RuleFor(input => input.Address.Type)
                .NotNull()
                .NotEmpty()
                .WithMessage($"{nameof(CreatePartnerInput.Address.Type)} cannot be null")
                .MaximumLength(255)
                .WithMessage($"{nameof(CreatePartnerInput.Address.Type)} wrong length");

            RuleFor(input => input.Address.Coordinates)
               .NotNull()
               .NotEmpty()
               .WithMessage($"{nameof(CreatePartnerInput.Address.Coordinates)} cannot be null");

            RuleForEach(input => input.Address.Coordinates)
               .NotNull()
               .NotEmpty()
               .WithMessage($"{nameof(CreatePartnerInput.Address.Coordinates)} cannot be null");

            RuleFor(input => input.CoverageArea.Type)
               .NotNull()
               .NotEmpty()
               .WithMessage($"{nameof(CreatePartnerInput.CoverageArea.Type)} cannot be null")
               .MaximumLength(255)
                .WithMessage($"{nameof(CreatePartnerInput.CoverageArea.Type)} wrong length");

            RuleFor(input => input.CoverageArea.Coordinates)
               .NotNull()
               .NotEmpty()
               .WithMessage($"{nameof(CreatePartnerInput.CoverageArea.Coordinates)} cannot be null");
        }
    }
}
