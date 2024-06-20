using MicroserviceBasedFintechApp.Identity.Core.Abstractions.Repository;
using MicroserviceBasedFintechApp.Identity.Core.Abstractions.Service;
using MicroserviceBasedFintechApp.Identity.Core.Implementations;
using MicroserviceBasedFintechApp.Identity.Persistence.DbContexts;
using MicroserviceBasedFintechApp.Identity.Persistence.Implementations;

namespace MicroserviceBasedFintechApp.Identity.Api.Extensions
{
    public static class DiExtensions
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            services
                .AddSingleton<IHashService, Sha256HashService>()
                .AddScoped<ICompanyService, CompanyService>();
            return services;
        }

        public static IServiceCollection AddPersistence(this IServiceCollection services)
        {
            services.AddDbContext<IdentityDbContext>();
            services.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<>));
            return services;
        }
    }
}
