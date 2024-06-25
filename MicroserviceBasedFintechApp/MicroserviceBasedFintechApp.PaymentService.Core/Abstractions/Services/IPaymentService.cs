using MicroserviceBasedFintechApp.PaymentService.Core.Contracts.Entities;

namespace MicroserviceBasedFintechApp.PaymentService.Core.Abstractions.Services
{
    public interface IPaymentService
    {
        Task AddOrder(PaymentOrder order);

        Task SendStatusNotifications();
    }
}
