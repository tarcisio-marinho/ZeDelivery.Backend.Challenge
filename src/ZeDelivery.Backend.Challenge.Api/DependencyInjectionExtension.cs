using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZeDelivery.Backend.Challenge.Api.UseCases.CreatePartner;
using ZeDelivery.Backend.Challenge.Api.UseCases.FindPartner;
using ZeDelivery.Backend.Challenge.Application.UseCases.CreatePartner;
using ZeDelivery.Backend.Challenge.Application.UseCases.FindPartner;

namespace ZeDelivery.Backend.Challenge.Api
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection AddPresenters(this IServiceCollection services)
        {

            services.AddScoped<ICreatePartnerOutputPort, CreatePartnerPresenter>();
            services.AddScoped<CreatePartnerPresenter>();
            services.AddScoped(typeof(ICreatePartnerOutputPort), sp => sp.GetRequiredService<CreatePartnerPresenter>());

            services.AddScoped<IFindPartnerOutputPort, FindPartnerPresenter>();
            services.AddScoped<FindPartnerPresenter>();
            services.AddScoped(typeof(IFindPartnerOutputPort), sp => sp.GetRequiredService<FindPartnerPresenter>());

            return services;
        }
    }
}
