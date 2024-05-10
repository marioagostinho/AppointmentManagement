using AutoMapper;
using MediatR;
using Team.Application.Dtos;
using Team.Domain.Repositories;

namespace Team.Application.Features.OpeningTimeSlot.Queries.GetOpeningTimeSlots
{
    public class GetOpeningTimeSlotsQueryHandler : IRequestHandler<GetOpeningTimeSlotsQuery, IReadOnlyList<OpeningTimeSlotDto>>
    {
        private readonly IOpeningTimeSlotRepository _openingTimeSlotRepository;
        private readonly IMapper _mapper;

        public GetOpeningTimeSlotsQueryHandler(IOpeningTimeSlotRepository openingTimeSlotRepository, IMapper mapper)
        {
            _openingTimeSlotRepository = openingTimeSlotRepository;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<OpeningTimeSlotDto>> Handle(GetOpeningTimeSlotsQuery request, CancellationToken cancellationToken)
        {
            var openingTimeSlots = await _openingTimeSlotRepository.GetAllAsync();
            var result = _mapper.Map<IReadOnlyList<OpeningTimeSlotDto>>(openingTimeSlots);

            return result;
        }
    }
}
