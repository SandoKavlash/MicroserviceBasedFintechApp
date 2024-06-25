using MicroserviceBasedFintechApp.PaymentService.Core.Contracts.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace MicroserviceBasedFintechApp.PaymentService.Persistence.DbContexts
{
    public class PaymentServiceDbContext : DbContext
    {
        private readonly IConfiguration _config;
        public PaymentServiceDbContext(IConfiguration config)
        {
            _config = config;
        }
        public DbSet<AggregatedOrdersDaily> AggregatedDailyOrders { get; set; }
        public DbSet<PaymentOrder> PaymentOrders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseNpgsql(_config.GetConnectionString("PaymentDB"))
                .UseSnakeCaseNamingConvention();

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PaymentOrder>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasIndex(e => new { e.ApiKey, e.IdempotencyKey }).IsUnique();

                entity.Property(e => e.CreationDateAtUtc)
                    .HasColumnType("timestamptz");

                entity.Property(e => e.UpdateDateAtUtc)
                    .HasColumnType("timestamptz");
            });

            modelBuilder.Entity<AggregatedOrdersDaily>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity
                    .HasIndex(e => new { e.ApiKey, e.DateAggregationUTC })
                    .IsUnique();

                entity.Property(e => e.CreationDateAtUtc)
                    .HasColumnType("timestamptz");

                entity.Property(e => e.UpdateDateAtUtc)
                    .HasColumnType("timestamptz");
            });
        }
    }
}
