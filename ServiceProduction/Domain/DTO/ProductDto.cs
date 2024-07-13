namespace Domain.DTO;

public class ProductDto
{
    public string? Name { get; set; }
    public double Price { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public int Estimative { get; set; }
    public Guid CategoryId { get; set; }
    public class Create : ProductDto 
    { }
    
    public class Update : ProductDto
    {
        public Guid Id { get; set; }
    }
}