using Appointment.Domain.Repositories;
using Appointment.Persistence.DatabaseContext;
using Entities = Appointment.Domain.Entities;

namespace Appointment.Persistence.Repositories
{
    public class AppointmentRepository : BaseRepository<Entities.Appointment>, IAppointmentRepository
    {
        public AppointmentRepository(AppointmentDatabaseContext context) : base(context)
        {
        }
    }
}
