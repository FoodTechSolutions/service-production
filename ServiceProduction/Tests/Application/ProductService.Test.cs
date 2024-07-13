using Application.Services;
using Domain.DTO;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.Extensions.Logging;
using Moq;

namespace Tests.Application;

[TestFixture]
public class ProductServiceTests
{
    private Mock<IProductRepository> _productRepositoryMock;
    private Mock<IProductIngredientRepository> _productIngredientRepositoryMock;
    private Mock<ILogger<ProductService>> _loggerMock;
    private ProductService _productService;

    [SetUp]
    public void Setup()
    {
        _productRepositoryMock = new Mock<IProductRepository>();
        _productIngredientRepositoryMock = new Mock<IProductIngredientRepository>();
        _loggerMock = new Mock<ILogger<ProductService>>();
        _productService = new ProductService(
            _productRepositoryMock.Object, 
            _productIngredientRepositoryMock.Object, 
            _loggerMock.Object);
    }

    [Test]
    public void GetById_ShouldReturnProductWhenProductExists()
    {
        // Arrange
        var productId = Guid.NewGuid();
        var product = new Product { Id = productId };
        _productRepositoryMock.Setup(repo => repo.GetById(productId)).Returns(product);

        // Act
        var result = _productService.GetById(productId);

        // Assert
        Assert.IsTrue(result.Success);
        Assert.That(result.Object, Is.EqualTo(product));
    }

    [Test]
    public void GetById_ShouldReturnFailResultWhenProductNotFound()
    {
        // Arrange
        var productId = Guid.NewGuid();
        _productRepositoryMock.Setup(repo => repo.GetById(productId)).Returns((Product)null);

        // Act
        var result = _productService.GetById(productId);

        // Assert
        Assert.IsFalse(result.Success);
        Assert.That(result.Message, Is.EqualTo("Product not found"));
    }

    [Test]
    public void GetAll_ShouldReturnAllProducts()
    {
        // Arrange
        var products = new List<Product> { new Product(), new Product() };
        _productRepositoryMock.Setup(repo => repo.GetAll()).Returns(products);

        // Act
        var result = _productService.GetAll();

        // Assert
        Assert.That(result, Is.EqualTo(products));
    }

    [Test]
    public void CreateProduct_ShouldCreateProductAndReturnSuccessResult()
    {
        // Arrange
        var model = new ProductDto.Create 
        { 
            Name = "New Product", 
            Description = "Description", 
            Estimative = 5, 
            Price = 10.99,
            CategoryId = Guid.NewGuid()
        };

        // Act
        var result = _productService.CreateProduct(model);

        // Assert
        Assert.IsTrue(result.Success);
        _productRepositoryMock.Verify(repo => repo.Update(It.IsAny<Product>()), Times.Once);
    }
    
    [Test]
    public void UpdateProduct_ShouldUpdateProductAndReturnSuccessResult()
    {
        // Arrange
        var model = new ProductDto.Update
        {
            Id = Guid.NewGuid(),
            Name = "Updated Product",
            Description = "Updated Description",
            Estimative = 10,
            Price = 15.99,
            CategoryId = Guid.NewGuid()
        };
        var existingProduct = new Product { Id = model.Id };
        _productRepositoryMock.Setup(repo => repo.GetById(model.Id)).Returns(existingProduct);

        // Act
        var result = _productService.UpdateProduct(model);

        // Assert
        Assert.IsTrue(result.Success);
        _productRepositoryMock.Verify(repo => repo.Update(It.Is<Product>(p => 
            p.Id == model.Id && 
            p.Name == model.Name && 
            p.Description == model.Description &&
            p.Estimative == model.Estimative &&
            p.Price == model.Price &&
            p.CategoryId == model.CategoryId
        )), Times.Once);
    }

    [Test]
    public void UpdateProduct_ShouldReturnFailResultWhenProductNotFound()
    {
        // Arrange
        var model = new ProductDto.Update { Id = Guid.NewGuid() };
        _productRepositoryMock.Setup(repo => repo.GetById(model.Id)).Returns((Product)null);

        // Act
        var result = _productService.UpdateProduct(model);

        // Assert
        Assert.IsFalse(result.Success);
        Assert.That(result.Message, Is.EqualTo("Product not found"));
    }

    [Test]
    public void DeleteProduct_ShouldSoftDeleteProductAndReturnSuccessResult()
    {
        // Arrange
        var productId = Guid.NewGuid();
        var product = new Product { Id = productId };
        _productRepositoryMock.Setup(repo => repo.GetById(productId)).Returns(product);

        // Act
        var result = _productService.DeleteProduct(productId);

        // Assert
        Assert.IsTrue(result.Success);
        _productRepositoryMock.Verify(repo => repo.Update(It.Is<Product>(p => p.DeletedAt.HasValue)), Times.Once);
    }

    [Test]
    public void DeleteProduct_ShouldReturnFailResultWhenProductNotFound()
    {
        // Arrange
        var productId = Guid.NewGuid();
        _productRepositoryMock.Setup(repo => repo.GetById(productId)).Returns((Product)null);

        // Act
        var result = _productService.DeleteProduct(productId);

        // Assert
        Assert.IsFalse(result.Success);
        Assert.That(result.Message, Is.EqualTo("Product not found"));
    }

    [Test]
    public void LinkIngredient_ShouldLinkIngredientsAndReturnSuccessResult()
    {
        // Arrange
        var productId = Guid.NewGuid();
        var ingredientIds = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };

        // Act
        var result = _productService.LinkIngredient(ingredientIds, productId);

        // Assert
        Assert.IsTrue(result.Success);
        _productRepositoryMock.Verify(repo => repo.AddRangeProductIngredient(It.IsAny<List<ProductIngredient>>()), Times.Once);
    }

    [Test]
    public void LinkIngredient_ShouldReturnFailResultWhenExceptionOccurs()
    {
        // Arrange
        var productId = Guid.NewGuid();
        var ingredientIds = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };
        _productRepositoryMock.Setup(repo => repo.AddRangeProductIngredient(It.IsAny<List<ProductIngredient>>())).Throws(new Exception("Database error"));

        // Act
        var result = _productService.LinkIngredient(ingredientIds, productId);

        // Assert
        Assert.IsFalse(result.Success);
        Assert.That(result.Message, Is.EqualTo("Database error"));
        _loggerMock.Verify(
            x => x.Log(
                LogLevel.Error,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString() == "Database error"),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception, string>>()
            ),
            Times.Once);
    }

    [Test]
    public void RemoveIngredient_ShouldRemoveIngredientAndReturnSuccessResult()
    {
        // Arrange
        var productIngredientId = Guid.NewGuid();
        var productIngredient = new ProductIngredient { Id = productIngredientId };
        _productRepositoryMock.Setup(repo => repo.GetProductIngredient(productIngredientId)).Returns(productIngredient);

        // Act
        var result = _productService.RemoveIngredient(productIngredientId);

        // Assert
        Assert.IsTrue(result.Success);
        Assert.That(result.Message, Is.EqualTo("Link removed successfully"));
        _productRepositoryMock.Verify(repo => repo.RemoveProductIngredient(productIngredient), Times.Once);
    }
}