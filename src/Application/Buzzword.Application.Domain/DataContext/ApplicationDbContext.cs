using Buzzword.Application.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Buzzword.Application.Domain.DataContext
{
    public class ApplicationDbContext : DbContext, IApplicationDataSource
    {
        public DbSet<User> Users => Set<User>();
        public DbSet<UserWord> UserWords => Set<UserWord>();
        public DbSet<Group> UserGroups => Set<Group>();
        public DbSet<GroupWord> GroupWords => Set<GroupWord>();

        public ApplicationDbContext(DbContextOptions contextOptions)
            : base(contextOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.RemovePluralizingTableNameConvention();

            modelBuilder.OnDeleteNoAction<UserWord, User>(x => x.User, x => x.UserWords);
            modelBuilder.OnDeleteCascade<Group, User>(x => x.User, x => x.Groups);
            modelBuilder.OnDeleteCascade<GroupWord, Group>(x => x.Group, x => x.GroupWords);
        }
    }
}
