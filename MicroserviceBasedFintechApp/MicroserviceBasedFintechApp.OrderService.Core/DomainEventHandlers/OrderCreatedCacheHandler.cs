using MediatR;
using MicroserviceBasedFintechApp.OrderService.Core.Contracts.DomainEvents;
using Microsoft.Extensions.Caching.Memory;


namespace MicroserviceBasedFintechApp.OrderService.Core.DomainEventHandlers
{
    public class OrderCreatedCacheHandler : INotificationHandler<OrderCreatedDomainEvent>
    {
        private readonly IMemoryCache _memoryCache;
        public OrderCreatedCacheHandler(IMemoryCache memoryCache)
        {
            Console.WriteLine("object created");
            _memoryCache = memoryCache;
        }
        public Task Handle(OrderCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            _memoryCache.Set<OrderCreatedDomainEvent>($"{notification.ApiKey}:{notification.IdempotencyKey}", notification);

            return Task.CompletedTask;
        }
    }
}
