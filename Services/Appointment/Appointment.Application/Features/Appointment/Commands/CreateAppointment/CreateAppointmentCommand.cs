using MediatR;

namespace Appointment.Application.Features.Appointment.Commands.CreateAppointment
{
    public record CreateAppointmentCommand(DateTime StartDateTime, TimeSpan Duration, Guid TeamId) : IRequest<Guid>;
}
