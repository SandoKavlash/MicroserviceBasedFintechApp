using MicroserviceBasedFintechApp.PaymentService.Core.Abstractions.Services;
using MicroserviceBasedFintechApp.PaymentService.Core.Contracts.Models;
using System.Text.Json;

namespace MicroserviceBasedFintechApp.PaymentService.Core.Implementations
{
    public class DispatcherService : IPaymentDispatcherService
    {
        public Action<PaymentModel> DispatchByCardNumber(string cardNumber)
        {
            if ((cardNumber[cardNumber.Length - 1] - '0') % 2 == 1)//Last digit is odd
            {
                return (paymentModel) => { Console.WriteLine($"Processed by ServiceA => {JsonSerializer.Serialize(paymentModel)}"); };
            }else
            {
                return (paymentModel) => { Console.WriteLine($"Processed by ServiceB => {JsonSerializer.Serialize(paymentModel)}"); };
            }
        }
    }
}
