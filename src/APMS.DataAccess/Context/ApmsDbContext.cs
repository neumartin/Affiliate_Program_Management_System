using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Linq;
using System;
using APMS.DataAccess.Mappings;
using APMS.Domain.Entities;

namespace APMS.DataAccess.Context
{
    public class ApmsDbContext : DbContext
    {
        public ApmsDbContext() : base()
        {
            Configure();
        }

        // public iZenEnterpriseDbContext(DbContextOptions<iZenEnterpriseDbContext> options) : base(options)
        // {
        //     Configure();
        // }

        public void Configure()
        {
            // Set up configuration sources.
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true);

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; set; }

        public DbSet<ApiKey> ApiKeys { get; set; }
        public DbSet<Affiliate> Affiliates { get; set; }
        public DbSet<Customer> Customers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connStr = Configuration["Data:ApmsDbContext:ConnectionString"];
            optionsBuilder.UseNpgsql(connStr);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            new ApiKeyMap(modelBuilder.Entity<ApiKey>());
            new AffiliateMap(modelBuilder.Entity<Affiliate>());
            new CustomerMap(modelBuilder.Entity<Customer>());
        }

        public void Seed()
        {
            new Seeder().Seed();
        }
    }
}
