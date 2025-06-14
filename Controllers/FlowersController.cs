using Microsoft.AspNetCore.Mvc;
using FlowerInventory.Services;
using FlowerInventory.Models;
using Microsoft.Extensions.Logging;

namespace FlowerInventory.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FlowersController : ControllerBase
    {
        private readonly FlowerService _flowerService;
        private readonly ILogger<FlowersController> _logger;

        public FlowersController(FlowerService flowerService, ILogger<FlowersController> logger)
        {
            _flowerService = flowerService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFlowers()
        {
            try
            {
                _logger.LogInformation("Getting all flowers");
                var flowers = await _flowerService.GetAllFlowersAsync();
                return Ok(flowers);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all flowers");
                return StatusCode(500, $"An error occurred while retrieving flowers: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFlowerById(int id)
        {
            try
            {
                _logger.LogInformation("Getting flower by ID: {FlowerId}", id);
                var flower = await _flowerService.GetFlowerByIdAsync(id);
                if (flower == null)
                {
                    _logger.LogWarning("Flower with ID {FlowerId} not found", id);
                    return NotFound();
                }
                return Ok(flower);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving flower by ID: {FlowerId}", id);
                return StatusCode(500, $"An error occurred while retrieving the flower: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddFlower([FromBody] Flower flower)
        {
            try
            {
                _logger.LogInformation("Adding a new flower: {@Flower}", flower);
                await _flowerService.AddFlowerAsync(flower);
                _logger.LogInformation("Flower added successfully with ID {FlowerId}", flower.FlowerId);
                return CreatedAtAction(nameof(GetFlowerById), new { id = flower.FlowerId }, flower);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding flower: {@Flower}", flower);
                return StatusCode(500, $"An error occurred while adding the flower: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFlower(int id, [FromBody] Flower flower)
        {
            if (id != flower.FlowerId)
            {
                _logger.LogWarning("UpdateFlower: Mismatched ID {Id} and FlowerId {FlowerId}", id, flower.FlowerId);
                return BadRequest();
            }
            try
            {
                _logger.LogInformation("Updating flower: {@Flower}", flower);
                await _flowerService.UpdateFlowerAsync(flower);
                _logger.LogInformation("Flower updated successfully with ID {FlowerId}", flower.FlowerId);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating flower: {@Flower}", flower);
                return StatusCode(500, $"An error occurred while updating the flower: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFlower(int id)
        {
            try
            {
                _logger.LogInformation("Deleting flower with ID: {FlowerId}", id);
                await _flowerService.DeleteFlowerAsync(id);
                _logger.LogInformation("Flower deleted successfully with ID {FlowerId}", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting flower with ID: {FlowerId}", id);
                return StatusCode(500, $"An error occurred while deleting the flower: {ex.Message}");
            }
        }
    }
}