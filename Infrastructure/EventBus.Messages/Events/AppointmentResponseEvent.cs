namespace EventBus.Messages.Events
{
    public class AppointmentResponseEvent : BaseIntegrationEvent
    {
        public DateTime StartDateTime { get; set; }
        public TimeSpan Duration { get; set; }
        public Guid TeamId { get; set; }
    }
}
