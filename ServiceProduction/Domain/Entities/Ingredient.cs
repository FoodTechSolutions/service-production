namespace Domain.Entities;

public class Ingredient : BaseEntity
{
    public string Name { get; private set; }
    public double Price { get; private set; }
    
    public static Ingredient Create()
    {
        var result = new Ingredient();
        result.SetCreatedAt();
        result.SetUpdatedAt();
        return result;
    }

    public Ingredient SetName(string name)
    {
        Name = name;
        return this;
    }

    public Ingredient SetPrice(double price)
    {
        Price = price;
        return this;
    }
}