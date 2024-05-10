using MediatR;
using Team.Domain.Enums;

namespace Team.Application.Features.OpeningHours.Commands.CreateOpeningHours
{
    public record CreateOpeningHoursCommand(EDayOfWeek DayOfWeek, HashSet<Guid> OpeningTimeSlotIds) : IRequest<Guid>;
}
