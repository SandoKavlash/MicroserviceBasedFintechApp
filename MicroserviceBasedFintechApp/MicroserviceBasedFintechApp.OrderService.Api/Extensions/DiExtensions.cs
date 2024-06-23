using MediatR.NotificationPublishers;
using MicroserviceBasedFintechApp.OrderService.Core;
using MicroserviceBasedFintechApp.OrderService.Core.Abstractions.Infrastructure;
using MicroserviceBasedFintechApp.OrderService.Core.Abstractions.Repository;
using MicroserviceBasedFintechApp.OrderService.Core.Abstractions.Services;
using MicroserviceBasedFintechApp.OrderService.Core.Implementations;
using MicroserviceBasedFintechApp.OrderService.Persistence.Abstraction;
using MicroserviceBasedFintechApp.OrderService.Persistence.Contracts.Configs;
using MicroserviceBasedFintechApp.OrderService.Persistence.DbContexts;
using MicroserviceBasedFintechApp.OrderService.Persistence.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace MicroserviceBasedFintechApp.OrderService.Api.Extensions
{
    public static class DiExtensions
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            services
                .AddScoped<IOrderService, Core.Implementations.OrderService>()
                .Decorate<IOrderService,OrderServiceCacheDecorator>()
                .AddSingleton<IHashService, Sha256HashService>()
                .AddMediatR(options =>
                {
                    options.RegisterServicesFromAssemblyContaining<CoreReference>();
                    options.Lifetime = ServiceLifetime.Singleton;
                    options.NotificationPublisher = new TaskWhenAllPublisher();

                });
            return services;
        }

        public static IServiceCollection AddPersistence(this IServiceCollection services,IConfiguration config)
        {
            services
                .AddDbContext<OrderDbContext>()
                .AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>))
                .AddSingleton<IAsyncServiceNotifier, AsyncServiceNotifier>()
                .AddSingleton<IAsyncServiceNotifier, AsyncServiceNotifier>()
                .AddSingleton<IRabbitMqInfrastructureWrapper,RabbitInfrastructureWrapper>()
                .Configure<RabbitMqConfig>(config.GetSection("RabbitConfig"));

            return services;
        }
    }
}
