using Domain.Entities;
using Domain.Enums;

namespace Tests.Domain;

[TestFixture]
public class ProductionTests
{
    [Test]
    public void CreateProduction_ShouldCreateProductionWithCurrentDateTime()
    {
        // Arrange & Act
        var production = Production.CreateProduction();

        // Assert
        Assert.That(production, Is.Not.Null);
        Assert.That(production.CreatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        Assert.That(production.UpdatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
    }

    [Test]
    public void SetOrder_ShouldSetOrderCorrectly()
    {
        // Arrange
        var production = Production.CreateProduction();
        var order = "Order123";

        // Act
        production.SetOrder(order);

        // Assert
        Assert.That(production.Order, Is.EqualTo(order));
    }

    [Test]
    public void SetOrder_ShouldThrowExceptionForNullOrder()
    {
        // Arrange
        var production = Production.CreateProduction();

        // Act & Assert
        Assert.Throws<Exception>(() => production.SetOrder(null));
    }

    [Test]
    public void SetCustomer_ShouldSetCustomerCorrectly()
    {
        // Arrange
        var production = Production.CreateProduction();
        var customer = "John Doe";

        // Act
        production.SetCustomer(customer);

        // Assert
        Assert.That(production.Customer, Is.EqualTo(customer));
    }

    [Test]
    public void SetCustomer_ShouldThrowExceptionForInvalidCustomerName()
    {
        // Arrange
        var production = Production.CreateProduction();

        // Act & Assert
        Assert.Throws<Exception>(() => production.SetCustomer("Jo")); // Muito curto
        Assert.Throws<Exception>(() => production.SetCustomer(new string('A', 301)));
    }

    [Test]
    public void NextStatus_ShouldChangeStatusCorrectly()
    {
        // Arrange
        var production = Production.CreateProduction();
        production.SetOrder("123").SetCustomer("Test");

        // Act & Assert
        Assert.That(production.Status, Is.EqualTo(StatusProduction.Received));

        production.NextStatus();
        Assert.That(production.Status, Is.EqualTo(StatusProduction.InProgress));

        production.NextStatus();
        Assert.That(production.Status, Is.EqualTo(StatusProduction.Ready));

        production.NextStatus();
        Assert.That(production.Status, Is.EqualTo(StatusProduction.Finished));

        production.NextStatus();
        Assert.That(production.Status, Is.EqualTo(StatusProduction.Finished));
    }

    [Test]
    public void NextStatus_ShouldThrowExceptionIfCanceled()
    {
        // Arrange
        var production = Production.CreateProduction();
        production.CancelProduction();

        // Act & Assert
        Assert.Throws<Exception>(() => production.NextStatus());
    }

    [Test]
    public void CancelProduction_ShouldCancelProduction()
    {
        // Arrange
        var production = Production.CreateProduction();

        // Act
        production.CancelProduction();

        // Assert
        Assert.That(production.Status, Is.EqualTo(StatusProduction.Cancel));
    }

    [Test]
    public void CancelProduction_ShouldThrowExceptionIfFinished()
    {
        // Arrange
        var production = Production.CreateProduction();
        production.SetOrder("123").SetCustomer("Test");
        production.NextStatus().NextStatus().NextStatus();

        // Act & Assert
        Assert.Throws<Exception>(() => production.CancelProduction());
    }
}
