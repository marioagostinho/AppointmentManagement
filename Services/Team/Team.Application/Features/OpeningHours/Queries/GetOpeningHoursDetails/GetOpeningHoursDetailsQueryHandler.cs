using AutoMapper;
using MediatR;
using Team.Application.Dtos;
using Team.Domain.Repositories;

namespace Team.Application.Features.OpeningHours.Queries.GetOpeningHoursDetails
{
    public class GetOpeningHoursDetailsQueryHandler : IRequestHandler<GetOpeningHoursDetailsQuery, OpeningHoursDto>
    {

        private readonly IMapper _mapper;
        private readonly IOpeningHoursRepository _openingHoursRepository;

        public GetOpeningHoursDetailsQueryHandler(IMapper mapper, IOpeningHoursRepository openingHoursRepository)
        {
            _mapper = mapper;
            _openingHoursRepository = openingHoursRepository;
        }

        public async Task<OpeningHoursDto> Handle(GetOpeningHoursDetailsQuery request, CancellationToken cancellationToken)
        {
            var openingHoursDetails = await _openingHoursRepository.GetByIdAsync(request.Id, include: true);
            var result = _mapper.Map<OpeningHoursDto>(openingHoursDetails);

            return result;
        }
    }
}
