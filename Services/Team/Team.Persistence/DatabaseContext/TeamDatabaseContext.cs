using Entities = Team.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Team.Domain.Entities;
using Team.Persistence.Configurations;

namespace Team.Persistence.DatabaseContext
{
    public class TeamDatabaseContext : DbContext
    {
        public TeamDatabaseContext(DbContextOptions<TeamDatabaseContext> options) : base(options)
        {
            Database.Migrate();
        }

        public DbSet<OpeningTimeSlot> OpeningTimeSlots { get; set; }
        public DbSet<OpeningHours> OpeningHours { get; set; }
        public DbSet<Entities.Team> Teams { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TeamDatabaseContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in base.ChangeTracker.Entries<Entities.BaseEntity>()
                .Where(q => q.State == EntityState.Added || q.State == EntityState.Modified))
            {
                entry.Entity.DateModified = DateTime.Now;
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.DateCreated = DateTime.Now;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
