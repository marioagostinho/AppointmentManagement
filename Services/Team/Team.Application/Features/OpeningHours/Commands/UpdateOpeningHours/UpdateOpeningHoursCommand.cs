using MediatR;
using Team.Domain.Enums;

namespace Team.Application.Features.OpeningHours.Commands.UpdateOpeningHours
{
    public record UpdateOpeningHoursCommand(Guid Id, EDayOfWeek DayOfWeek, HashSet<Guid> OpeningTimeSlotIds) : IRequest<bool>;
}
