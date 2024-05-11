using Entities = Appointment.Domain.Entities;

namespace Appointment.Domain.Repositories
{
    public interface IAppointmentRepository : IBaseRepository<Entities.Appointment>
    {
    }
}
