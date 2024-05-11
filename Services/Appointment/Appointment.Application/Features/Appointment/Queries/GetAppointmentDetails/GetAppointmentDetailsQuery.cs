using Appointment.Application.Dtos;
using MediatR;

namespace Appointment.Application.Features.Appointment.Queries.GetAppointmentDetails
{
    public record GetAppointmentDetailsQuery(Guid Id) : IRequest<AppointmentDto>;
}
