namespace Team.Domain.Entities
{
    public class OpeningTimeSlot : BaseEntity
    {
        public TimeSpan StartHour { get; set; }
        public TimeSpan EndHour { get; set; }

        public List<OpeningHoursTimeSlot>? OpeningHoursTimeSlots { get; set; }
    }
}
