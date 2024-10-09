using FlashCard.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FlashCard.Core.Queries.Abstract
{
    public abstract class BaseQuery<TEntity, TKey> : IQuery<TEntity> where TEntity : BaseEntity<TKey>
    {
        protected IQueryable<TEntity> Query { get; set; }

        protected BaseQuery(IQueryable<TEntity> query)
        {
            Query = query.Where(
                entity =>
                    !entity.DeletedDate.HasValue
                );
        }

        public async Task<bool> AnyAsync()
        {
            return await Query.AnyAsync();
        }

        public async Task<IEnumerable<TOutput>> AsEnumerable<TOutput>(Expression<Func<TEntity, TOutput>> selector) where TOutput : class
        {
            return await Query.Select(selector)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<int> CountAsync()
        {
            return await Query.CountAsync();
        }

        public async Task<TEntity?> FirstOrDefaultAsync()
        {
            return await Query.FirstOrDefaultAsync();
        }

        public async Task<TOutput?> FirstOrDefaultAsync<TOutput>(Expression<Func<TEntity, TOutput>> selector)
        {
            return await Query
                .Select(selector)
                .FirstOrDefaultAsync();
        }

        public IQuery<TEntity> OrderBy<TOrderKey>(Expression<Func<TEntity, TOrderKey>> keySelector)
        {
            Query = Query.OrderBy(keySelector);

            return this;
        }

        public IQuery<TEntity> OrderByDescending<TOrderKey>(Expression<Func<TEntity, TOrderKey>> keySelector)
        {
            Query = Query.OrderByDescending(keySelector);

            return this;
        }

        public IQuery<TEntity> Skip(int count)
        {
            Query = Query.Skip(count);

            return this;
        }

        public async Task<long> SumAsync(Expression<Func<TEntity, long>> keySelector)
        {
            return await Query.SumAsync(keySelector);
        }

        public IQuery<TEntity> Take(int count)
        {
            Query = Query.Take(count);

            return this;
        }

        //public async Task<PagingResultDto<TOutput>> AsPagination<TOutput>(Expression<Func<TEntity, TOutput>> selector, int pageNo, int pageSize) where TOutput : class
        //{
        //    return new PagingResultDto<TOutput>
        //    {
        //        Total = await Query.CountAsync(),
        //        Items = await Query.Skip(pageSize * (pageNo - 1)).Take(pageSize).Select(selector).ToListAsync()
        //    };
        //}

        public async Task<List<TEntity>> ToListAsync()
        {
            return await Query.ToListAsync();
        }
    }
}
