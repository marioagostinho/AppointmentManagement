namespace Appointment.Application.Dtos
{
    public record AppointmentDto(Guid Id, DateTime StartDateTime, TimeSpan Duration, Guid TeamId);
}
