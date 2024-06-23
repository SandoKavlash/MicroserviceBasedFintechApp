using MicroserviceBasedFintechApp.OrderService.Core.Contracts.Entities;

namespace MicroserviceBasedFintechApp.OrderService.Core.Abstractions.Infrastructure
{
    public interface IAsyncServiceNotifier
    {
        void SendToIdentityService(Order order);
    }
}
