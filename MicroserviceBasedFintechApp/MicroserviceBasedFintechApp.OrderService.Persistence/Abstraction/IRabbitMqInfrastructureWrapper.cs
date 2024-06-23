using RabbitMQ.Client;

namespace MicroserviceBasedFintechApp.OrderService.Persistence.Abstraction
{
    public interface IRabbitMqInfrastructureWrapper : IDisposable
    {
        object ConnectionLock { get; }
        IConnection Connection { get; }
        IModel CreateChannel();
    }
}
