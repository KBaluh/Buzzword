using Buzzword.Application.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Buzzword.Application.Domain.DataContext
{
    public interface IDataSource : IDisposable
    {
        DbSet<User> Users { get; }
        DbSet<UserWord> UserWords { get;}
        DbSet<Group> Groups { get; }
        DbSet<GroupWord> GroupWords { get; }

        EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
