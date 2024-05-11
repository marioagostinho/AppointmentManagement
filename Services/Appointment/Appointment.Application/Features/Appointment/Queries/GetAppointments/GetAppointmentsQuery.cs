using Appointment.Application.Dtos;
using MediatR;

namespace Appointment.Application.Features.Appointment.Queries.GetAppointments
{
    public record GetAppointmentsQuery : IRequest<IReadOnlyList<AppointmentDto>>;
}
