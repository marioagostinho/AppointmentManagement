using Appointment.Domain.Repositories;
using Appointment.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Entities = Appointment.Domain.Entities;

namespace Appointment.Persistence.Repositories
{
    public class AppointmentRepository : BaseRepository<Entities.Appointment>, IAppointmentRepository
    {
        public AppointmentRepository(AppointmentDatabaseContext context) : base(context)
        {
        }

        public async Task<IReadOnlyList<Entities.Appointment>> GetByTeamIdAndDateAsync(Guid teamId, DateTime date)
        {
            return await _context.Set<Entities.Appointment>()
                .Where(p => p.TeamId == teamId &&
                    p.StartDateTime.Year == date.Year &&
                    p.StartDateTime.Month == date.Month &&
                    p.StartDateTime.Day == date.Day)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
