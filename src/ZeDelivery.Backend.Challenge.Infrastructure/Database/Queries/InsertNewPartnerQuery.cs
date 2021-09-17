using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeDelivery.Backend.Challenge.Domain.Entities;

namespace ZeDelivery.Backend.Challenge.Infrastructure.Database.Queries
{
    public class InsertNewPartnerQuery
    {
        private readonly string ConnectionString;

        public InsertNewPartnerQuery(IConfiguration configuration)
        {
            ConnectionString = configuration.GetConnectionString("SqlConnection"); // TODO: update appsettings
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
                    @CoverageAreaCoordinates,
                    @AddressType,
                    @AddressCoordinates
                )";

            try
            {
                using (var db = new SqlConnection(ConnectionString))
                {
                    var result = db.Execute(sql, new
                    {
                        Id = partner.Id,
                        TradingName = partner.TradingName,
                        OwnerName = partner.OwnerName,
                        Document = partner.Document,
                        CoverageAreaType = partner.CoverageArea.Type,
                        CoverageAreaCoordinates = partner.CoverageArea.Coordinates,
                        AddressType = partner.Address.Type,
                        AddressCoordinates = partner.Address.Coordinates
                    });
                }
            }catch(Exception e)
            {
                return false;
            }

            return true;
        }
    }
}
