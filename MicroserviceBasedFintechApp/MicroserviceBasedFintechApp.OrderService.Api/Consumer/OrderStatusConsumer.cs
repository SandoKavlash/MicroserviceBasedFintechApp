using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Threading.Channels;
using MicroserviceBasedFintechApp.OrderService.Persistence.Contracts.Configs;
using MicroserviceBasedFintechApp.OrderService.Persistence.Abstraction;
using MicroserviceBasedFintechApp.OrderService.Core.Abstractions.Services;
using Microsoft.Extensions.Options;
using MicroserviceBasedFintechApp.OrderService.Core.Contracts.Entities;
using System.Text;
using System.Text.Json;

namespace MicroserviceBasedFintechApp.OrderService.Api.Consumer
{
    public class OrderStatusConsumer : BackgroundService
    {
        private readonly IRabbitMqInfrastructureWrapper _rabbitMqInfrastructureWrapper;
        private readonly RabbitMqConfig _rabbitConfig;
        private readonly IOrderService _orderService;
        private readonly IServiceScope _serviceScope;
        private readonly IModel _channel;
        private readonly object _channelLock;
        private readonly ILogger<OrderStatusConsumer> _logger;
        public OrderStatusConsumer(
            IRabbitMqInfrastructureWrapper rabbitInfrastructure,
            IOptions<RabbitMqConfig> rabbitConfigs,
            IServiceProvider serviceProvider,
            ILogger<OrderStatusConsumer> logger)
        {
            _channelLock = new object();
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
                if (order == null)
                {
                    _logger.LogError($"cannot be deserialized: {Encoding.UTF8.GetString(@event.Body.ToArray())}");
                }
                else
                {
                    _orderService.UpdateCompanyStatus(order);
                }


                _channel.BasicAck(@event.DeliveryTag, false);
            };

            _channel.BasicConsume(_rabbitConfig.OrderStatusQueue, false, consumer);

            stoppingToken.WaitHandle.WaitOne();
        }

        private void InitializeInfrastructure()
        {
            lock (_channelLock)
            {
                _channel.QueueDeclare(
                    queue: _rabbitConfig.OrderStatusQueue,
                    exclusive: false,
                    autoDelete: false,
                    durable: true,
                    arguments: null);

                _channel.ExchangeDeclare(
                    exchange: _rabbitConfig.AuthenticationExchange,
                    type: ExchangeType.Direct,
                    durable: true,
                    autoDelete: false,
                    arguments: null);

                _channel.QueueBind(_rabbitConfig.OrderStatusQueue, _rabbitConfig.AuthenticationExchange, _rabbitConfig.OrderStatusQueueRoutingKey);
            }
        }

        public override void Dispose()
        {
            _channel?.Dispose();
            _serviceScope?.Dispose();
            base.Dispose();
        }
    }
}
