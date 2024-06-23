
namespace MicroserviceBasedFintechApp.Identity.Api.Consumers
{
    public class OrderAuthenticationConsumer : BackgroundService
    {
        public OrderAuthenticationConsumer()
        {

        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {

            stoppingToken.WaitHandle.WaitOne();
        }
    }
}
