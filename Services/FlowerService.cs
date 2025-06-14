using FlowerInventory.Data;
using FlowerInventory.Models;
using Microsoft.EntityFrameworkCore;

namespace FlowerInventory.Services
{
    public class FlowerService
    {
        private readonly FlowerShopContext _context;

        public FlowerService(FlowerShopContext context)
        {
            _context = context;
        }

        public async Task<List<Flower>> GetAllFlowersAsync()
        {
            return await _context.Flowers.Include(f => f.Category).ToListAsync();
        }

        public async Task<Flower> GetFlowerByIdAsync(int id)
        {
            return await _context.Flowers.Include(f => f.Category).FirstOrDefaultAsync(f => f.FlowerId == id);
        }

        public async Task AddFlowerAsync(Flower flower)
        {
            _context.Flowers.Add(flower);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateFlowerAsync(Flower flower)
        {
            _context.Flowers.Update(flower);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteFlowerAsync(int id)
        {
            var flower = await _context.Flowers.FindAsync(id);
            if (flower != null)
            {
                _context.Flowers.Remove(flower);
                await _context.SaveChangesAsync();
            }
        }
    }
}