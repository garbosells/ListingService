using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ListingService.EtsyDatabase
{
    public class EtsyDatabaseContext : DbContext
    {
        private IConfiguration configuration;

        public DbSet<Category> categories { get; set; }
        public DbSet<Defaults> defaults { get; set; }

        public EtsyDatabaseContext(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
=> optionsBuilder.UseNpgsql(GetConnectionString());

        private string GetConnectionString()
        {
            var server = configuration.GetValue<string>("EtsyDbConnectionInfo:Server");
            var port = configuration.GetValue<string>("EtsyDbConnectionInfo:Port");
            var user = configuration.GetValue<string>("EtsyDbConnectionInfo:User");
            var pw = configuration.GetValue<string>("EtsyDbConnectionInfo:Password");
            var database = configuration.GetValue<string>("EtsyDbConnectionInfo:Database");
            return $"Server={server};Port={port};User Id={user};Password={pw};Database={database};SSL Mode=Require;Trust Server Certificate=true";
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().ToTable("categories");
            modelBuilder.Entity<Defaults>().ToTable("defaults");
        }
    }
}
