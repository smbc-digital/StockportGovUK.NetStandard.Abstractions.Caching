using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;

namespace StockportGovUK.NetStandard.Abstractions.Caching
{
    public interface ICacheProvider
    {
        Task<string> GetStringAsync(string key);
        Task SetStringAsync(string key, string value);
        Task SetStringAsync(string key, string value, DistributedCacheEntryOptions options);
    }
}