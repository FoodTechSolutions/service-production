using NUnit.Framework;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entities;

[TestFixture]
public class ProductionProductTests
{
    [Test]
    public void CreateProductionProduct_ShouldCreateProductionProductWithCurrentDateTime()
    {
        // Arrange & Act
        var productionProduct = ProductionProduct.CreateProductionProduct();

        // Assert
        Assert.That(productionProduct, Is.Not.Null);
        Assert.That(productionProduct.CreatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        Assert.That(productionProduct.UpdatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
    }

    [Test]
    public void SetProduction_ShouldSetProductionIdCorrectly()
    {
        // Arrange
        var productionProduct = ProductionProduct.CreateProductionProduct();
        var productionId = Guid.NewGuid();

        // Act
        productionProduct.SetProduction(productionId);

        // Assert
        Assert.That(productionProduct.ProductionId, Is.EqualTo(productionId));
    }

    [Test]
    public void SetProduct_ShouldSetProductIdCorrectly()
    {
        // Arrange
        var productionProduct = ProductionProduct.CreateProductionProduct();
        var productId = Guid.NewGuid();

        // Act
        productionProduct.SetProduct(productId);

        // Assert
        Assert.That(productionProduct.ProductId, Is.EqualTo(productId));
    }
}