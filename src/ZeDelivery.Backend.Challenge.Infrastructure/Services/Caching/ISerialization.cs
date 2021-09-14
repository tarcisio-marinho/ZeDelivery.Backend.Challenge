using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeDelivery.Backend.Challenge.Infrastructure.Services.Caching
{
    public interface ISerialization
    {
        public string Serialize<T>(T @object);
        public T Deserialize<T>(string packedData);
    }
}
