using MediatR;

namespace Team.Application.Features.OpeningTimeSlot.Commands.UpdateOpeningTimeSlot
{
    public record UpdateOpeningTimeSlotCommand(Guid Id, TimeSpan StartHour, TimeSpan EndHour) : IRequest<bool>;
}
