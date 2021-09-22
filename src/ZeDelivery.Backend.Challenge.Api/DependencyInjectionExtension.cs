using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using ZeDelivery.Backend.Challenge.Api.UseCases.CreatePartner;
using ZeDelivery.Backend.Challenge.Api.UseCases.FindPartner;
using ZeDelivery.Backend.Challenge.Api.UseCases.SearchNearestPartner;
using ZeDelivery.Backend.Challenge.Application.UseCases.CreatePartner;
using ZeDelivery.Backend.Challenge.Application.UseCases.FindPartner;
using ZeDelivery.Backend.Challenge.Application.UseCases.SearchNearestPartner;

namespace ZeDelivery.Backend.Challenge.Api
{
    [ExcludeFromCodeCoverage]
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

            services.AddScoped<ISearchNearestPartnerOutputPort, SearchNearestPartnerPresenter>();
            services.AddScoped<SearchNearestPartnerPresenter>();
            services.AddScoped(typeof(ISearchNearestPartnerOutputPort), sp => sp.GetRequiredService<SearchNearestPartnerPresenter>());

            return services;
        }
    }
}
