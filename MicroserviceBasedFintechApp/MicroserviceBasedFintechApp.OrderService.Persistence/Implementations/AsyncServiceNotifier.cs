using MicroserviceBasedFintechApp.OrderService.Core.Abstractions.Infrastructure;
using MicroserviceBasedFintechApp.OrderService.Core.Contracts.Entities;
using MicroserviceBasedFintechApp.OrderService.Persistence.Abstraction;
using MicroserviceBasedFintechApp.OrderService.Persistence.Contracts.Configs;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace MicroserviceBasedFintechApp.OrderService.Persistence.Implementations
{
    public class AsyncServiceNotifier : IAsyncServiceNotifier, IDisposable
    {
        private readonly RabbitMqConfig _rabbitConfigs;
        private readonly IRabbitMqInfrastructureWrapper _rabbitInfraWrapper;
        private IModel _channel;
        private IBasicProperties _durableProperties;
        public AsyncServiceNotifier(
            IOptions<RabbitMqConfig> rabbitConfigOptions,
            IRabbitMqInfrastructureWrapper rabbitInfraWrapper)
        {
            _rabbitInfraWrapper = rabbitInfraWrapper;
            _rabbitConfigs = rabbitConfigOptions.Value;
            InitInfrastructure();
        }

        private void InitInfrastructure()
        {
            _channel = _rabbitInfraWrapper.CreateChannel();

            _channel.ExchangeDeclare(
                exchange:_rabbitConfigs.AuthenticationExchange, 
                type:ExchangeType.Fanout, 
                durable:true, 
                autoDelete: false,
                arguments: null);

            _channel.QueueDeclare(
                queue: _rabbitConfigs.AuthenticationResponseQueue,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            _channel.QueueBind(_rabbitConfigs.AuthenticationResponseQueue, _rabbitConfigs.AuthenticationExchange, "");

            _durableProperties = _channel.CreateBasicProperties();
            _durableProperties.DeliveryMode = 2;//Persistence = true
        }

        public void SendToIdentityService(Order order)
        {
            lock (this)
            {
                _channel.BasicPublish(
                exchange: _rabbitConfigs.AuthenticationExchange,
                routingKey: "",
                basicProperties: _durableProperties,
                body: Encoding.UTF8.GetBytes(JsonSerializer.Serialize(order)));
            }
        }


        public void Dispose()
        {
            _channel?.Dispose(); 
            _rabbitInfraWrapper?.Dispose();
        }


    }
}
