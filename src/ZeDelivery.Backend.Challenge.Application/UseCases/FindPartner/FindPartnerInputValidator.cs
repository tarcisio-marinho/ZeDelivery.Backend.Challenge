using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeDelivery.Backend.Challenge.Application.UseCases.FindPartner.Input;

namespace ZeDelivery.Backend.Challenge.Application.UseCases.FindPartner
{
    public class FindPartnerInputValidator : AbstractValidator<FindPartnerInput>
    {
        public FindPartnerInputValidator()
        {
            RuleFor(input => input)
                .NotNull()
                .NotEmpty()
                .WithMessage($"{nameof(FindPartnerInput)} cannot be null");

            RuleFor(input => input.Id)
                .NotNull()
                .NotEmpty()
                .WithMessage($"{nameof(FindPartnerInput.Id)} cannot be null");
        }
    }
}