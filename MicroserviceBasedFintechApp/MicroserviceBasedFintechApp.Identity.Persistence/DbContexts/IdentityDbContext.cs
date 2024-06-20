using MicroserviceBasedFintechApp.Identity.Core.Contracts.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace MicroserviceBasedFintechApp.Identity.Persistence.DbContexts
{
    public class IdentityDbContext : DbContext
    {
        private readonly IConfiguration _config;
        public IdentityDbContext(IConfiguration config)
        {
            _config = config;
        }
        public DbSet<Company> Companies { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseNpgsql(_config.GetConnectionString("IdentityDatabase"))
                .UseSnakeCaseNamingConvention();
        
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>(entity =>
            {
                entity.HasKey(e => e.Id);
                
                entity.HasIndex(e => new { e.ApiKey, e.HashedSecret }).IsUnique();

                entity.Property(e => e.CreationDateAtUtc)
                    .HasColumnType("timestamptz");

                entity.Property(e => e.UpdateDateAtUtc)
                    .HasColumnType("timestamptz");
            });
        }
    }
}
