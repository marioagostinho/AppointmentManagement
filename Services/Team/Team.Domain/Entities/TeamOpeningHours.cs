namespace Team.Domain.Entities
{
    public class TeamOpeningHours
    {
        public Guid TeamId { get; set; }
        public Guid OpeningHoursId { get; set; }

        public Team? Team { get; set; }
        public OpeningHours? OpeningHours { get; set; }
    }
}
