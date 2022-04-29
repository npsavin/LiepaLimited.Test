using LiepaLimited.Test.Domain;
using Microsoft.EntityFrameworkCore;

namespace LiepaLimited.Test.Database
{
    public class LiepaLimitedDbContext: DbContext
    {
        public DbSet<UserInfo> Users { get; set; }

        public LiepaLimitedDbContext(DbContextOptions<LiepaLimitedDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(LiepaLimitedDbContext).Assembly);
        }
    }
}
