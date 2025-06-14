using FlowerInventory.Data;
using FlowerInventory.Models;
using FlowerInventory.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace FlowerInventory.Tests
{
    public class FlowerServiceTests
    {
        private readonly FlowerService _flowerService;
        private readonly FlowerShopContext _context;

        public FlowerServiceTests()
        {
            var options = new DbContextOptionsBuilder<FlowerShopContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new FlowerShopContext(options);
            _flowerService = new FlowerService(_context);

            SeedDatabase();
        }

        private void SeedDatabase()
        {
            _context.Categories.RemoveRange(_context.Categories);
            _context.Flowers.RemoveRange(_context.Flowers);
            _context.SaveChanges();

            var category = new Category { Name = "TestCategory", Description = "TestDescription" };
            _context.Categories.Add(category);
            _context.SaveChanges();

            var flower = new Flower { Name = "TestFlower", Type = "TestType", Price = 10.0m, CategoryId = category.CategoryId };
            _context.Flowers.Add(flower);
            _context.SaveChanges();
        }

        [Fact]
        public async Task GetAllFlowersAsync_ShouldReturnFlowers()
        {
            var flowers = await _flowerService.GetAllFlowersAsync();
            Assert.Single(flowers);
        }
    }
}