using AutoMapper;
using MediatR;
using Team.Application.Dtos;
using Team.Domain.Repositories;

namespace Team.Application.Features.Team.Queries.GetTeamDetails
{
    public class GetTeamDetailsQueryHandler : IRequestHandler<GetTeamDetailsQuery, TeamDto>
    {
        private readonly IMapper _mapper;
        private readonly ITeamRepository _teamRepository;

        public GetTeamDetailsQueryHandler(IMapper mapper, ITeamRepository teamRepository)
        {
            _mapper = mapper;
            _teamRepository = teamRepository;
        }

        public async Task<TeamDto> Handle(GetTeamDetailsQuery request, CancellationToken cancellationToken)
        {
            var team = await _teamRepository.GetByIdAsync(request.Id, include: true);
            var response = _mapper.Map<TeamDto>(team);

            return response;
        }
    }
}
