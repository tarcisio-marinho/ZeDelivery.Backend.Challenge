using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeDelivery.Backend.Challenge.Application.UseCases.CreatePartner;
using ZeDelivery.Backend.Challenge.Application.UseCases.FindPartner;
using ZeDelivery.Backend.Challenge.Application.UseCases.FindPartner.Input;

namespace ZeDelivery.Backend.Challenge.Application
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddApplicationValidators(this IServiceCollection services)
        {
            services.AddScoped<IValidator<CreatePartnerInput>, CreatePartnerInputValidator>();
            services.AddScoped<IValidator<FindPartnerInput>, FindPartnerInputValidator>();

            return services;
        }

        public static IServiceCollection AddUseCases(this IServiceCollection services)
        {
            services.AddScoped<IUseCase<CreatePartnerInput>, CreatePartnerUseCase>();
            services.AddScoped<IUseCase<FindPartnerInput>, FindPartnerUseCase>();

            return services;
        }
    }
}
