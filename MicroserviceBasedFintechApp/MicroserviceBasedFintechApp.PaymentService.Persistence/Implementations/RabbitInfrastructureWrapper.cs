using MicroserviceBasedFintechApp.PaymentService.Persistence.Abstraction;
using MicroserviceBasedFintechApp.PaymentService.Persistence.Contacts.Configs;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;


namespace MicroserviceBasedFintechApp.PaymentService.Persistence.Implementations
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
                DispatchConsumersAsync = true,
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
