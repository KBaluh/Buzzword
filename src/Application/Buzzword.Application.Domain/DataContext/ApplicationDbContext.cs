using Buzzword.Application.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Buzzword.Application.Domain.DataContext
{
    public class ApplicationDbContext : DbContext, IApplicationDataSource
    {
        public DbSet<User> Users => Set<User>();
        public DbSet<UserWord> UserWords => Set<UserWord>();
        public DbSet<Group> Groups => Set<Group>();
        public DbSet<GroupWord> GroupWords => Set<GroupWord>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.RemovePluralizingTableNameConvention();
        }
    }
}
