using MicroserviceBasedFintechApp.OrderService.Core.Abstractions.Services;
using MicroserviceBasedFintechApp.OrderService.Core.Contracts.Entities;
using MicroserviceBasedFintechApp.OrderService.Persistence.Abstraction;
using MicroserviceBasedFintechApp.OrderService.Persistence.Contracts.Configs;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace MicroserviceBasedFintechApp.OrderService.Api.Consumer
{
    public class AuthenticatedOrdersConsumer : BackgroundService
    {
        private readonly IRabbitMqInfrastructureWrapper _rabbitMqInfrastructureWrapper;
        private readonly RabbitMqConfig _rabbitConfig;
        private readonly IOrderService _orderService;
        private readonly IServiceScope _serviceScope;
        private readonly IModel _channel;
        private readonly ILogger<AuthenticatedOrdersConsumer> _logger;
        public AuthenticatedOrdersConsumer(
            IRabbitMqInfrastructureWrapper rabbitInfrastructure,
            IOptions<RabbitMqConfig> rabbitConfigs,
            IServiceProvider serviceProvider,
            ILogger<AuthenticatedOrdersConsumer> logger)
        {
            _logger = logger;
            _serviceScope = serviceProvider.CreateScope();
            _orderService = _serviceScope.ServiceProvider.GetRequiredService<IOrderService>();
            _rabbitMqInfrastructureWrapper = rabbitInfrastructure;
            _rabbitConfig = rabbitConfigs.Value;
            _channel = rabbitInfrastructure.CreateChannel();
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Yield();
            InitializeInfrastructure();

            AsyncEventingBasicConsumer consumer = new AsyncEventingBasicConsumer(_channel);
            consumer.Received += async (sender, @event) =>//Idempotency is saved here
            {
                Order order = JsonSerializer.Deserialize<Order>(@event.Body.ToArray());
                if(order == null)
                {
                    _logger.LogError($"cannot be deserialized: {Encoding.UTF8.GetString(@event.Body.ToArray())}");
                }
                else
                {
                    _orderService.UpdateCompanyId(order);
                }
                
                
                _channel.BasicAck(@event.DeliveryTag, false);
            };

            _channel.BasicConsume(_rabbitConfig.AuthenticationResponseQueue, false, consumer);
            
            stoppingToken.WaitHandle.WaitOne();
        }

        private void InitializeInfrastructure()
        {
            _channel.ExchangeDeclare(
                exchange: _rabbitConfig.AuthenticationExchange,
                type: ExchangeType.Direct,
                durable: true,
                autoDelete: false,
                arguments: null);

            _channel.QueueDeclare(
                queue: _rabbitConfig.AuthenticationResponseQueue,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            _channel.QueueDeclare(
                queue: _rabbitConfig.AuthenticationRequestQueue,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            _channel.QueueBind(_rabbitConfig.AuthenticationRequestQueue, _rabbitConfig.AuthenticationExchange, _rabbitConfig.AuthenticationRequestQueueRoutingKey);
            _channel.QueueBind(_rabbitConfig.AuthenticationResponseQueue, _rabbitConfig.AuthenticationExchange, _rabbitConfig.AuthenticationResponseQueueRoutingKey);
        }

        public override void Dispose()
        {
            _channel?.Dispose();
            _serviceScope?.Dispose();
            base.Dispose();
        }
    }
}
