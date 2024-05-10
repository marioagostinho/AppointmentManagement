using MediatR;

namespace Team.Application.Features.OpeningTimeSlot.Commands.CreateOpeningTimeSlot
{
    public record CreateOpeningTimeSlotCommand(TimeSpan StartHour, TimeSpan EndHour) : IRequest<Guid>;
}
