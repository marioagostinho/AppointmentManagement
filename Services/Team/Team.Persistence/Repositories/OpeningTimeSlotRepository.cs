using Microsoft.EntityFrameworkCore;
using Team.Domain.Entities;
using Team.Domain.Enums;
using Team.Domain.Repositories;
using Team.Persistence.DatabaseContext;

namespace Team.Persistence.Repositories
{
    public class OpeningTimeSlotRepository : BaseRepository<OpeningTimeSlot>, IOpeningTimeSlotRepository
    {
        public OpeningTimeSlotRepository(TeamDatabaseContext context) : base(context)
        {

        }

        public async Task<IList<OpeningTimeSlot>> GetByTeamIdAndDayAsync(Guid teamId, EDayOfWeek dayOfWeek)
        {
            FormattableString query = $@"
                SELECT ots.*
                FROM OpeningTimeSlots ots
                JOIN OpeningHoursTimeSlots ohts ON ots.Id = ohts.OpeningTimeSlotId
                JOIN OpeningHours oh ON ohts.OpeningHoursId = oh.Id
                JOIN TeamOpeningHours toh ON oh.Id = toh.OpeningHoursId
                WHERE toh.TeamId = {teamId} AND oh.DayOfWeek = {(int)dayOfWeek}
                ORDER BY ots.StartHour ASC
            ";

            return await _context.OpeningTimeSlots
                    .FromSqlInterpolated(query)
                    .ToListAsync();
        }
    }
}
