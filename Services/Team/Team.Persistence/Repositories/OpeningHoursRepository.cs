using Microsoft.EntityFrameworkCore;
using System.Linq;
using Team.Domain.Entities;
using Team.Domain.Repositories;
using Team.Persistence.DatabaseContext;

namespace Team.Persistence.Repositories
{
    public class OpeningHoursRepository : BaseRepository<OpeningHours>, IOpeningHoursRepository
    {
        public OpeningHoursRepository(TeamDatabaseContext context) : base(context)
        {
        }

        public async override Task<OpeningHours> GetByIdAsync(Guid id, bool track = false, bool include = false)
        {
            IQueryable<OpeningHours> query = _context.Set<OpeningHours>();

            if (include)
            {
                query = query.Include(p => p.OpeningHoursTimeSlots)
                    .ThenInclude(ohts => ohts.OpeningTimeSlot);
            }  

            if (track == false)
            {
                query = query.AsNoTracking();
            }

            return await query.FirstOrDefaultAsync(p => p.Id == id);
        }

        public override async Task<IReadOnlyList<OpeningHours>> GetByIdRangeAsync(HashSet<Guid> ids, bool track = false, bool include = false)
        {
            IQueryable<OpeningHours> query = _context.Set<OpeningHours>()
                .Where(p => ids.Contains(p.Id));

            if (include)
            {
                query = query.Include(p => p.OpeningHoursTimeSlots)
                    .ThenInclude(ohts => ohts.OpeningTimeSlot);
            }

            if (track == false)
            {
                query = query.AsNoTracking();
            }

            return await query.ToListAsync();
        }

        public async override Task<IReadOnlyList<OpeningHours>> GetAllAsync(bool track = false, bool include = false)
        {
            IQueryable<OpeningHours> query = _context.Set<OpeningHours>();

            if (include)
            {
                query = query.Include(p => p.OpeningHoursTimeSlots)
                    .ThenInclude(ohts => ohts.OpeningTimeSlot);
            }

            if (track == false)
            {
                query = query.AsNoTracking();
            }

            return await query.ToListAsync();
        }

        public override async Task<bool> UpdateAsync(OpeningHours entity)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync()) 
            {
                try
                {
                    _context.Entry(entity).State = EntityState.Modified;

                    // Add and Remove OpeningHoursTimeSlots
                    var actualInDb = await _context.Set<OpeningHoursTimeSlot>()
                        .Where(p => p.OpeningHoursId == entity.Id)
                        .AsNoTracking()
                        .ToListAsync();

                    var entriesToAdd = entity.OpeningHoursTimeSlots
                        .Where(actSlot => !actualInDb.Any(dbSlot => dbSlot.OpeningTimeSlotId == actSlot.OpeningTimeSlotId))
                        .ToList();

                    var entriesToRemove = actualInDb
                        .Where(dbSlot => !entity.OpeningHoursTimeSlots.Any(actSlot => actSlot.OpeningTimeSlotId == dbSlot.OpeningTimeSlotId))
                        .ToList();

                    _context.Set<OpeningHoursTimeSlot>().AddRange(entriesToAdd);
                    _context.Set<OpeningHoursTimeSlot>().RemoveRange(entriesToRemove);

                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();

                    return true;
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }
    }
}
