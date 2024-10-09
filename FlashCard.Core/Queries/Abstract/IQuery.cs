using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FlashCard.Core.Queries.Abstract
{
    public interface IQuery<TEntity> where TEntity : class
    {
        IQuery<TEntity> Take(int count);

        IQuery<TEntity> Skip(int count);

        Task<IEnumerable<TOutput>> AsEnumerable<TOutput>(Expression<Func<TEntity, TOutput>> selector) where TOutput : class;

        Task<List<TEntity>> ToListAsync();

        //Task<PagingResultDto<TOutput>> AsPagination<TOutput>(Expression<Func<TEntity, TOutput>> selector, int pageNo, int pageSize) where TOutput : class;


        Task<TEntity?> FirstOrDefaultAsync();
        Task<TOutput?> FirstOrDefaultAsync<TOutput>(Expression<Func<TEntity, TOutput>> selector);
        IQuery<TEntity> OrderBy<TOrderKey>(Expression<Func<TEntity, TOrderKey>> keySelector);

        IQuery<TEntity> OrderByDescending<TOrderKey>(Expression<Func<TEntity, TOrderKey>> keySelector);

        Task<long> SumAsync(Expression<Func<TEntity, long>> keySelector);

        Task<bool> AnyAsync();

        Task<int> CountAsync();
    }
}
