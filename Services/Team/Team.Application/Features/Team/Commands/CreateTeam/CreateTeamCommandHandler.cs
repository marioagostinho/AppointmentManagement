using AutoMapper;
using MediatR;
using Team.Domain.Repositories;
using Entities = Team.Domain.Entities;

namespace Team.Application.Features.Team.Commands.CreateTeam
{
    public class CreateTeamCommandHandler : IRequestHandler<CreateTeamCommand, Guid>
    {
        private readonly IMapper _mapper;
        private readonly ITeamRepository _teamRepository;
        private readonly IOpeningHoursRepository _openingHoursRepository;

        public CreateTeamCommandHandler(IMapper mapper, ITeamRepository teamRepository, IOpeningHoursRepository openingHoursRepository)
        {
            _mapper = mapper;
            _teamRepository = teamRepository;
            _openingHoursRepository = openingHoursRepository;
        }

        public async Task<Guid> Handle(CreateTeamCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateTeamCommandValidator(_openingHoursRepository);
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

            await _teamRepository.CreateAsync(team);

            return team.Id;
        }
    }
}
