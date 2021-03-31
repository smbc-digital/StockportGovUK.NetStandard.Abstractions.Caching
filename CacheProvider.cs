using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;

namespace StockportGovUK.NetStandard.Abstractions.Caching
{
    public class CacheProvider : ICacheProvider
    {
        private readonly CacheProviderConfiguration _cacheProviderConfiguration;

        private readonly IDistributedCache _distributedCache;

        public CacheProvider(IDistributedCache distributedCache, IOptions<CacheProviderConfiguration> cacheProviderConfiguration )
        {
            _distributedCache = distributedCache;
            _cacheProviderConfiguration = cacheProviderConfiguration.Value;
        }

        public async Task<string> GetStringAsync(string key)
        {
            if (_cacheProviderConfiguration.AllowCaching)
                return await _distributedCache.GetStringAsync(key);

            return null;
        }

        public async Task SetStringAsync(string key, string value)
        {
            if (_cacheProviderConfiguration.AllowCaching)
            {
                await _distributedCache.SetStringAsync(key, value, new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(_cacheProviderConfiguration.Timeout)
                });
            }
        }

        public async Task SetStringAsync(string key, string value, DistributedCacheEntryOptions options)
        {
            if (_cacheProviderConfiguration.AllowCaching)
                await _distributedCache.SetStringAsync(key, value, options);
        }
    }
}