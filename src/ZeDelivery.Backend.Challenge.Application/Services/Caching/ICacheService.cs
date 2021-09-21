using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeDelivery.Backend.Challenge.Application.Services.Caching
{
    public interface ICacheService
    {
        public Task<CacheEntry<T>> TryGetAsync<T>(string key);
        public Task TrySetAsync<T>(string key, T value);
    }
}
