using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeDelivery.Backend.Challenge.Domain.Services.Queries;

namespace ZeDelivery.Backend.Challenge.Infrastructure.Database.Queries
{
    public class CheckIfPartnerExistsQuery : ICheckIfPartnerExistsQuery
    {
        private readonly string ConnectionString;
        public CheckIfPartnerExistsQuery(IConfiguration configuration)
        {
            ConnectionString = configuration.GetConnectionString("SqlConnection"); // TODO: update appsettings

        }

        public async Task<bool> ExecuteAsync(string Id)
        {

            var query = @"
                        SELECT 
                            TOP (1) Id
                        FROM tabela
                        where Id = @Id ";

            using (var connection = new SqlConnection(ConnectionString))
            {
                var result = connection.Query(query).FirstOrDefault();

            }


            return true;
        }
    }
}
