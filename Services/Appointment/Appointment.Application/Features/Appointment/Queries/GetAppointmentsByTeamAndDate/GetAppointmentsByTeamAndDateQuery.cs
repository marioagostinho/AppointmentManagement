using Appointment.Application.Dtos;
using MediatR;

namespace Appointment.Application.Features.Appointment.Queries.GetAppointmentsByTeamAndDate
{
    public record GetAppointmentsByTeamAndDateQuery(Guid TeamId, DateTime Date) : IRequest<IReadOnlyList<AppointmentDto>>;
}
