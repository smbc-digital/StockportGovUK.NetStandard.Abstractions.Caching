using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;

namespace StockportGovUK.NetStandard.Abstractions.Caching
{
    public class CacheProvider
    {
        private readonly bool _allowCaching;

        private readonly double _timeout;

        private readonly IDistributedCache _cacheProvider;

        public CacheProvider(IDistributedCache cacheProvider, bool allowCaching, double timeout = 20)
        {
            _allowCaching = allowCaching;
            _timeout = timeout;
            _cacheProvider = cacheProvider;
        }

        public async Task<string> GetStringAsync(string key)
        {
            if (_allowCaching)
                return await _cacheProvider.GetStringAsync(key);

            return null;
        }

        public async Task SetStringAsync(string key, string value)
        {
            if (_allowCaching)
            {
                await _cacheProvider.SetStringAsync(key, value, new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(_timeout)
                });
            }
        }

        public async Task SetStringAsync(string key, string value, DistributedCacheEntryOptions options)
        {
            if (_allowCaching)
                await _cacheProvider.SetStringAsync(key, value, options);
        }
    }
}