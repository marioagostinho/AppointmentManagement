using MediatR;
using Team.Application.Dtos;
namespace Team.Application.Features.Team.Queries.GetTeamDetails
{
    public record GetTeamDetailsQuery(Guid Id) : IRequest<TeamDto>;
}
