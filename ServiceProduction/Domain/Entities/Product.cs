namespace Domain.Entities;

public class Product : BaseEntity
{
    public string Name { get; private set; }
    public Guid CategoryId { get; private set; }
    public double Price { get; private set; }
    public string Description { get; private set; }
    public string ImageUrl { get; private set; }
    public int Estimative { get; private set; }

    public static Product CreateProduct()
    {
        var result = new Product();
        result.SetCreatedAt();
        result.SetUpdatedAt();
        return result;
    }

    public Product SetName(string name)
    {
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
        Estimative = estimative;
        return this;
    }
}