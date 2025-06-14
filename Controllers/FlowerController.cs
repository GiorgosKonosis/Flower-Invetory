using Microsoft.AspNetCore.Mvc;
using FlowerInventory.Services;
using FlowerInventory.Models;

namespace FlowerInventory.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FlowerController : ControllerBase
    {
        private readonly FlowerService _flowerService;

        public FlowerController(FlowerService flowerService)
        {
            _flowerService = flowerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFlowers()
        {
            var flowers = await _flowerService.GetAllFlowersAsync();
            return Ok(flowers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFlowerById(int id)
        {
            var flower = await _flowerService.GetFlowerByIdAsync(id);
            if (flower == null)
                return NotFound();

            return Ok(flower);
        }

        [HttpPost]
        public async Task<IActionResult> AddFlower([FromBody] Flower flower)
        {
            await _flowerService.AddFlowerAsync(flower);
            return CreatedAtAction(nameof(GetFlowerById), new { id = flower.FlowerId }, flower);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFlower(int id, [FromBody] Flower flower)
        {
            if (id != flower.FlowerId)
                return BadRequest();

            await _flowerService.UpdateFlowerAsync(flower);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFlower(int id)
        {
            await _flowerService.DeleteFlowerAsync(id);
            return NoContent();
        }
    }
}