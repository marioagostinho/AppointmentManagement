using Team.Domain.Enums;

namespace Team.Application.Dtos
{
    public record OpeningHoursDto(Guid Id, EDayOfWeek DayOfWeek, IReadOnlyList<OpeningTimeSlotDto> OpeningTimeSlots);
}
