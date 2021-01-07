using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OrderLaptop.Models;

namespace OrderLaptop.Data
{
    public class LibraryContext : DbContext
    {
              public LibraryContext(DbContextOptions<LibraryContext> options) :base(options)  
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Laptop> Laptops { get; set; }
        public DbSet<Site> Sites { get; set; }
        public DbSet<LaptopSite> LaptopSites { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().ToTable("Customer");
            modelBuilder.Entity<Order>().ToTable("Order");
            modelBuilder.Entity<Laptop>().ToTable("Laptop");
            modelBuilder.Entity<Site>().ToTable("Site");
            modelBuilder.Entity<LaptopSite>().ToTable("LaptopSite");

            modelBuilder.Entity<LaptopSite>()
                        .HasKey(c => new { c.LaptopID, c.SiteID });//configureaza cheia primara compusa
        }
    }
}
