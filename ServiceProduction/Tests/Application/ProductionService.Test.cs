//using Application.Services;
//using Application.Services.Interface;
//using Domain.DTO;
//using Domain.Entities;
//using Domain.Enums;
//using Domain.Repositories;
//using Microsoft.Extensions.Logging;
//using Moq;

//namespace Tests.Application;

//[TestFixture]
//public class ProductionServiceTests
//{
//    private Mock<IProductionRepository> _productionRepositoryMock;
//    private Mock<IProductRepository> _productRepositoryMock;
//    private Mock<IProductionProductRepository> _productionProductRepositoryMock;
//    private Mock<ILogger<ProductionService>> _loggerMock;
//    private ProductionService _productionService;
//    private Mock<IRabbitMqService> _rabbitMqService;

//    [SetUp]
//    public void Setup()
//    {
//        _productionRepositoryMock = new Mock<IProductionRepository>();
//        _productRepositoryMock = new Mock<IProductRepository>();
//        _rabbitMqService = new Mock<IRabbitMqService>(); ;
//        _productionProductRepositoryMock = new Mock<IProductionProductRepository>();
//        _loggerMock = new Mock<ILogger<ProductionService>>();
//        _productionService = new ProductionService(
//            _productionRepositoryMock.Object, 
//            _productRepositoryMock.Object, 
//            _productionProductRepositoryMock.Object,
//            _rabbitMqService,
//            _loggerMock.Object
//        );
//    }

//    [Test]
//    public void ReceiveOrder_ShouldCreateProductionAndProductionProducts()
//    {
//        // Arrange
//        var model = new ReceivingOrderDto
//        {
//            Customer = "John Doe",
//            Order = "Order123",
//            Items = new List<ReceivingOrderDto.Item>
//            {
//                new ReceivingOrderDto.Item { Name = "Product 1" },
//                new ReceivingOrderDto.Item { Name = "Product 2" }
//            }
//        };
//        var products = new List<Product>();
//        for (int i = 1; i < 3; i++)
//        {
//            var prod = Product.CreateProduct();
//            prod.SetName($"Produto {i}");
//            prod.SetDescription($"Descrição {i}");
            
//            products.Add(prod);
//        }
//        _productRepositoryMock.Setup(repo => repo.GetAll()).Returns(products);

//        // Act
//        var result = _productionService.ReceiveOrder(model);

//        // Assert
//        Assert.IsTrue(result.Success);
//        _productionRepositoryMock.Verify(repo => repo.Add(It.IsAny<Production>()), Times.Once);
//        _productionProductRepositoryMock.Verify(repo => repo.Add(It.IsAny<ProductionProduct>()), Times.Exactly(2));
//    }

//    [Test]
//    public void ReceiveOrder_ShouldLogAndReturnFailResultWhenExceptionOccurs()
//    {
//        // Arrange
//        var model = new ReceivingOrderDto();
//        _productRepositoryMock.Setup(repo => repo.GetAll()).Throws(new Exception("Database error"));

//        // Act
//        var result = _productionService.ReceiveOrder(model);

//        // Assert
//        Assert.IsFalse(result.Success);
//        Assert.That(result.Message, Is.EqualTo("Database error"));
//        _loggerMock.Verify(
//            x => x.Log(
//                LogLevel.Error,
//                It.IsAny<EventId>(),
//                It.Is<It.IsAnyType>((v, t) => v.ToString() == "Database error"),
//                It.IsAny<Exception>(),
//                It.IsAny<Func<It.IsAnyType, Exception, string>>()
//            ),
//            Times.Once);
//    }

//    [Test]
//    public void StartProduction_ShouldUpdateProductionStatusToInProgress()
//    {
//        // Arrange
//        var productionId = Guid.NewGuid();

//        var production = Production.CreateProduction();
//        _productionRepositoryMock.Setup(repo => repo.GetById(productionId)).Returns(production);

//        // Act
//        var result = _productionService.StartProduction(productionId);

//        // Assert
//        Assert.IsTrue(result.Success);
//        Assert.That(production.Status, Is.EqualTo(StatusProduction.InProgress));
//        _productionRepositoryMock.Verify(repo => repo.Update(production), Times.Once);
//    }

//    [Test]
//    public void StartProduction_ShouldReturnFailResultWhenProductionNotFound()
//    {
//        // Arrange
//        var productionId = Guid.NewGuid();
//        _productionRepositoryMock.Setup(repo => repo.GetById(productionId)).Returns((Production)null);

//        // Act
//        var result = _productionService.StartProduction(productionId);

//        // Assert
//        Assert.IsFalse(result.Success);
//        Assert.That(result.Message, Is.EqualTo("Production not found"));
//    }

//    [Test]
//    public void FinishProduction_ShouldUpdateProductionStatusToFinished()
//    {
//        // Arrange
//        var productionId = Guid.NewGuid();
//        var production = Production.CreateProduction();
//        production.NextStatus();
//        production.NextStatus();
//        _productionRepositoryMock.Setup(repo => repo.GetById(productionId)).Returns(production);

//        // Act
//        var result = _productionService.FinishProduction(productionId);

//        // Assert
//        Assert.IsTrue(result.Success);
//        Assert.That(production.Status, Is.EqualTo(StatusProduction.Finished));
//        _productionRepositoryMock.Verify(repo => repo.Update(production), Times.Once);
//    }

//    [Test]
//    public void FinishProduction_ShouldReturnFailResultWhenProductionNotFound()
//    {
//        // Arrange
//        var productionId = Guid.NewGuid();
//        _productionRepositoryMock.Setup(repo => repo.GetById(productionId)).Returns((Production)null);

//        // Act
//        var result = _productionService.FinishProduction(productionId);

//        // Assert
//        Assert.IsFalse(result.Success);
//        Assert.That(result.Message, Is.EqualTo("Production not found"));
//    }

//    [Test]
//    public void CancelProduction_ShouldUpdateProductionStatusToCancel()
//    {
//        // Arrange
//        var productionId = Guid.NewGuid();
//        var production = Production.CreateProduction();
//        production.NextStatus();
//        _productionRepositoryMock.Setup(repo => repo.GetById(productionId)).Returns(production);

//        // Act
//        var result = _productionService.CancelProduction(productionId);

//        // Assert
//        Assert.IsTrue(result.Success);
//        Assert.That(production.Status, Is.EqualTo(StatusProduction.Cancel));
//        _productionRepositoryMock.Verify(repo => repo.Update(production), Times.Once);
//    }

//    [Test]
//    public void CancelProduction_ShouldReturnFailResultWhenProductionNotFound()
//    {
//        // Arrange
//        var productionId = Guid.NewGuid();
//        _productionRepositoryMock.Setup(repo => repo.GetById(productionId)).Returns((Production)null);

//        // Act
//        var result = _productionService.CancelProduction(productionId);

//        // Assert
//        Assert.IsFalse(result.Success);
//        Assert.That(result.Message, Is.EqualTo("Production not found"));
//    }
//}