using ScriptEase.OnlinePharmacy.Data.Interfaces.Repositories;

namespace ScriptEase.OnlinePharmacy.Data.Repositories
{
    public abstract class AbstractBaseRepository<T> : IBaseRepository<T> where T : class
    {
        public abstract Task<IEnumerable<T>> GetAllAsync();
        public abstract Task<T> GetByIdAsync(int id);
        public abstract Task CreateAsync(T entity);
        public abstract Task UpdateAsync(T entity);
        public abstract Task DeleteAsync(int id);
    }
}
