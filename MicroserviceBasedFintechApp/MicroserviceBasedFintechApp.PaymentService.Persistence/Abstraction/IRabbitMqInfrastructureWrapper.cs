using RabbitMQ.Client;

namespace MicroserviceBasedFintechApp.PaymentService.Persistence.Abstraction
{
    public interface IRabbitMqInfrastructureWrapper
    {
        object ConnectionLock { get; }
        IConnection Connection { get; }
        IModel CreateChannel();
    }
}
