using MicroserviceBasedFintechApp.PaymentService.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace MicroserviceBasedFintechApp.PaymentService.Api
{
    public class RunMigrations : BackgroundService
    {
        public RunMigrations(IServiceProvider provider)
        {
            using IServiceScope scoped = provider.CreateScope();
            PaymentServiceDbContext context = scoped.ServiceProvider.GetService<PaymentServiceDbContext>();
            context.Database.Migrate();
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.CompletedTask;
        }
    }
}
