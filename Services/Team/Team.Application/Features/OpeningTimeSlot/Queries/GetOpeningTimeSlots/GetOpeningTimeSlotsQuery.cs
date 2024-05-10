using MediatR;
using Team.Application.Dtos;

namespace Team.Application.Features.OpeningTimeSlot.Queries.GetOpeningTimeSlots
{
    public record GetOpeningTimeSlotsQuery : IRequest<IReadOnlyList<OpeningTimeSlotDto>>;
}
