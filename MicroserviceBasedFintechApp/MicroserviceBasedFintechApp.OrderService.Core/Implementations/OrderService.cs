using MicroserviceBasedFintechApp.OrderService.Core.Abstractions.Infrastructure;
using MicroserviceBasedFintechApp.OrderService.Core.Abstractions.Repository;
using MicroserviceBasedFintechApp.OrderService.Core.Abstractions.Services;
using MicroserviceBasedFintechApp.OrderService.Core.Contracts.DomainEvents;
using MicroserviceBasedFintechApp.OrderService.Core.Contracts.Entities;
using MicroserviceBasedFintechApp.OrderService.Core.Contracts.Requests;
using MicroserviceBasedFintechApp.OrderService.Core.Contracts.Responses;
using Microsoft.EntityFrameworkCore;

namespace MicroserviceBasedFintechApp.OrderService.Core.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IGenericRepository<Order> _orderRepository;
        private readonly IHashService _hashService;
        private readonly IAsyncServiceNotifier _asyncServiceNotifier;
        public OrderService(
            IGenericRepository<Order> orderRepo,
            IHashService hashService,
            IAsyncServiceNotifier asyncServiceNotifier)
        {
            _orderRepository = orderRepo;
            _hashService = hashService;
            _asyncServiceNotifier = asyncServiceNotifier;
        }
        public async Task<OrderCreateResponse> CreateOrder(OrderCreateRequest request)
        {
            Order? findOrderLikeRequest = await _orderRepository
                .GetQueryable()
                .Where(order => order.ApiKey == request.ApiKey && order.IdempotencyKey == request.IdempotencyKey)
                .SingleOrDefaultAsync();

            if (findOrderLikeRequest != null) return new OrderCreateResponse() { OrderId = -1 };


            Order order = new Order()
            {
                ApiKey = request.ApiKey,
                SecretHashed = _hashService.Hash(request.Secret.ToString()),
                Amount = request.Amount,
                Currency = request.Currency,
                IdempotencyKey = request.IdempotencyKey,
                UpdateDateAtUtc = DateTime.UtcNow,
                CreationDateAtUtc = DateTime.UtcNow,
            };

            await _orderRepository.InsertAsync(order);
            _orderRepository.AddDomainEvent(new OrderCreatedDomainEvent(order.ApiKey, order.SecretHashed, order.Amount, order.Currency, order.IdempotencyKey));

            await _orderRepository.SaveChangesAsync();

            return new OrderCreateResponse()
            {
                OrderId = order.Id
            };
        }

        public Task SendEventsForAuthentication()
        {
            List<Order> unAuthedOrders = _orderRepository.GetQueryable()
                        .Where(order => order.Authenticated == null)
                        .Take(100)
                        .ToList();

            foreach (var order in unAuthedOrders)
            {
                _asyncServiceNotifier.SendToIdentityService(order);
                order.Authenticated = false;
            }

            return _orderRepository.SaveChangesAsync();
        }

        public void UpdateCompanyId(Order order)
        {
            Order orderRetrieved = _orderRepository.GetQueryable().Single(o => o.ApiKey == order.ApiKey && o.IdempotencyKey == order.IdempotencyKey);
            orderRetrieved.Authenticated = true;
            orderRetrieved.UpdateDateAtUtc = DateTime.UtcNow;
            orderRetrieved.CompanyId = order.CompanyId;
            _orderRepository.SaveChanges();
        }

        public void UpdateCompanyStatus(Order order)
        {
            Order orderRetrieved = _orderRepository.GetQueryable().Single(o => o.ApiKey == order.ApiKey && o.IdempotencyKey == order.IdempotencyKey);
            orderRetrieved.Status = order.Status;
            _orderRepository.SaveChanges();
        }
    }
}
