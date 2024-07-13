using Domain.Entities;

namespace Tests.Domain;

[TestFixture]
public class ProductTests
{
    [Test]
    public void CreateProduct_ShouldCreateProductWithCurrentDateTime()
    {
        // Arrange & Act
        var product = Product.CreateProduct();

        // Assert
        Assert.That(product, Is.Not.Null);
        Assert.That(product.CreatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        Assert.That(product.UpdatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
    }

    [Test]
    public void Setters_ShouldSetPropertiesCorrectly()
    {
        // Arrange
        var product = Product.CreateProduct();
        var name = "Test Product";
        var categoryId = Guid.NewGuid();
        var price = 9.99;
        var description = "Test Description";
        var imageUrl = "https://test.com/image.jpg";
        var estimative = 5;

        // Act
        product.SetName(name)
            .SetCategoryId(categoryId)
            .SetPrice(price)
            .SetDescription(description)
            .SetImageUrl(imageUrl)
            .SetEstimative(estimative);

        // Assert
        Assert.That(product.Name, Is.EqualTo(name));
        Assert.That(product.CategoryId, Is.EqualTo(categoryId));
        Assert.That(product.Price, Is.EqualTo(price));
        Assert.That(product.Description, Is.EqualTo(description));
        Assert.That(product.ImageUrl, Is.EqualTo(imageUrl));
        Assert.That(product.Estimative, Is.EqualTo(estimative));
    }
    
    [Test]
    public void Setters_ShouldThrowArgumentExceptionForInvalidInput()
    {
        // Arrange
        var product = Product.CreateProduct();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => product.SetName(null));
        Assert.Throws<ArgumentException>(() => product.SetName(""));
        Assert.Throws<ArgumentException>(() => product.SetPrice(-1));
        Assert.Throws<ArgumentException>(() => product.SetEstimative(-5));
    }
}