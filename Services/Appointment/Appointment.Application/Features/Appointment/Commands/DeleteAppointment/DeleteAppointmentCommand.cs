using MediatR;

namespace Appointment.Application.Features.Appointment.Commands.DeleteAppointment
{
    public record DeleteAppointmentCommand(Guid Id) : IRequest<bool>;
}
