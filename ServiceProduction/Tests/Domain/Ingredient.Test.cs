using Domain.Entities;

namespace Tests.Domain;

using NUnit.Framework;
using System;

[TestFixture]
public class IngredientTests
{
    [Test]
    public void Create_ShouldCreateIngredientWithEmptyNameAndCurrentDateTime()
    {
        // Arrange & Act
        var ingredient = Ingredient.Create();

        // Assert
        Assert.That(ingredient, Is.Not.Null);
        Assert.That(ingredient.Name, Is.EqualTo(string.Empty));
        Assert.That(ingredient.CreatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        Assert.That(ingredient.UpdatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
    }

    [Test]
    public void SetName_ShouldSetNameCorrectly()
    {
        // Arrange
        var ingredient = Ingredient.Create();
        var name = "Tomato";

        // Act
        ingredient.SetName(name);

        // Assert
        Assert.That(ingredient.Name, Is.EqualTo(name));
    }

    [Test]
    public void SetName_ShouldThrowExceptionForNullOrEmptyName()
    {
        // Arrange
        var ingredient = Ingredient.Create();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => ingredient.SetName(null));
        Assert.Throws<ArgumentException>(() => ingredient.SetName(""));
    }

    [Test]
    public void SetName_ShouldThrowExceptionForShortName()
    {
        // Arrange
        var ingredient = Ingredient.Create();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => ingredient.SetName("To"));
    }

    [Test]
    public void SetPrice_ShouldSetPriceCorrectly()
    {
        // Arrange
        var ingredient = Ingredient.Create();
        var price = 5.99;

        // Act
        ingredient.SetPrice(price);

        // Assert
        Assert.That(ingredient.Price, Is.EqualTo(price));
    }

    [Test]
    public void SetPrice_ShouldThrowExceptionForNegativePrice()
    {
        // Arrange
        var ingredient = Ingredient.Create();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => ingredient.SetPrice(-2.50));
    }
}