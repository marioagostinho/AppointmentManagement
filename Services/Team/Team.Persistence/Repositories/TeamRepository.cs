using Entities = Team.Domain.Entities;
using Team.Domain.Repositories;
using Team.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace Team.Persistence.Repositories
{
    public class TeamRepository : BaseRepository<Entities.Team>, ITeamRepository
    {
        public TeamRepository(TeamDatabaseContext context) : base(context)
        {

        }

        public async override Task<Entities.Team> GetByIdAsync(Guid id, bool track = false, bool include = false)
        {
            IQueryable<Entities.Team> query = _context.Set<Entities.Team>();

            if (include)
            { 
                query = query.Include(p => p.TeamOpeningHours)
                    .ThenInclude(toh => toh.OpeningHours);
            }

            if (track == false)
            {
                query = query.AsNoTracking();
            }

            return await query.FirstOrDefaultAsync(p => p.Id == id);
        }

        public override async Task<IReadOnlyList<Entities.Team>> GetByIdRangeAsync(HashSet<Guid> ids, bool track = false, bool include = false)
        {
            IQueryable<Entities.Team> query = _context.Set<Entities.Team>()
                .Where(p => ids.Contains(p.Id));

            if (include)
            {
                query = query.Include(p => p.TeamOpeningHours)
                    .ThenInclude(toh => toh.OpeningHours);
            }

            if (track == false)
            {
                query = query.AsNoTracking();
            }

            return await query.ToListAsync();
        }

        public async override Task<IReadOnlyList<Entities.Team>> GetAllAsync(bool track = false, bool include = false)
        {
            IQueryable<Entities.Team> query = _context.Set<Entities.Team>();

            if (include)
            {
                query = query.Include(p => p.TeamOpeningHours)
                    .ThenInclude(toh => toh.OpeningHours);
            }

            if (track == false)
            {
                query = query.AsNoTracking();
            }

            return await query.ToListAsync();
        }

        public override async Task<bool> UpdateAsync(Entities.Team entity)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    _context.Entry(entity).State = EntityState.Modified;

                    // Add and Remove TeamOpeningHours
                    var actualInDb = await _context.Set<Entities.TeamOpeningHours>()
                        .Where(p => p.TeamId == entity.Id)
                        .AsNoTracking()
                        .ToListAsync();

                    var entriesToAdd = entity.TeamOpeningHours
                        .Where(actSlot => !actualInDb.Any(dbSlot => dbSlot.OpeningHoursId == actSlot.OpeningHoursId))
                        .ToList();

                    var entriesToRemove = actualInDb
                        .Where(dbSlot => !entity.TeamOpeningHours.Any(actSlot => actSlot.OpeningHoursId == dbSlot.OpeningHoursId))
                        .ToList();

                    _context.Set<Entities.TeamOpeningHours>().AddRange(entriesToAdd);
                    _context.Set<Entities.TeamOpeningHours>().RemoveRange(entriesToRemove);

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
