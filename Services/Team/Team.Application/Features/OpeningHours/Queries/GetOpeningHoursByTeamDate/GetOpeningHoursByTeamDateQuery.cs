using MediatR;
using Team.Application.Dtos;

namespace Team.Application.Features.OpeningHours.Queries.GetOpeningHoursByTeamDate
{
    public record GetOpeningHoursByTeamDateQuery(Guid TeamId, DateTime Date) : IRequest<IReadOnlyList<OpeningHoursDto>>;
}
