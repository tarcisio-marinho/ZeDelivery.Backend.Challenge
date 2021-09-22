using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeDelivery.Backend.Challenge.Domain.Queries;

namespace ZeDelivery.Backend.Challenge.Infrastructure.Database.Queries
{
    [ExcludeFromCodeCoverage]
    public class CheckIfPartnerExistsQuery : ICheckIfPartnerExistsQuery
    {
        private readonly string ConnectionString;
        private readonly ILogger<CheckIfPartnerExistsQuery> logger;

        public CheckIfPartnerExistsQuery(IConfiguration configuration, ILogger<CheckIfPartnerExistsQuery> logger)
        {
            ConnectionString = configuration.GetConnectionString("SqlConnection");
            this.logger = logger;
        }

        public async Task<bool> ExecuteAsync(string id)
        {

            var sql = @"
                        SELECT 
                            *
                        FROM partner
                        where Id = @Id"; // TODO: add indexes at database creation

            try
            {
                using (var db = new MySqlConnection(ConnectionString))
                {
                    var result = await db.QueryAsync(sql, new
                    {
                        Id = id,
                    });

                    return result.Any();
                }
            }
            catch (Exception e)
            {
                logger.LogError(e, $"Error Checking if partner exists query");

                return false;
            }
        }
    }
}
