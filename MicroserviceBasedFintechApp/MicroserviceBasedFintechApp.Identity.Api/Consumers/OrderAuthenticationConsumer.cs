using MicroserviceBasedFintechApp.Identity.Core.Abstractions.Service;
using MicroserviceBasedFintechApp.Identity.Core.Contracts.Models;
using MicroserviceBasedFintechApp.Identity.Core.Contracts.Requests;
using MicroserviceBasedFintechApp.Identity.Core.Contracts.Responses;
using MicroserviceBasedFintechApp.Identity.Persistence.Contracts.Abstractions;
using MicroserviceBasedFintechApp.Identity.Persistence.Contracts.Configs;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace MicroserviceBasedFintechApp.Identity.Api.Consumers
{
    public class OrderAuthenticationConsumer : BackgroundService
    {
        private readonly IModel _channel;
        private readonly RabbitMqConfig _rabbitConfigs;
        private readonly IServiceScope _scopedProvider;
        private readonly ICompanyService _companyService;
        private readonly IBasicProperties _durableProperties;
        public OrderAuthenticationConsumer(
            IRabbitInfrastructureWrapper rabbit, 
            IOptions<RabbitMqConfig> rabbitConfigOptions,
            IServiceProvider provider)
        {
            _channel = rabbit.CreateChannel();
            _channel.BasicQos(0, 1, false);
            _rabbitConfigs = rabbitConfigOptions.Value;
            _scopedProvider = provider.CreateScope();
            _durableProperties = _channel.CreateBasicProperties();
            _durableProperties.DeliveryMode = 2;

            _companyService = _scopedProvider.ServiceProvider.GetRequiredService<ICompanyService>();
        }
        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Yield();
            InitializeInfrastructure();
            AsyncEventingBasicConsumer consumer = new AsyncEventingBasicConsumer(_channel);
            consumer.Received += async (sender, @event) =>
            {
                Order order = JsonSerializer.Deserialize<Order>(@event.Body.ToArray());

                AuthenticateCompanyResponse response = await _companyService.IsAuthenticatedCompany(new AuthenticateCompanyRequest() { ApiKey = order.ApiKey, HashedSecret = order.SecretHashed });
                if (response.CompanyId != -1)
                {
                    order.Authenticated = true;
                    order.CompanyId = response.CompanyId;
                    
                    _channel.BasicPublish(
                        exchange: _rabbitConfigs.AuthenticationExchange, 
                        routingKey: _rabbitConfigs.AuthenticationResponseQueueRoutingKey,
                        basicProperties: _durableProperties, 
                        body: Encoding.UTF8.GetBytes(JsonSerializer.Serialize(order)));
                }

                _channel.BasicAck(@event.DeliveryTag, false);
            };

            _channel.BasicConsume(_rabbitConfigs.AuthenticationRequestQueue,false,consumer);
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
                queue: _rabbitConfigs.AuthenticationResponseQueue,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            _channel.QueueDeclare(
                queue: _rabbitConfigs.AuthenticationRequestQueue,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            _channel.QueueDeclare(
                queue: _rabbitConfigs.PaymentQueue,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);


            _channel.QueueBind(_rabbitConfigs.AuthenticationRequestQueue, _rabbitConfigs.AuthenticationExchange, _rabbitConfigs.AuthenticationRequestQueueRoutingKey);
            _channel.QueueBind(_rabbitConfigs.AuthenticationResponseQueue, _rabbitConfigs.AuthenticationExchange, _rabbitConfigs.AuthenticationResponseQueueRoutingKey);
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
