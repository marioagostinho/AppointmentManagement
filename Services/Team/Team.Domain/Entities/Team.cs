namespace Team.Domain.Entities
{
    public class Team : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public TimeSpan AppointmentDuration { get; set; }
        public int AmountOfAppointments { get; set; }

        public List<TeamOpeningHours>? TeamOpeningHours { get; set; }

    }
}
