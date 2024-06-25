using MicroserviceBasedFintechApp.OrderService.Core.Abstractions.Services;
using MicroserviceBasedFintechApp.OrderService.Core.Contracts.DomainEvents;
using MicroserviceBasedFintechApp.OrderService.Core.Contracts.Entities;
using MicroserviceBasedFintechApp.OrderService.Core.Contracts.Requests;
using MicroserviceBasedFintechApp.OrderService.Core.Contracts.Responses;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace MicroserviceBasedFintechApp.OrderService.Core.Implementations
{
    public class OrderServiceCacheDecorator : IOrderService
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IOrderService _orderService;
        private readonly ILogger<OrderServiceCacheDecorator> _logger;
        public OrderServiceCacheDecorator(
            IMemoryCache memoryCache, 
            IOrderService orderService,
            ILogger<OrderServiceCacheDecorator> logger)
        {
            _memoryCache = memoryCache;
            _orderService = orderService;
            _logger = logger;
        }
        public Task<OrderCreateResponse> CreateOrder(OrderCreateRequest request)
        {
            try
            {
                OrderCreatedDomainEvent @event;
                if (_memoryCache.TryGetValue<OrderCreatedDomainEvent>($"{request.ApiKey}:{request.IdempotencyKey}", out @event))
                {
                    return Task.FromResult(new OrderCreateResponse()
                    {
                        OrderId = -1
                    });
                }
                return _orderService.CreateOrder(request);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex,"Error occured in CacheDecoratorOrderService");
                return _orderService.CreateOrder(request);
            }
        }

        public Task SendEventsForAuthentication()
        {
            return _orderService.SendEventsForAuthentication();
        }

        public void UpdateCompanyId(Order order)
        {
            _orderService.UpdateCompanyId(order);
        }

        public void UpdateCompanyStatus(Order order)
        {
            _orderService.UpdateCompanyStatus(order);
        }
    }
}
