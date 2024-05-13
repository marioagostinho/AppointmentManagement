using Entities = Appointment.Domain.Entities;

namespace Appointment.Domain.Repositories
{
    public interface IAppointmentRepository : IBaseRepository<Entities.Appointment>
    {
        public Task<IReadOnlyList<Entities.Appointment>> GetByTeamIdAndDateAsync(Guid teamId, DateTime date);
    }
}
