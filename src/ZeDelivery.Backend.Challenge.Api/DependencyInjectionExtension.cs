using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZeDelivery.Backend.Challenge.Api.UseCases.CreatePartner;
using ZeDelivery.Backend.Challenge.Application.UseCases.CreatePartner;

namespace ZeDelivery.Backend.Challenge.Api
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection AddPresenters(this IServiceCollection services)
        {

            services.AddScoped<ICreatePartnerOutputPort, CreatePartnerPresenter>();
            services.AddScoped<CreatePartnerPresenter>();
            services.AddScoped(typeof(ICreatePartnerOutputPort), sp => sp.GetRequiredService<CreatePartnerPresenter>());

            return services;
        }
    }
}
