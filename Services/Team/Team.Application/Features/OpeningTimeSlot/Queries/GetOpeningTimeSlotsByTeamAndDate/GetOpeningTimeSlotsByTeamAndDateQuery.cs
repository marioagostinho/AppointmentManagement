using MediatR;
using Team.Application.Dtos;

namespace Team.Application.Features.OpeningTimeSlot.Queries.GetOpeningTimeSlotsByTeamAndDate
{
    public record GetOpeningTimeSlotsByTeamAndDateQuery(Guid TeamId, DateTime Date) : IRequest<IReadOnlyList<OpeningTimeSlotDto>>;
}
