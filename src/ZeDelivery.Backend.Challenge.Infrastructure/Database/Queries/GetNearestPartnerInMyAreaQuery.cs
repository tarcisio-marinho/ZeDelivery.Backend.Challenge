using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeDelivery.Backend.Challenge.Domain.Entities;
using ZeDelivery.Backend.Challenge.Domain.Queries;

namespace ZeDelivery.Backend.Challenge.Infrastructure.Database.Queries
{
    [ExcludeFromCodeCoverage]
    public class GetNearestPartnerInMyAreaQuery : IGetNearestPartnerInMyAreaQuery
    {
        private readonly string ConnectionString;
        private readonly ILogger<GetNearestPartnerInMyAreaQuery> logger;
        public GetNearestPartnerInMyAreaQuery(IConfiguration configuration, ILogger<GetNearestPartnerInMyAreaQuery> logger)
        {
            ConnectionString = configuration.GetConnectionString("SqlConnection");
            this.logger = logger;
        }

        public async Task<Partner> ExecuteAsync(Point point)
        {
            var sql = @"
                        select 
                            Id,
                            TradingName,
                            OwnerName,
                            Document,
                            CoverageAreaType,
                            ST_AsText(CoverageAreaCoordinates) as CoverageAreaCoordinates,
                            AddressType,
                            ST_AsText(AddressCoordinates) as AddressCoordinates
                        from partner 
                        where st_contains(CoverageAreaCoordinates, pointfromtext(@Point))
                        ORDER BY st_distance_sphere(pointfromtext(@Point), AddressCoordinates) ASC
                        LIMIT 1;
                    ";
            try
            {
                using (var db = new MySqlConnection(ConnectionString))
                {
                    var result = await db.QueryAsync<Partner>(sql, new
                    {
                        Point = point.ToGeometry(),
                    });

                    return result.FirstOrDefault();
                }
            }
            catch (Exception e)
            {
                logger.LogError(e, $"Error getting nearest partner query");
                return default;
            }
        }
    }
}
