using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeDelivery.Backend.Challenge.Infrastructure.Services.Caching
{
    [ExcludeFromCodeCoverage]
    public class RedisConnector
    {
        public IDatabase Database { get; private set; }
        private ConnectionMultiplexer multiplexer { get; set; }
        public RedisConnector(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("RedisConnection");

            multiplexer = ConnectionMultiplexer.Connect(connectionString); 

            Database = multiplexer.GetDatabase();
        }
    }
}
