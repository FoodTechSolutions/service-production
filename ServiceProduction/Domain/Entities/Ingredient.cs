namespace Domain.Entities;

public class Ingredient : BaseEntity
{
    public Ingredient()
    {
        Name = string.Empty;
        SetCreatedAt();
        SetUpdatedAt();
    }
    
    public string Name { get; private set; }
    public double Price { get; private set; }
    
    public static Ingredient Create()
    {
        var result = new Ingredient();
        return result;
    }

    public Ingredient SetName(string? name)
    {
        if (name == null)
            throw new ArgumentException("name cannot be empty");
    
        if (name != null && name.Length < 3)
            throw new ArgumentException("the name cannot be less than 3 characters");
        
        Name = name;
        return this;
    }

    public Ingredient SetPrice(double price)
    {
        if (double.IsNegative(price))
            throw new ArgumentException("the value cannot be negative");
        
        Price = price;
        return this;
    }
}