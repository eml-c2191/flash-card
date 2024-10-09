using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashCard.Business.MemoryCaches
{
    public interface IFlashCardCacheService
    {
        void Remove(string entry);

        Task<TOutput?> GetAsync<TOutput>(string entry, Func<Task<TOutput>> task);

        void ClearAll();

        T? Get<T>(string entry);

        void Set<T>(string entry, T Data, int cacheTimeInSeconds);
    }
}
