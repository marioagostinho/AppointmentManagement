using Entities = Appointment.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Appointment.Persistence.DatabaseContext
{
    public class AppointmentDatabaseContext : DbContext
    {
        public AppointmentDatabaseContext(DbContextOptions<AppointmentDatabaseContext> options) : base(options)
        {
            if (Database.IsRelational())
            {
                Database.Migrate();
            }
        }

        public DbSet<Entities.Appointment> Appointments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppointmentDatabaseContext).Assembly);
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
