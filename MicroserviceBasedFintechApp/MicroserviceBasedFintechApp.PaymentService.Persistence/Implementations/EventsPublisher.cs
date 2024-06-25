using MicroserviceBasedFintechApp.PaymentService.Core.Abstractions.Infrastructure;
using MicroserviceBasedFintechApp.PaymentService.Core.Contracts.Entities;
using MicroserviceBasedFintechApp.PaymentService.Persistence.Abstraction;
using MicroserviceBasedFintechApp.PaymentService.Persistence.Contacts.Configs;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace MicroserviceBasedFintechApp.PaymentService.Persistence.Implementations
{
    public class EventsPublisher : IEventsPublisher, IDisposable
    {
        private readonly IRabbitMqInfrastructureWrapper _rabbitWrapper;
        private readonly IModel _channel;
        private readonly object _channelLock;
        private readonly RabbitMqConfig _rabbitMqConfig;
        private readonly IBasicProperties _durableProperties;
        public EventsPublisher(
            IRabbitMqInfrastructureWrapper rabbitInfrastructure,
            IOptions<RabbitMqConfig> rabbitConfig)
        {
            _channelLock = new object();
            _rabbitMqConfig = rabbitConfig.Value;
            _rabbitWrapper = rabbitInfrastructure;
            _channel = rabbitInfrastructure.CreateChannel();
            _durableProperties = _channel.CreateBasicProperties();
            _durableProperties.DeliveryMode = 2;//Durable messages
            InitializeInfrastructure();
        }

        public void Dispose()
        {
            _channel?.Dispose();
        }

        public void PublishOrderStatus(PaymentOrder order)
        {
            lock( _channelLock )
            {
                _channel.BasicPublish(_rabbitMqConfig.AuthenticationExchange, _rabbitMqConfig.OrderStatusQueueRoutingKey, _durableProperties, Encoding.UTF8.GetBytes(JsonSerializer.Serialize(order)));
            }
        }

        private void InitializeInfrastructure()
        {
            lock(_channelLock )
            {
                _channel.QueueDeclare(
                    queue: _rabbitMqConfig.OrderStatusQueue,
                    exclusive: false,
                    autoDelete: false,
                    durable: true,
                    arguments: null);

                _channel.ExchangeDeclare(
                    exchange: _rabbitMqConfig.AuthenticationExchange,
                    type: ExchangeType.Direct,
                    durable: true,
                    autoDelete: false,
                    arguments: null);

                _channel.QueueBind(_rabbitMqConfig.OrderStatusQueue, _rabbitMqConfig.AuthenticationExchange, _rabbitMqConfig.OrderStatusQueueRoutingKey);
            }
        }
    }
}
