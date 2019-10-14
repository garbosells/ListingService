using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ListingService.Database
{
    public class EbayDatabaseContext : DbContext
    {
        private IConfiguration configuration;

        public DbSet<Category> categories { get; set; }
        public DbSet<CategoryHasAspect> categoryHasAspects { get; set; }
        public DbSet<Aspect> aspects { get; set; }

        public EbayDatabaseContext(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
 => optionsBuilder.UseNpgsql(GetConnectionString());

        private string GetConnectionString()
        {
            var server = configuration.GetValue<string>("DbConnectionInfo:Server");
            var port = configuration.GetValue<string>("DbConnectionInfo:Port");
            var user = configuration.GetValue<string>("DbConnectionInfo:User");
            var pw = configuration.GetValue<string>("DbConnectionInfo:Password");
            var database = configuration.GetValue<string>("DbConnectionInfo:Database");
            return $"Server={server};Port={port};User Id={user};Password={pw};Database={database};SSL Mode=Require;Trust Server Certificate=true";
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().ToTable("categories");
            modelBuilder.Entity<CategoryHasAspect>().ToTable("category_has_aspect");
            modelBuilder.Entity<Aspect>().ToTable("aspects");

            modelBuilder.Entity<Category>().HasKey(c => new { c.garboSellsSubcategoryId, c.isVintage });
            modelBuilder.Entity<Aspect>().HasKey(a => a.aspectId);

            modelBuilder.Entity<CategoryHasAspect>().HasKey(c => new { c.garbosellsSubcategoryId, c.isVintage, c.aspectId });
            modelBuilder.Entity<CategoryHasAspect>().HasOne(c => c.category).WithMany(c => c.categoryHasAspects).HasForeignKey(c => new { c.garbosellsSubcategoryId, c.isVintage });
            modelBuilder.Entity<CategoryHasAspect>().HasOne(a => a.aspect).WithMany(a => a.categoryHasAspects).HasForeignKey(a => a.aspectId);
        }
    }
}
