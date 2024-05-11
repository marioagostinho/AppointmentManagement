namespace Appointment.Domain.Entities
{
    public class Appointment : BaseEntity
    {
        public DateTime StartDateTime { get; set; }
        public TimeSpan Duration { get; set; }
        public Guid TeamId { get; set; }
    }
}
