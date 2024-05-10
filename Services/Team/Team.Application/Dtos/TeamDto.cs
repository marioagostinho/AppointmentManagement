namespace Team.Application.Dtos
{
    public record TeamDto(Guid Id, string Name, TimeSpan AppointmentDuration, int AmountOfAppointments, IReadOnlyList<OpeningHoursDto> OpeningHours);
}
