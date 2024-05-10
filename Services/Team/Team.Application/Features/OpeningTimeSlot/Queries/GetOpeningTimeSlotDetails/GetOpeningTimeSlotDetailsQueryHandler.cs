using AutoMapper;
using MediatR;
using Team.Application.Dtos;
using Team.Domain.Repositories;

namespace Team.Application.Features.OpeningTimeSlot.Queries.GetOpeningTimeSlotDetails
{
    public class GetOpeningTimeSlotDetailsQueryHandler : IRequestHandler<GetOpeningTimeSlotDetailsQuery, OpeningTimeSlotDto>
    {
        private readonly IOpeningTimeSlotRepository _openingTimeSlotRepository;
        private readonly IMapper _mapper;

        public GetOpeningTimeSlotDetailsQueryHandler(IOpeningTimeSlotRepository openingTimeSlotRepository, IMapper mapper)
        {
            _openingTimeSlotRepository = openingTimeSlotRepository;
            _mapper = mapper;
        }

        public async Task<OpeningTimeSlotDto> Handle(GetOpeningTimeSlotDetailsQuery request, CancellationToken cancellationToken)
        {
            var openingTimeSlot = await _openingTimeSlotRepository.GetByIdAsync(request.Id);
            var result = _mapper.Map<OpeningTimeSlotDto>(openingTimeSlot);

            return result;
        }
    }
}
