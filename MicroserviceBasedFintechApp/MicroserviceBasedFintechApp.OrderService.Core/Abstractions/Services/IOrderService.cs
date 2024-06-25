using MicroserviceBasedFintechApp.OrderService.Core.Contracts.Entities;
using MicroserviceBasedFintechApp.OrderService.Core.Contracts.Requests;
using MicroserviceBasedFintechApp.OrderService.Core.Contracts.Responses;

namespace MicroserviceBasedFintechApp.OrderService.Core.Abstractions.Services
{
    public interface IOrderService
    {
        /// <summary>
        ///     Returns OrderId: -1 if order already exists.
        /// </summary>
        Task<OrderCreateResponse> CreateOrder(OrderCreateRequest request);

        Task SendEventsForAuthentication();

        void UpdateCompanyId(Order order);
        void UpdateCompanyStatus(Order order);
    }
}
