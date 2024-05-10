using MediatR;

namespace Team.Application.Features.Team.Commands.DeleteTeam
{
    public record DeleteTeamCommand(Guid Id) : IRequest<bool>;
}
