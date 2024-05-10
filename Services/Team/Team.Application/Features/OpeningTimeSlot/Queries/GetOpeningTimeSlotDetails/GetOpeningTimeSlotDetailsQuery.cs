using MediatR;
using Team.Application.Dtos;

namespace Team.Application.Features.OpeningTimeSlot.Queries.GetOpeningTimeSlotDetails
{
    public record GetOpeningTimeSlotDetailsQuery(Guid Id) : IRequest<OpeningTimeSlotDto>;
}
