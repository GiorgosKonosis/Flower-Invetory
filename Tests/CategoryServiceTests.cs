using FlowerInventory.Data;
using FlowerInventory.Models;
using FlowerInventory.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace FlowerInventory.Tests
{
    public class CategoryServiceTests
    {
        private readonly CategoryService _categoryService;
        private readonly FlowerShopContext _context;

        public CategoryServiceTests()
        {
            var options = new DbContextOptionsBuilder<FlowerShopContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new FlowerShopContext(options);
            _categoryService = new CategoryService(_context);

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
        public async Task GetAllCategoriesAsync_ShouldReturnCategories()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            Assert.Single(categories);
        }

        [Fact]
        public async Task GetCategoryByIdAsync_ShouldReturnCategory()
        {
            var category = await _categoryService.GetCategoryByIdAsync(1);
            Assert.NotNull(category);
            Assert.Equal("TestCategory", category.Name);
        }
    }
}