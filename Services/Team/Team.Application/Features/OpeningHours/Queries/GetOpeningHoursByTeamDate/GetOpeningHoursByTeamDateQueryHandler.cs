using AutoMapper;
using EventBus.Messages.Events;
using MassTransit;
using MediatR;
using Team.Application.Dtos;
using Team.Domain.Enums;
using Team.Domain.Repositories;

namespace Team.Application.Features.OpeningHours.Queries.GetOpeningHoursByTeamDate
{
    public class GetOpeningHoursByTeamDateQueryHandler : IRequestHandler<GetOpeningHoursByTeamDateQuery ,IReadOnlyList<OpeningHoursDto>>
    {
        private readonly IMapper _mapper;
        private readonly IRequestClient<AppointmentRequestEvent> _requestClient;
        private readonly IOpeningHoursRepository _openingHoursRepository;
        private readonly ITeamRepository _teamRepository;

        public GetOpeningHoursByTeamDateQueryHandler(IMapper mapper, IRequestClient<AppointmentRequestEvent> requestClient,
            IOpeningHoursRepository openingHoursRepository, ITeamRepository teamRepository)
        {
            _mapper = mapper;
            _requestClient = requestClient;
            _openingHoursRepository = openingHoursRepository;
            _teamRepository = teamRepository;
        }

        public async Task<IReadOnlyList<OpeningHoursDto>> Handle(GetOpeningHoursByTeamDateQuery request, CancellationToken cancellationToken)
        {
            var validator = new GetOpeningHoursByTeamDateQueryValidator(_teamRepository);
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if (validatorResult.Errors.Any())
                throw new Exception();

            // Get day of the week by day
            EDayOfWeek dayOfWeek = (EDayOfWeek)request.Date.DayOfWeek;

            // Get team opening hours by team
            var team = await _teamRepository.GetByIdAsync(request.TeamId, include: true);

            if (team == null || team.TeamOpeningHours == null || team.TeamOpeningHours.Count == 0)
                throw new Exception();

            var openingHoursId = new HashSet<Guid>(team.TeamOpeningHours.Select(p => p.OpeningHoursId));
            var openingHours = await _openingHoursRepository.GetByIdRangeAsync(openingHoursId, include: true);

            // Get opening hours by day of the week
            var openingHoursByDay = openingHours
                .Where(p => p.DayOfWeek == dayOfWeek)
                .ToList();

            var messageResponse = await _requestClient.GetResponse<AppointmentResponseBatchEvent>(new AppointmentRequestEvent
            {
                TeamId = request.TeamId,
                Date = request.Date
            });

            List<DateTime> dates = messageResponse.Message.Appointments
                .Select(p => p.StartDateTime)
                .ToList();

            var result = _mapper.Map<IReadOnlyList<OpeningHoursDto>>(openingHoursByDay);

            return result;
        }
    }
}
