using MediatR;
using MicroserviceBasedFintechApp.OrderService.Core.Abstractions.Repository;
using MicroserviceBasedFintechApp.OrderService.Core.Contracts.DomainEvents.Base;
using MicroserviceBasedFintechApp.OrderService.Core.Contracts.Entities.Base;
using MicroserviceBasedFintechApp.OrderService.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;


namespace MicroserviceBasedFintechApp.OrderService.Persistence.Implementations
{
    public class GenericRepository<T> : IGenericRepository<T>
        where T : BaseEntity
    {
        private readonly List<DomainEvent> _domainEvents;
        private readonly OrderDbContext _identityDbContext;
        private readonly IMediator _mediator;
        public GenericRepository(
            OrderDbContext identityDbContext,
            IMediator mediator)
        {
            _identityDbContext = identityDbContext;
            _domainEvents = new List<DomainEvent>();
            _mediator = mediator;
        }

        public void AddDomainEvent(DomainEvent @event)
        {
            _domainEvents.Add(@event);
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
            foreach(var @event in _domainEvents)
            {
                _mediator.Publish(@event);
            }
        }

        public async Task SaveChangesAsync()
        {
            await _identityDbContext.SaveChangesAsync();

            foreach (var @event in _domainEvents)
            {
                _mediator.Publish(@event);
            }
        }

        public void Update(T entity)
        {
            _identityDbContext.Set<T>().Update(entity);
        }
    }
}
