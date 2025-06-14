namespace FlowerInventory.Models
{
    public class Flower
    {
        public int FlowerId { get; set; }
        public string? Name { get; set; }
        public string? Type { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}