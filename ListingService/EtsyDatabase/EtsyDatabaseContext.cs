﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ListingService.EtsyDatabase
{
    public class EtsyDatabaseContext : DbContext
    {
        private IConfiguration configuration;

        public DbSet<Category> categories { get; set; }
        public DbSet<Defaults> defaults { get; set; }
        public DbSet<Material> materials { get; set; }
        public DbSet<Measurement> measurements { get; set; }
        public DbSet<Era> eras { get; set; }
        public DbSet<Color> colors { get; set; }
        public DbSet<SizeType> sizeTypes { get; set; }
        public DbSet<LetterSize> letterSizes { get; set; }
        public DbSet<NumericSize> numericSizes { get; set; }
        public DbSet<Property> properties { get; set; }
        public DbSet<PropertyValue> propertyValues { get; set; }

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
            modelBuilder.Entity<Material>().ToTable("materials");
            modelBuilder.Entity<Measurement>().ToTable("measurements");
            modelBuilder.Entity<Era>().ToTable("eras");
            modelBuilder.Entity<Color>().ToTable("colors");
            modelBuilder.Entity<SizeType>().ToTable("size_types");
            modelBuilder.Entity<LetterSize>().ToTable("letter_sizes");
            modelBuilder.Entity<NumericSize>().ToTable("numeric_sizes");
            modelBuilder.Entity<Property>().ToTable("properties");
            modelBuilder.Entity<PropertyValue>().ToTable("property_values");

            modelBuilder.Entity<Category>().HasKey(c => new { c.garboSellsSubcategoryId, c.isVintage });
            modelBuilder.Entity<SizeType>().HasKey(s => new { s.garbosellsSizeTypeId, s.garbosellsSubcategoryId });
            modelBuilder.Entity<LetterSize>().HasKey(s => new { s.letterSize, s.etsyScaleId });
        }
    }
}
