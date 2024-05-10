using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Team.Domain.Entities;


namespace Team.Persistence.Configurations
{
    public class OpeningHoursTimeSlotConfiguration : IEntityTypeConfiguration<OpeningHoursTimeSlot>
    {
        public void Configure(EntityTypeBuilder<OpeningHoursTimeSlot> builder)
        {
            builder
                .HasKey(to => new { to.OpeningHoursId, to.OpeningTimeSlotId });

            builder
                .HasOne(to => to.OpeningHours)
                .WithMany(o => o.OpeningHoursTimeSlots)
                .HasForeignKey(to => to.OpeningHoursId);

            builder
                .HasOne(to => to.OpeningTimeSlot)
                .WithMany(t => t.OpeningHoursTimeSlots)
                .HasForeignKey(to => to.OpeningTimeSlotId);
        }
    }
}
