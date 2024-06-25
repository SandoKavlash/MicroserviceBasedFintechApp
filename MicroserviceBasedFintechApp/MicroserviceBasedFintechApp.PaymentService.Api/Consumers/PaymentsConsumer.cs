using MicroserviceBasedFintechApp.PaymentService.Core.Abstractions.Services;
using MicroserviceBasedFintechApp.PaymentService.Core.Contracts.Entities;
using MicroserviceBasedFintechApp.PaymentService.Persistence.Abstraction;
using MicroserviceBasedFintechApp.PaymentService.Persistence.Contacts.Configs;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text.Json;

namespace MicroserviceBasedFintechApp.PaymentService.Api.Consumers
{
    public class PaymentsConsumer : BackgroundService
    {
        private readonly IModel _channel;
        private readonly RabbitMqConfig _rabbitConfigs;
        private readonly IServiceScope _scopedProvider;
        private readonly IPaymentService _paymentService;
        private readonly IBasicProperties _durableProperties;
        public PaymentsConsumer(
            IRabbitMqInfrastructureWrapper rabbit,
            IOptions <RabbitMqConfig> rabbitConfigOptions,
            IServiceProvider provider)
        {
            _channel = rabbit.CreateChannel();
            _channel.BasicQos(0, 1, false);
            _rabbitConfigs = rabbitConfigOptions.Value;
            _scopedProvider = provider.CreateScope();
            _durableProperties = _channel.CreateBasicProperties();
            _durableProperties.DeliveryMode = 2;

            _paymentService = _scopedProvider.ServiceProvider.GetRequiredService<IPaymentService>();
        }
        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Yield();
            InitializeInfrastructure();
            AsyncEventingBasicConsumer consumer = new AsyncEventingBasicConsumer(_channel);
            consumer.Received += async (sender, @event) =>
            {
                PaymentOrder order = JsonSerializer.Deserialize<PaymentOrder>(@event.Body.ToArray());

                await _paymentService.AddOrder(order);

                _channel.BasicAck(@event.DeliveryTag, false);
            };

            _channel.BasicConsume(_rabbitConfigs.PaymentQueue, false, consumer);
            stoppingToken.WaitHandle.WaitOne();
        }
        private void InitializeInfrastructure()
        {
            _channel.ExchangeDeclare(
                exchange: _rabbitConfigs.AuthenticationExchange,
                type: ExchangeType.Direct,
                durable: true,
                autoDelete: false,
                arguments: null);


            _channel.QueueDeclare(
                queue: _rabbitConfigs.PaymentQueue,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            _channel.QueueBind(_rabbitConfigs.PaymentQueue, _rabbitConfigs.AuthenticationExchange, _rabbitConfigs.PaymentQueueRoutingKey);
        }


        public override void Dispose()
        {
            _channel?.Dispose();
            _scopedProvider?.Dispose();
            base.Dispose();
        }
    }
}
