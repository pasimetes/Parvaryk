using Microsoft.Extensions.Caching.Memory;
using Parvaryk.Application.Common.Interfaces;
using System;
using System.Threading.Tasks;

namespace Parvaryk.Infrastructure.Cache
{
    public class MemoryCacheProvider : IMemoryCacheProvider
    {
        private readonly IMemoryCache _memoryCache;

        public MemoryCacheProvider(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public async Task<TResult> Get<TResult>(string key, Func<Task<TResult>> retrievalOperation)
        {
            var result = _memoryCache.Get<TResult>(key);
            if (result != null)
            {
                return result;
            }

            result = await retrievalOperation();

            _memoryCache.Set(key, result, TimeSpan.FromHours(6));

            return result;
        }

        public void Invalidate(string key)
        {
            _memoryCache.Remove(key);
        }
    }
}
