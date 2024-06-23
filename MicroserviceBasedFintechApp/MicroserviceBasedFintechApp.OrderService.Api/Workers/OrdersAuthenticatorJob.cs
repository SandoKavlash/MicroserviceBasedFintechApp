
using MicroserviceBasedFintechApp.OrderService.Core.Abstractions.Services;

namespace MicroserviceBasedFintechApp.OrderService.Api.Workers
{
    public class OrdersAuthenticatorJob : BackgroundService
    {
        private readonly ILogger<OrdersAuthenticatorJob> _logger;
        private readonly IOrderService _orderService;
        public OrdersAuthenticatorJob(
            ILogger<OrdersAuthenticatorJob> logger,
            IServiceProvider serviceProvider)
        {
            _logger = logger;
            IServiceProvider scopedServiceProvider = serviceProvider.CreateScope().ServiceProvider;
            _orderService = scopedServiceProvider.GetRequiredService<IOrderService>();
        }
        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while(!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await _orderService.SendEventsForAuthentication();
                }catch(Exception ex)
                {
                    _logger.LogError(ex, "Error occured in OrdersAuthenticatorJob");
                }
                finally
                {
                    await Task.Delay(1000);
                }
            }
        }
    }
}
