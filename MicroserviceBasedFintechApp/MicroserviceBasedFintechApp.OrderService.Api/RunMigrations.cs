
using MicroserviceBasedFintechApp.OrderService.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace MicroserviceBasedFintechApp.OrderService.Api
{
    public class RunMigrations : BackgroundService
    {
        public RunMigrations(IServiceProvider provider)
        {
            using IServiceScope scoped = provider.CreateScope();
            OrderDbContext context = scoped.ServiceProvider.GetService<OrderDbContext>();
            context.Database.Migrate();
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.CompletedTask;
        }
    }
}
