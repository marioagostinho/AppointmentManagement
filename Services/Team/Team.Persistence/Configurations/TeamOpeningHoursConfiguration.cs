using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Team.Domain.Entities;

namespace Team.Persistence.Configurations
{
    public class TeamOpeningHoursConfiguration : IEntityTypeConfiguration<TeamOpeningHours>
    {
        public void Configure(EntityTypeBuilder<TeamOpeningHours> builder)
        {
            builder
                .HasKey(to => new { to.TeamId, to.OpeningHoursId });

            builder
                .HasOne(to => to.OpeningHours)
                .WithMany(o => o.TeamOpeningHours)
                .HasForeignKey(to => to.OpeningHoursId);

            builder
                .HasOne(to => to.Team)
                .WithMany(t => t.TeamOpeningHours)
                .HasForeignKey(to => to.TeamId);
        }
    }
}