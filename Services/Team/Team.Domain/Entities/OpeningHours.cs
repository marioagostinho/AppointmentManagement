using Team.Domain.Enums;

namespace Team.Domain.Entities
{
    public class OpeningHours : BaseEntity
    {
        public EDayOfWeek DayOfWeek { get; set; }

        public List<OpeningHoursTimeSlot>? OpeningHoursTimeSlots { get; set; }
        public List<TeamOpeningHours>? TeamOpeningHours { get; set; }
    }
}
