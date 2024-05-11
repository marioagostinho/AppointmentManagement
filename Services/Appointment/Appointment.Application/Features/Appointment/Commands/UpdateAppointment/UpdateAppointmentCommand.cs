using MediatR;

namespace Appointment.Application.Features.Appointment.Commands.UpdateAppointment
{
    public record UpdateAppointmentCommand(Guid Id,DateTime StartDateTime, TimeSpan Duration, Guid TeamId) : IRequest<bool>;
}
