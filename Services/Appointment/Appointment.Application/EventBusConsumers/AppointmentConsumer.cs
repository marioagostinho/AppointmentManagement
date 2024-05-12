using Appointment.Application.Features.Appointment.Queries.GetAppointmentsByTeamAndDate;
using EventBus.Messages.Events;
using MassTransit;
using MediatR;

namespace Appointment.Application.EventBusConsumers
{
    public class AppointmentConsumer : IConsumer<AppointmentRequestEvent>
    {
        private readonly IMediator _mediator;

        public AppointmentConsumer(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Consume(ConsumeContext<AppointmentRequestEvent> context)
        {
            var appointments = await _mediator.Send(new GetAppointmentsByTeamAndDateQuery(context.Message.TeamId, context.Message.Date));
            var response = new AppointmentResponseBatchEvent(
                appointments.Select(p => new AppointmentResponseEvent
                {
                    StartDateTime = p.StartDateTime,
                    Duration = p.Duration,
                    TeamId = p.TeamId
                }).ToList()
            );

            await context.RespondAsync(response);
        }
    }
}
