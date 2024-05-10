using AutoMapper;
using MediatR;
using Team.Application.Dtos;
using Team.Domain.Repositories;

namespace Team.Application.Features.OpeningHours.Queries.GetOpeningHours
{
    public class GetOpeningHoursQueryHandler : IRequestHandler<GetOpeningHoursQuery, IReadOnlyList<OpeningHoursDto>>
    {
        private readonly IMapper _mapper;
        private readonly IOpeningHoursRepository _openingHoursRepository;

        public GetOpeningHoursQueryHandler(IMapper mapper, IOpeningHoursRepository openingHoursRepository)
        {
            _mapper = mapper;
            _openingHoursRepository = openingHoursRepository;
        }

        public async Task<IReadOnlyList<OpeningHoursDto>> Handle(GetOpeningHoursQuery request, CancellationToken cancellationToken)
        {
            var openingHours = await _openingHoursRepository.GetAllAsync(include: true);
            var result = _mapper.Map<IReadOnlyList<OpeningHoursDto>>(openingHours);

            return result;
        }
    }
}
