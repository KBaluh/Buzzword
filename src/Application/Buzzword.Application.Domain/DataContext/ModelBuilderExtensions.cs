using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Buzzword.Application.Domain.DataContext
{
    public static class ModelBuilderExtensions
    {
        public static void RemovePluralizingTableNameConvention(this ModelBuilder modelBuilder)
        {
            foreach (IMutableEntityType entity in modelBuilder.Model.GetEntityTypes())
            {
                entity.SetTableName(entity.DisplayName());
            }
        }

        public static void OnDeleteRestrict<TEntity, TRelatedEntity>(this ModelBuilder modelBuilder,
            Expression<Func<TEntity, TRelatedEntity>> navigationProperty,
            Expression<Func<TRelatedEntity, IEnumerable<TEntity>>> navigationCollection)
            where TEntity : class
            where TRelatedEntity : class
        {
            modelBuilder.Entity<TEntity>()
                .HasOne(navigationProperty)
                .WithMany(navigationCollection)
                .OnDelete(DeleteBehavior.Restrict);
        }

        public static void OnDeleteCascade<TEntity, TRelatedEntity>(this ModelBuilder modelBuilder,
            Expression<Func<TEntity, TRelatedEntity>> navigationProperty,
            Expression<Func<TRelatedEntity, IEnumerable<TEntity>>> navigationCollection)
            where TEntity : class
            where TRelatedEntity : class
        {
            modelBuilder.Entity<TEntity>()
                .HasOne(navigationProperty)
                .WithMany(navigationCollection)
                .OnDelete(DeleteBehavior.Cascade);
        }

        public static void OnDeleteNoAction<TEntity, TRelatedEntity>(this ModelBuilder modelBuilder,
            Expression<Func<TEntity, TRelatedEntity>> navigationProperty,
            Expression<Func<TRelatedEntity, IEnumerable<TEntity>>> navigationCollection)
            where TEntity : class
            where TRelatedEntity : class
        {
            modelBuilder.Entity<TEntity>()
                .HasOne(navigationProperty)
                .WithMany(navigationCollection)
                .OnDelete(DeleteBehavior.NoAction);
        }

        public static void OnDeleteSetNull<TEntity, TRelatedEntity>(this ModelBuilder modelBuilder,
            Expression<Func<TEntity, TRelatedEntity>> navigationProperty,
            Expression<Func<TRelatedEntity, IEnumerable<TEntity>>> navigationCollection)
            where TEntity : class
            where TRelatedEntity : class
        {
            modelBuilder.Entity<TEntity>()
                .HasOne(navigationProperty)
                .WithMany(navigationCollection)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
