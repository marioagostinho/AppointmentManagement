using Team.Domain.Entities;
using Team.Domain.Repositories;
using Team.Persistence.DatabaseContext;

namespace Team.Persistence.Repositories
{
    public class OpeningTimeSlotRepository : BaseRepository<OpeningTimeSlot>, IOpeningTimeSlotRepository
    {
        public OpeningTimeSlotRepository(TeamDatabaseContext context) : base(context)
        {
        }
    }
}
