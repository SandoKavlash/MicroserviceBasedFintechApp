using RabbitMQ.Client;

namespace MicroserviceBasedFintechApp.Identity.Persistence.Contracts.Abstractions
{
    public interface IRabbitInfrastructureWrapper
    {
        object ConnectionLock { get; }
        IConnection Connection { get; }
        IModel CreateChannel();
    }
}
