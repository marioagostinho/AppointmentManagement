using AutoMapper;
using MediatR;
using Team.Application.Dtos;
using Team.Domain.Repositories;

namespace Team.Application.Features.Team.Queries.GetTeams
{
    public class GetTeamsQueryHandler : IRequestHandler<GetTeamsQuery, IReadOnlyList<TeamDto>>
    {
        private readonly IMapper _mapper;
        private readonly ITeamRepository _teamRepository;

        public GetTeamsQueryHandler(IMapper mapper, ITeamRepository teamRepository)
        {
            _mapper = mapper;
            _teamRepository = teamRepository;
        }

        public async Task<IReadOnlyList<TeamDto>> Handle(GetTeamsQuery request, CancellationToken cancellationToken)
        {
            var teams = await _teamRepository.GetAllAsync(include: true);
            var response = _mapper.Map<IReadOnlyList<TeamDto>>(teams);

            return response;
        }
    }
}
