using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeDelivery.Backend.Challenge.Application.Services.Caching;
using ZeDelivery.Backend.Challenge.Domain.Queries;
using ZeDelivery.Backend.Challenge.Infrastructure.Database.Queries;
using ZeDelivery.Backend.Challenge.Infrastructure.Services.Caching;

namespace ZeDelivery.Backend.Challenge.Infrastructure
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<ICacheService, CacheService>();
            services.AddSingleton<RedisConnector>();
            return services;
        }

        public static IServiceCollection AddQueries(this IServiceCollection services)
        {
            services.AddScoped<IInsertNewPartnerQuery, InsertNewPartnerQuery>();
            services.AddScoped<ICheckIfPartnerExistsQuery, CheckIfPartnerExistsQuery>();
            return services;
        }
    }
}
