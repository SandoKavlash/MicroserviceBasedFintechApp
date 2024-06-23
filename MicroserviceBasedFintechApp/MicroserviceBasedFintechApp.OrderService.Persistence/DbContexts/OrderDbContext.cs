using MicroserviceBasedFintechApp.OrderService.Core.Contracts.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace MicroserviceBasedFintechApp.OrderService.Persistence.DbContexts
{
    public class OrderDbContext : DbContext
    {
        private readonly IConfiguration _config;
        public OrderDbContext(IConfiguration config)
        {
            _config = config;
        }

        public DbSet<Order> Orders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseNpgsql(_config.GetConnectionString("OrderDatabase"))
                .UseSnakeCaseNamingConvention();

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasIndex(e => e.Authenticated);

                entity
                    .HasIndex(e => new { e.ApiKey, e.IdempotencyKey })
                    .IsUnique();

                entity.Property(e => e.CreationDateAtUtc)
                    .HasColumnType("timestamptz");

                entity.Property(e => e.UpdateDateAtUtc)
                    .HasColumnType("timestamptz");
            });
        }
    }
}
