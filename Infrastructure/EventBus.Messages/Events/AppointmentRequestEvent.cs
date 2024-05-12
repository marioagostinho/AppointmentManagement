namespace EventBus.Messages.Events
{
    public class AppointmentRequestEvent : BaseIntegrationEvent
    {
        public Guid TeamId { get; set; }
        public DateTime Date { get; set; }
    }
}
