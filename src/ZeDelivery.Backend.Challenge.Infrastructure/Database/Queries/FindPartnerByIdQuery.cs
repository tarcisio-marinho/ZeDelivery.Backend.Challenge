﻿using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeDelivery.Backend.Challenge.Domain.Entities;
using ZeDelivery.Backend.Challenge.Domain.Queries;

namespace ZeDelivery.Backend.Challenge.Infrastructure.Database.Queries
{
    public class FindPartnerByIdQuery : IFindPartnerByIdQuery
    {
        private readonly string ConnectionString;

        public FindPartnerByIdQuery(IConfiguration configuration)
        {
            ConnectionString = configuration.GetConnectionString("SqlConnection");
        }

        public async Task<Partner> ExecuteAsync(string id)
        {
            var sql =
                @"SELECT 
                    Id,
                    TradingName,
                    OwnerName,
                    Document,
                    CoverageAreaType,
                    ST_AsText(CoverageAreaCoordinates) as CoverageAreaCoordinates,
                    AddressType,
                    ST_AsText(AddressCoordinates) as AddressCoordinates
                FROM partner
                WHERE Id = @Id;
                ";

            try
            {
                using (var db = new MySqlConnection(ConnectionString))
                {
                    var result = await db.QueryAsync<Partner>(sql, new
                    {
                        Id = id,
                    });

                    return result.FirstOrDefault();
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
