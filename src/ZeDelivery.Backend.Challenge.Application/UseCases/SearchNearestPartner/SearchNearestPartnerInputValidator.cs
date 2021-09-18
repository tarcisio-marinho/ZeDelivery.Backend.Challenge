using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeDelivery.Backend.Challenge.Application.UseCases.SearchNearestPartner.Input;

namespace ZeDelivery.Backend.Challenge.Application.UseCases.SearchNearestPartner
{
    public class SearchNearestPartnerInputValidator : AbstractValidator<SearchNearestPartnerInput>
    {
        public SearchNearestPartnerInputValidator()
        {
            RuleFor(input => input)
                .NotNull()
                .NotEmpty()
                .WithMessage($"{nameof(SearchNearestPartnerInput)} cannot be null");

            RuleFor(input => input.Latitude)
                .NotNull()
                .NotEmpty()
                .WithMessage($"{nameof(SearchNearestPartnerInput.Latitude)} cannot be null");

            RuleFor(input => input.Longitude)
                .NotNull()
                .NotEmpty()
                .WithMessage($"{nameof(SearchNearestPartnerInput.Longitude)} cannot be null");
        }
    }
}
