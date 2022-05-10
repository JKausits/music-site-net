using Microsoft.EntityFrameworkCore;
using MusicSite.API.Interaces;
using MusicSite.API.Persistence.Entities;
using System.Reflection;

namespace MusicSite.API.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        public DbSet<Show> Shows { get; set; }
        public DbSet<Venue> Venues { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            SaveTimeAuditable();
            return base.SaveChangesAsync(cancellationToken);
        }


        private void SaveTimeAuditable()
        {
            var entries = ChangeTracker.Entries<ITimeAuditable>()
                .Where(x => x.State.Equals(EntityState.Added) || x.State.Equals(EntityState.Modified));

            foreach (var entry in entries)
            {
                entry.Entity.ModifiedAt = DateTime.UtcNow;

                if (entry.State.Equals(EntityState.Added))
                    entry.Entity.CreatedAt = DateTime.UtcNow;
            }
        }
    }
}
