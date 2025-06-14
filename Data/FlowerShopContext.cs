using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class FlowerShopContext : DbContext
    {
        public FlowerShopContext(DbContextOptions<FlowerShopContext> options) : base(options) { }

        public DbSet<Flower> Flowers { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, Name = "Roses", Description = "Beautiful and fragrant flowers" },
                new Category { CategoryId = 2, Name = "Tulips", Description = "Elegant spring flowers" }
            );

            modelBuilder.Entity<Flower>().HasData(
                new Flower { FlowerId = 1, Name = "Red Rose", Type = "Perennial", Price = 2.50m, CategoryId = 1 },
                new Flower { FlowerId = 2, Name = "White Rose", Type = "Perennial", Price = 3.00m, CategoryId = 1 },
                new Flower { FlowerId = 3, Name = "Yellow Tulip", Type = "Annual", Price = 1.75m, CategoryId = 2 },
                new Flower { FlowerId = 4, Name = "Purple Tulip", Type = "Annual", Price = 2.00m, CategoryId = 2 }
            );
        }
    }
}