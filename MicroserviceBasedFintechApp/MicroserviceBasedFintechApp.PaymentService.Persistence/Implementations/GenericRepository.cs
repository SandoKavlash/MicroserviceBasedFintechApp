using MicroserviceBasedFintechApp.PaymentService.Core.Abstractions.Repository;
using MicroserviceBasedFintechApp.PaymentService.Core.Contracts.Entities.Base;
using MicroserviceBasedFintechApp.PaymentService.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace MicroserviceBasedFintechApp.PaymentService.Persistence.Implementations
{
    public class GenericRepository<T> : IGenericRepository<T>
        where T : BaseEntity
    {
        private readonly PaymentServiceDbContext _identityDbContext;
        public GenericRepository(
            PaymentServiceDbContext identityDbContext)
        {
            _identityDbContext = identityDbContext;
        }

        public void Delete(T entity)
        {
            _identityDbContext.Set<T>().Remove(entity);
        }

        public List<T> GetAll()
        {
            return _identityDbContext.Set<T>().ToList();
        }

        public Task<List<T>> GetAllAsync()
        {
            return _identityDbContext.Set<T>().ToListAsync();
        }

        public T? GetById(int id)
        {
            return _identityDbContext.Set<T>().Find(id);
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _identityDbContext.Set<T>().FindAsync(id);
        }

        public IQueryable<T> GetQueryable()
        {
            return _identityDbContext.Set<T>();
        }

        public void Insert(T entity)
        {
            _identityDbContext.Set<T>().Add(entity);
        }

        public async Task InsertAsync(T entity)
        {
            await _identityDbContext.Set<T>().AddAsync(entity);
        }

        public void SaveChanges()
        {
            _identityDbContext.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _identityDbContext.SaveChangesAsync();
        }

        public void Update(T entity)
        {
            _identityDbContext.Set<T>().Update(entity);
        }

    }
}