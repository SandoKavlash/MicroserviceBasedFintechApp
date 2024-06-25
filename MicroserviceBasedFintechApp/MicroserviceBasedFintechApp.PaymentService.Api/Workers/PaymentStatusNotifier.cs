
using MicroserviceBasedFintechApp.PaymentService.Core.Abstractions.Services;

namespace MicroserviceBasedFintechApp.PaymentService.Api.Workers
{
    public class PaymentStatusNotifier : BackgroundService
    {
        private readonly ILogger<PaymentStatusNotifier> _logger;
        private readonly IPaymentService _paymentService;
        private readonly IServiceScope _serviceScope;
        public PaymentStatusNotifier(
            ILogger<PaymentStatusNotifier> logger,
            IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceScope = serviceProvider.CreateScope();
            IServiceProvider scopedServiceProvider = serviceProvider.CreateScope().ServiceProvider;
            _paymentService = scopedServiceProvider.GetRequiredService<IPaymentService>();
        }
        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await _paymentService.SendStatusNotifications();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occured in OrdersAuthenticatorJob");
                }
                finally
                {
                    await Task.Delay(1000);
                }
            }
        }

        public override void Dispose()
        {
            _serviceScope?.Dispose();
            base.Dispose();
        }
    }
}
