using MediatR;
using Team.Application.Dtos;

namespace Team.Application.Features.Team.Queries.GetTeams
{
    public record GetTeamsQuery : IRequest<IReadOnlyList<TeamDto>>;
}
