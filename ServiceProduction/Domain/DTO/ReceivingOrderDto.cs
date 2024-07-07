using Domain.Entities;

namespace Domain.DTO;

public class ReceivingOrderDto
{
    public string Order { get; set; }
    public string Customer { get; set; }
    public IEnumerable<Item> Items { get; set; }
    
    
    public class Item
    {
        public string Name { get; set; }
        public IEnumerable<Ingredient> Ingredients { get; set; }
    }
    
}