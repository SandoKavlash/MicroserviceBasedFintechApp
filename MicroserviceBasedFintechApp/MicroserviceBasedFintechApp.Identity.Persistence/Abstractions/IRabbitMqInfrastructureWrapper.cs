using RabbitMQ.Client;

namespace MicroserviceBasedFintechApp.Identity.Persistence.Abstractions
{
    public interface IRabbitMqInfrastructureWrapper
    {
        object ConnectionLock { get; }
        IConnection Connection { get; }
        IModel CreateChannel();
    }
}
