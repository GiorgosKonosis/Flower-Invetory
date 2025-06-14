using Microsoft.AspNetCore.Mvc;
using FlowerInventory.Services;
using FlowerInventory.Models;
using Microsoft.Extensions.Logging;

namespace FlowerInventory.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryService _categoryService;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(CategoryService categoryService, ILogger<CategoryController> logger)
        {
            _categoryService = categoryService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            try
            {
                _logger.LogInformation("Getting all categories");
                var categories = await _categoryService.GetAllCategoriesAsync();
                return Ok(categories);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all categories");
                return StatusCode(500, $"An error occurred while retrieving categories: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            try
            {
                _logger.LogInformation("Getting category by ID: {CategoryId}", id);
                var category = await _categoryService.GetCategoryByIdAsync(id);
                if (category == null)
                {
                    _logger.LogWarning("Category with ID {CategoryId} not found", id);
                    return NotFound();
                }
                return Ok(category);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving category by ID: {CategoryId}", id);
                return StatusCode(500, $"An error occurred while retrieving the category: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory([FromBody] Category category)
        {
            try
            {
                _logger.LogInformation("Adding a new category: {@Category}", category);
                await _categoryService.AddCategoryAsync(category);
                _logger.LogInformation("Category added successfully with ID {CategoryId}", category.CategoryId);
                return CreatedAtAction(nameof(GetCategoryById), new { id = category.CategoryId }, category);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding category: {@Category}", category);
                return StatusCode(500, $"An error occurred while adding the category: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] Category category)
        {
            if (id != category.CategoryId)
            {
                _logger.LogWarning("UpdateCategory: Mismatched ID {Id} and CategoryId {CategoryId}", id, category.CategoryId);
                return BadRequest();
            }
            try
            {
                _logger.LogInformation("Updating category: {@Category}", category);
                await _categoryService.UpdateCategoryAsync(category);
                _logger.LogInformation("Category updated successfully with ID {CategoryId}", category.CategoryId);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating category: {@Category}", category);
                return StatusCode(500, $"An error occurred while updating the category: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                _logger.LogInformation("Deleting category with ID: {CategoryId}", id);
                await _categoryService.DeleteCategoryAsync(id);
                _logger.LogInformation("Category deleted successfully with ID {CategoryId}", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting category with ID: {CategoryId}", id);
                return StatusCode(500, $"An error occurred while deleting the category: {ex.Message}");
            }
        }
    }
}