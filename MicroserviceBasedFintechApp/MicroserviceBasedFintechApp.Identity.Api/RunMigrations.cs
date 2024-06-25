using MicroserviceBasedFintechApp.Identity.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace MicroserviceBasedFintechApp.Identity.Api
{
    public class RunMigrations : BackgroundService
    {
        public RunMigrations(IServiceProvider provider)
        {
            using IServiceScope scoped = provider.CreateScope();
            IdentityDbContext context = scoped.ServiceProvider.GetService<IdentityDbContext>();
            context.Database.Migrate();
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.CompletedTask;
        }
    }
}
