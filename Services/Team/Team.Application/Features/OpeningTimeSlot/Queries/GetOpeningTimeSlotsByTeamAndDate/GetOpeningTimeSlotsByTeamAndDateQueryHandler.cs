using AutoMapper;
using EventBus.Messages.Events;
using FluentValidation;
using MassTransit;
using MediatR;
using Team.Application.Dtos;
using Team.Domain.Enums;
using Team.Domain.Repositories;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Team.Application.Features.OpeningTimeSlot.Queries.GetOpeningTimeSlotsByTeamAndDate
{
    public class GetOpeningTimeSlotsByTeamAndDateQueryHandler : IRequestHandler<GetOpeningTimeSlotsByTeamAndDateQuery, IReadOnlyList<OpeningTimeSlotDto>>
    {
        private readonly IMapper _mapper;
        private readonly IRequestClient<AppointmentRequestEvent> _requestClient;
        private readonly IOpeningTimeSlotRepository _openingTimeSlotRepository;
        private readonly ITeamRepository _teamRepository;

        public GetOpeningTimeSlotsByTeamAndDateQueryHandler(IMapper mapper, IRequestClient<AppointmentRequestEvent> requestClient,
            IOpeningTimeSlotRepository openingTimeSlotRepository, ITeamRepository teamRepository)
        {
            _mapper = mapper;
            _requestClient = requestClient;
            _openingTimeSlotRepository = openingTimeSlotRepository;
            _teamRepository = teamRepository;
        }

        public async Task<IReadOnlyList<OpeningTimeSlotDto>> Handle(GetOpeningTimeSlotsByTeamAndDateQuery request, CancellationToken cancellationToken)
        {
            var validator = new GetOpeningTimeSlotsByTeamAndDateQueryValidator(_teamRepository);
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if (validatorResult.Errors.Any())
                throw new Exception();

            // AppointmentResponseBatchEvent
            var messageResponse = await _requestClient.GetResponse<AppointmentResponseBatchEvent>(new AppointmentRequestEvent
            {
                TeamId = request.TeamId,
                Date = request.Date
            });

            // Aggroup by appointment start date and minute
            var appointmentCounts = messageResponse.Message.Appointments
                .GroupBy(p => new TimeSpan(p.StartDateTime.Hour, p.StartDateTime.Minute, 0))
                .ToDictionary(g => g.Key, g => g.Count());

            // Get OpeningTimeSlots by team id and day of the week
            EDayOfWeek dayOfWeek = (EDayOfWeek)request.Date.DayOfWeek;
            var openingTimeSlots = await _openingTimeSlotRepository.GetByTeamIdAndDayAsync(request.TeamId, dayOfWeek);

            // Team's amount of apointments
            var team = await _teamRepository.GetByIdAsync(request.TeamId);
            var amountOfAppointments = team.AmountOfAppointments;

            // Filter out times that have the exact number of appointments matching the team's amountOfAppointments
            var filteredOpeningTimeSlots = openingTimeSlots
                .Where(ots => !appointmentCounts.TryGetValue(ots.StartHour, out int count) || count != amountOfAppointments)
                .ToList();

            var result = _mapper.Map<IReadOnlyList<OpeningTimeSlotDto>>(filteredOpeningTimeSlots);
            return result;
        }
    }
}
