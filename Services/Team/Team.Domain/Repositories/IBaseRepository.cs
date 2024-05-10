using System;
using Team.Domain.Entities;

namespace Team.Domain.Repositories
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        public Task<T> GetByIdAsync(Guid id, bool track = false, bool include = false);
        public Task<IReadOnlyList<T>> GetByIdRangeAsync(HashSet<Guid> ids, bool track = false, bool include = false);
        public Task<IReadOnlyList<T>> GetAllAsync(bool track = false, bool include = false);
        public Task<Guid> CreateAsync(T entity);
        public Task<bool> UpdateAsync(T entity);
        public Task<bool> DeleteAsync(T entity);
    }
}
