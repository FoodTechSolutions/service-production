namespace Domain.Entities;

public class Product : BaseEntity
{
    public Product()
    {
        Name = string.Empty;
        SetCreatedAt();
        SetUpdatedAt();
    }
    
    public string? Name { get; private set; }
    public Guid CategoryId { get; private set; }
    public double Price { get; private set; }
    public string? Description { get; private set; }
    public string? ImageUrl { get; private set; }
    public int Estimative { get; private set; }

    public static Product CreateProduct()
    {
        var result = new Product();
        return result;
    }

    public Product SetName(string? name)
    {
        if (name == null)
            throw new ArgumentException("name cannot be empty");
    
        if (name != null && name.Length < 3)
            throw new ArgumentException("the name cannot be less than 3 characters");

        Name = name;
        return this;
    }

    public Product SetCategoryId(Guid categoryId)
    {
        CategoryId = categoryId;
        return this;
    }

    public Product SetPrice(double price)
    {
        if (double.IsNegative(price))
            throw new ArgumentException("the value cannot be negative");
        
        Price = price;
        return this;
    }

    public Product SetDescription(string description)
    {
        Description = description;
        return this;
    }

    public Product SetImageUrl(string imageUrl)
    {
        ImageUrl = imageUrl;
        return this;
    }
    public Product SetEstimative(int estimative)
    {
        if (int.IsNegative(estimative))
            throw new ArgumentException("the value cannot be negative");
        
        Estimative = estimative;
        return this;
    }
}