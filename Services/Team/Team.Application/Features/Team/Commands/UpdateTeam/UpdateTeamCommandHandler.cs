using AutoMapper;
using MediatR;
using Entities = Team.Domain.Entities;
using Team.Domain.Repositories;

namespace Team.Application.Features.Team.Commands.UpdateTeam
{
    public class UpdateTeamCommandHandler : IRequestHandler<UpdateTeamCommand, bool>
    {
        private readonly IMapper _mapper;
        private readonly ITeamRepository _teamRepository;
        private readonly IOpeningHoursRepository _openingHoursRepository;

        public UpdateTeamCommandHandler(IMapper mapper, ITeamRepository teamRepository, IOpeningHoursRepository openingHoursRepository)
        {
            _mapper = mapper;
            _teamRepository = teamRepository;
            _openingHoursRepository = openingHoursRepository;
        }

        public async Task<bool> Handle(UpdateTeamCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateTeamCommandValidator(_teamRepository, _openingHoursRepository);
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if (validatorResult.Errors.Any())
                throw new Exception();

            var team = _mapper.Map<Entities.Team>(request);

            // Instantiate TeamOpeningHours Relationship
            team.TeamOpeningHours = request.OpeningHoursIds.Select(id => new Entities.TeamOpeningHours
            {
                TeamId = team.Id,
                OpeningHoursId = id
            }).ToList();

            var result = await _teamRepository.UpdateAsync(team);

            return result;
        }
    }
}
