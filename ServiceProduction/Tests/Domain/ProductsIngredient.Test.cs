using Domain.Entities;

namespace Tests.Domain;

using NUnit.Framework;
using System;
using System.ComponentModel.DataAnnotations.Schema;

[TestFixture]
public class ProductIngredientTests
{
    [Test]
    public void Create_ShouldCreateProductIngredientWithCurrentDateTime()
    {
        // Arrange & Act
        var productIngredient = ProductIngredient.Create();

        // Assert
        Assert.That(productIngredient, Is.Not.Null);
        Assert.That(productIngredient.CreatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        Assert.That(productIngredient.UpdatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
    }

    [Test]
    public void SetProductId_ShouldSetProductIdCorrectly()
    {
        // Arrange
        var productIngredient = ProductIngredient.Create();
        var productId = Guid.NewGuid();

        // Act
        productIngredient.SetProductId(productId);

        // Assert
        Assert.That(productIngredient.ProductId, Is.EqualTo(productId));
    }

    [Test]
    public void SetIngredientId_ShouldSetIngredientIdCorrectly()
    {
        // Arrange
        var productIngredient = ProductIngredient.Create();
        var ingredientId = Guid.NewGuid();
        var productId = Guid.NewGuid();

        // Act
        productIngredient.SetIngredientId(ingredientId);
        productIngredient.SetProductId(productId);

        // Assert
        Assert.That(productIngredient.IngredientId, Is.EqualTo(ingredientId));
    }
}
