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
            var validationTask = validator.ValidateAsync(request, cancellationToken);

            // Send AppointmentRequestEvent while simultaneously with validation
            var messageResponseTask = _requestClient.GetResponse<AppointmentResponseBatchEvent>(new AppointmentRequestEvent
            {
                TeamId = request.TeamId,
                Date = request.Date
            });

            // Await validation to complete
            var validatorResult = await validationTask;

            if (validatorResult.Errors.Any())
                throw new Exception();

            // Await the message response for appointments
            var messageResponse = await messageResponseTask;
            var appointmentsDates = new HashSet<TimeSpan>(
                messageResponse.Message.Appointments.Select(p => new TimeSpan(p.StartDateTime.Hour, p.StartDateTime.Minute, 0)));

            // Get OpeningTimeSlots by team id and day of the weak
            EDayOfWeek dayOfWeek = (EDayOfWeek)request.Date.DayOfWeek;
            var openingTimeSlots = await _openingTimeSlotRepository.GetByTeamIdAndDayAsync(request.TeamId, dayOfWeek);

            // Filter out times that conflict with appointments
            var filteredOpeningTimeSlots = openingTimeSlots
                .Where(ots => !appointmentsDates.Contains(ots.StartHour))
                .ToList();

            var result = _mapper.Map<IReadOnlyList<OpeningTimeSlotDto>>(filteredOpeningTimeSlots);
            return result;
        }
    }
}
