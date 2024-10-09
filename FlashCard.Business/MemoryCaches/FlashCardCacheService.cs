using Microsoft.Extensions.Caching.Memory;

namespace FlashCard.Business.MemoryCaches
{
    public class FlashCardCacheService : IFlashCardCacheService
    {
        private readonly IMemoryCache _memoryCache;

        public FlashCardCacheService
        (
            IMemoryCache memoryCache
        )
        {
            _memoryCache = memoryCache;
        }
        public void ClearAll()
        {
            _memoryCache.Remove(CacheEntries.OTPCodeEntry);
        }

        public async Task<TOutput?> GetAsync<TOutput>(string entry, Func<Task<TOutput>> task)
        {
            return await _memoryCache.GetOrCreateAsync
            (
                entry,
                async cacheEntry =>
                {
                    cacheEntry.SetAbsoluteExpiration(TimeSpan.FromSeconds(GetCacheTimeInSecond()));
                    return await task();
                }
            );
        }

        public void Remove(string entry)
        {
            _memoryCache.Remove(entry);
        }

        private int GetCacheTimeInSecond()
        {
            return 24 * 60 * 60; // default 1 day
        }

        public T? Get<T>(string entry)
        {
            return _memoryCache.Get<T>(entry);
        }

        public void Set<T>(string entry, T Data, int cacheTimeInSeconds)
        {
            _memoryCache.Set<T>(entry, Data, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(cacheTimeInSeconds)
            });
        }
    }
}
