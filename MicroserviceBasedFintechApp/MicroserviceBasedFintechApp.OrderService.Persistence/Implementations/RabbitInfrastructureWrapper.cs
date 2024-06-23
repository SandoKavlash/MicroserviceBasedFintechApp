using MicroserviceBasedFintechApp.OrderService.Persistence.Abstraction;
using MicroserviceBasedFintechApp.OrderService.Persistence.Contracts.Configs;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.Threading.Channels;

namespace MicroserviceBasedFintechApp.OrderService.Persistence.Implementations
{
    public class RabbitInfrastructureWrapper : IRabbitMqInfrastructureWrapper
    {
        private object _connectionLock;
        public object ConnectionLock { get => _connectionLock; }
        private readonly IConnection _connection;
        public RabbitInfrastructureWrapper(IOptions<RabbitMqConfig> rabbitConfigOptions)
        {
            _connectionLock = new object();
            RabbitMqConfig rabbitConfig = rabbitConfigOptions.Value;
            ConnectionFactory rabbitConnectionFactory = new ConnectionFactory()
            {
                HostName = rabbitConfig.Host,
                Password = rabbitConfig.Password,
                Port = rabbitConfig.Port,
                UserName = rabbitConfig.UserName,
                AutomaticRecoveryEnabled = true,
                NetworkRecoveryInterval = TimeSpan.FromSeconds(1),
            };
            _connection = rabbitConnectionFactory.CreateConnection();
        }
        public IConnection Connection => _connection;

        public IModel CreateChannel()
        {
            lock (_connectionLock)
            {
                return _connection.CreateModel();
            }
        }

        public void Dispose()
        {
            _connection?.Dispose();
        }
    }
}
