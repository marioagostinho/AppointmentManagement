namespace Team.Domain.Entities
{
    public class OpeningHoursTimeSlot
    {
        public Guid OpeningHoursId { get; set; }
        public Guid OpeningTimeSlotId { get; set; }

        public OpeningHours? OpeningHours { get; set; }
        public OpeningTimeSlot? OpeningTimeSlot { get; set; }
    }
}
