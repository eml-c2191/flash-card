using FlashCard.Abstract.UserContext;
using FlashCard.Core.Entities;

namespace FlashCard.Core.Repositories.Abstract
{
    public abstract class BaseRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        protected readonly FlashCardDbContext _context;
        protected readonly IUserContext _userContext;
        public BaseRepository
        (
            FlashCardDbContext context,
            IUserContext userContext
        )
        {
            _context = context;
            _userContext = userContext;
        }
        // TO DO: need to use user context
        public async Task<TEntity?> FindAsync(TKey key)
        {
            TEntity? entity = await _context.FindAsync<TEntity>(key);
            if
            (
                entity == null
                || entity.DeletedDate.HasValue
                || !string.IsNullOrWhiteSpace(entity.DeletedBy)
            )
            {
                return null;
            }

            return entity;
        }
        public async Task AddAsync(TEntity entity)
        {
            entity.CreatedDate = DateTime.Now;
            entity.CreatedBy = _userContext.Identity;
            entity.UpdatedDate = DateTime.Now;
            entity.UpdatedBy = _userContext.Identity;

            await _context.AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            entities = entities.Select(entity =>
            {
                entity.CreatedDate = DateTime.Now;
                entity.CreatedBy = _userContext.Identity;
                entity.UpdatedDate = DateTime.Now;
                entity.UpdatedBy = _userContext.Identity;

                return entity;
            });

            await _context.AddRangeAsync(entities);
        }

        public void SoftRemove(TEntity entity)
        {
            entity.DeletedDate = DateTime.Now;
            entity.DeletedBy = _userContext.Identity;
        }

        public void SoftRemoveRange(List<TEntity> entities)
        {
            foreach (TEntity entity in entities)
            {
                entity.DeletedDate = DateTime.Now;
                entity.DeletedBy = _userContext.Identity;
            }
        }
    }
}
