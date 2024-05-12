namespace EventBus.Messages.Events
{
    public class AppointmentResponseBatchEvent : BaseIntegrationEvent
    {
        public List<AppointmentResponseEvent> Appointments { get; set; }

        public AppointmentResponseBatchEvent(List<AppointmentResponseEvent> appointments)
        {
            Appointments = appointments;
        }
    }
}
