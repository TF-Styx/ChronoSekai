using Microsoft.EntityFrameworkCore;

namespace ChronoSekai.Shared.API.Infrastructure
{
    public interface IBaseContext
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
