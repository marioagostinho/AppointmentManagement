using AutoMapper;
using MediatR;
using Team.Domain.Repositories;

namespace Team.Application.Features.Team.Commands.DeleteTeam
{
    public class DeleteTeamCommandHandler : IRequestHandler<DeleteTeamCommand, bool>
    {
        private readonly ITeamRepository _teamRepository;

        public DeleteTeamCommandHandler(IMapper mapper, ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }

        public async Task<bool> Handle(DeleteTeamCommand request, CancellationToken cancellationToken)
        {
            var team = await _teamRepository.GetByIdAsync(request.Id);

            if (team == null)
                throw new Exception();

            var result = await _teamRepository.DeleteAsync(team);

            return result;
        }
    }
}
