using MicroserviceBasedFintechApp.PaymentService.Core.Contracts.Models;



namespace MicroserviceBasedFintechApp.PaymentService.Core.Abstractions.Services
{
    public interface IPaymentDispatcherService
    {
        Action<PaymentModel> DispatchByCardNumber(string cardNumber);
    }
}
