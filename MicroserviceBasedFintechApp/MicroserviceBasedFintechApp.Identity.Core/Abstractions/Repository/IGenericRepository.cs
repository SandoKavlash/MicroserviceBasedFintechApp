using MicroserviceBasedFintechApp.Identity.Core.Contracts.Entities.Base;

namespace MicroserviceBasedFintechApp.Identity.Core.Abstractions.Repository
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<List<T>> GetAllAsync();
        List<T> GetAll();

        /// <returns>Returns null if not found</returns>
        T? GetById(int id);

        /// <returns>Returns null if not found</returns>
        Task<T?> GetByIdAsync(int id);

        IQueryable<T> GetQueryable();

        void Insert(T entity);
        Task InsertAsync(T entity);

        void Update(T entity);
        void Delete(T entity);

        Task SaveChangesAsync();
        void SaveChanges(); 
    }
}
