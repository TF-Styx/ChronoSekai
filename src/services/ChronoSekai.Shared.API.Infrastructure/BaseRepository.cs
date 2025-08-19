using Microsoft.EntityFrameworkCore;

namespace ChronoSekai.Shared.API.Infrastructure
{
    public abstract class BaseRepository<TEntity, TContext> : IBaseRepository<TEntity>
        where TEntity : class
        where TContext : IBaseContext
    {
        protected readonly TContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        protected BaseRepository(TContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public virtual async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await _dbSet.AddAsync(entity, cancellationToken);

            return entity;
        }

        public virtual void Remove(TEntity entity) => _dbSet.Remove(entity);
    }
}
