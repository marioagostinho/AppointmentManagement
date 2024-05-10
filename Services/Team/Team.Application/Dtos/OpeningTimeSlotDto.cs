namespace Team.Application.Dtos
{
    public record OpeningTimeSlotDto(Guid Id, TimeSpan StartHour, TimeSpan EndHour);
}
