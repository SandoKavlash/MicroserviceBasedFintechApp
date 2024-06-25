using MicroserviceBasedFintechApp.PaymentService.Core.Contracts.Entities;

namespace MicroserviceBasedFintechApp.PaymentService.Core.Abstractions.Infrastructure
{
    public interface IEventsPublisher
    {
        void PublishOrderStatus(PaymentOrder order);
    }
}
