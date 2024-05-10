using Microsoft.EntityFrameworkCore;
using Team.Domain.Entities;
using Team.Domain.Repositories;
using Team.Persistence.DatabaseContext;

namespace Team.Persistence.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly TeamDatabaseContext _context;

        public BaseRepository(TeamDatabaseContext context)
        {
            _context = context;
        }

        public virtual async Task<T> GetByIdAsync(Guid id, bool track = false, bool include = false)
        {
            IQueryable<T> query = _context.Set<T>();

            if (track == false)
            {
                query = query.AsNoTracking();
            }

            return await query.FirstOrDefaultAsync(e => e.Id == id);
        }

        public virtual async Task<IReadOnlyList<T>> GetByIdRangeAsync(HashSet<Guid> ids, bool track = false, bool include = false)
        {
            IQueryable<T> query = _context.Set<T>()
                .Where(p => ids.Contains(p.Id));

            if (track == false)
            {
                query = query.AsNoTracking();
            }
                
            return await query.ToListAsync();
        }

        public virtual async Task<IReadOnlyList<T>> GetAllAsync(bool track = false, bool include = false)
        {

            IQueryable<T> query = _context.Set<T>();

            if (track == false)
            {
                query = query.AsNoTracking();
            }

            return await query.ToListAsync();
        }

        public async Task<Guid> CreateAsync(T entity)
        {
            var entry = await _context.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entry.Entity.Id;
        }

        public virtual async Task<bool> UpdateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            var result = await _context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            _context.Remove(entity);
            var result = await _context.SaveChangesAsync();

            return result > 0; 
        }
    }
}
