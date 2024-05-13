using Team.Domain.Entities;
using Team.Domain.Enums;

namespace Team.Domain.Repositories
{
    public interface IOpeningTimeSlotRepository : IBaseRepository<OpeningTimeSlot>
    {
        public Task<IList<OpeningTimeSlot>> GetByTeamIdAndDayAsync(Guid teamId, EDayOfWeek dayOfWeek);
    }
}
