using Buzzword.Application.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Buzzword.Application.Domain.DataContext
{
    public interface IDataSource : IDisposable
    {
        DbSet<User> Users { get; set; }
        DbSet<UserWord> UserWords { get; set; }
        DbSet<Group> Groups { get; set; }
        DbSet<GroupWord> GroupWords { get; set; }

        EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
