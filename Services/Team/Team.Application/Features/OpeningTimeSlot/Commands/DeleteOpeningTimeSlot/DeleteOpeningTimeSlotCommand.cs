using MediatR;

namespace Team.Application.Features.OpeningTimeSlot.Commands.DeleteOpeningTimeSlot
{
    public record DeleteOpeningTimeSlotCommand(Guid Id) : IRequest<bool>;
}
