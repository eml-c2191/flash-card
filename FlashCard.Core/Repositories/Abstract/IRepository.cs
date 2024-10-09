using FlashCard.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashCard.Core.Repositories.Abstract
{
    public interface IRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        Task<TEntity?> FindAsync(TKey key);

        Task AddAsync(TEntity entity);

        Task AddRangeAsync(IEnumerable<TEntity> entities);

        void SoftRemove(TEntity entity);

        void SoftRemoveRange(List<TEntity> entities);
    }
}
