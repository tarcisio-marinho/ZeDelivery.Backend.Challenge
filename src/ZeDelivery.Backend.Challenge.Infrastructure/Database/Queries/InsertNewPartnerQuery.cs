using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeDelivery.Backend.Challenge.Domain.Entities;
using ZeDelivery.Backend.Challenge.Domain.Queries;

namespace ZeDelivery.Backend.Challenge.Infrastructure.Database.Queries
{
    [ExcludeFromCodeCoverage]
    public class InsertNewPartnerQuery : IInsertNewPartnerQuery
    {
        private readonly string ConnectionString;
        private readonly ILogger<InsertNewPartnerQuery> logger;

        public InsertNewPartnerQuery(IConfiguration configuration, ILogger<InsertNewPartnerQuery> logger)
        {
            ConnectionString = configuration.GetConnectionString("SqlConnection");
            this.logger = logger;
        }

        public async Task<bool> ExecuteAsync(Partner partner)
        {
            var sql =
                @"INSERT INTO partner( 
                    `Id`, 
                    `TradingName`, 
                    `OwnerName`, 
                    `Document`,
                    `CoverageAreaType`,
                    `CoverageAreaCoordinates`,
                    `AddressType`, 
                    `AddressCoordinates`
                ) VALUES (
                    @Id, 
                    @TradingName,
                    @OwnerName,
                    @Document, 
                    @CoverageAreaType,
                    ST_GEOMFROMTEXT(@CoverageAreaCoordinates),
                    @AddressType,
                    ST_GEOMFROMTEXT(@AddressCoordinates)
                )";

            try
            {
                using (var db = new MySqlConnection(ConnectionString))
                {
                    var result = await db.ExecuteAsync(sql, new
                    {
                        Id = partner.Id,
                        TradingName = partner.TradingName,
                        OwnerName = partner.OwnerName,
                        Document = partner.Document,
                        CoverageAreaType = partner.CoverageArea.Type,
                        CoverageAreaCoordinates = partner.CoverageArea.Coordinates.ToGeometry(),
                        AddressType = partner.Address.Type,
                        AddressCoordinates = partner.Address.Coordinates.ToGeometry()
                    });

                    return result > 0;
                }
            }
            catch(Exception e)
            {
                logger.LogError(e, $"Error inserting new partner query");

                return false;
            }
        }
    }
}
