using MicroserviceBasedFintechApp.PaymentService.Core.Contracts.Entities;
using MicroserviceBasedFintechApp.PaymentService.Core.Contracts.Models;

namespace MicroserviceBasedFintechApp.PaymentService.Core.Abstractions.Services
{
    public interface IPaymentService
    {
        Task AddOrder(PaymentOrder order);

        Task SendStatusNotifications();

        Task<PaymentResponse> Pay(PaymentModel paymentModel);
    }
}
