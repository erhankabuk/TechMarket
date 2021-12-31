using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class MarketContext : DbContext
    {
        public MarketContext(DbContextOptions<MarketContext> options): base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Brand> Brands{ get; set; }
        public DbSet<Product> Products{ get; set; }
        public DbSet<Basket> Baskets{ get; set; }
        public DbSet<BasketItem> BasketItems{ get; set; }
        public DbSet<Order>Orders{ get; set; }
        public DbSet<OrderItem> OrderItems{ get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<Category>().Property(x => x.Name).IsRequired();
            //modelBuilder.ApplyConfiguration(new BrandConfig()) 
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        }
    }
}
