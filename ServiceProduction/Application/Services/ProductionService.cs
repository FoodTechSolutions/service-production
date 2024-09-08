using Application.BackgroundServices.Models;
using Application.Configuration;
using Application.Helpers;
using Application.Services.Interface;
using Domain.DTO;
using Domain.Entities;
using Domain.Repositories;
using Domain.Services;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class ProductionService : IProductionService
{
    private readonly IProductionRepository _productionRepository;
    private readonly IProductRepository _productRepository;
    private readonly IProductionProductRepository _productionProductRepository;
    private readonly IRabbitMqService _rabbitMqService;
    private readonly ILogger<ProductionService> _logger;
    
    public ProductionService(IProductionRepository productionRepository, IProductRepository productRepository, IProductionProductRepository productionProductRepository, ILogger<ProductionService> logger, IRabbitMqService rabbitMqService)
    {
        _productionRepository = productionRepository;
        _productRepository = productRepository;
        _productionProductRepository = productionProductRepository;
        _rabbitMqService = rabbitMqService;
        _logger = logger;
    }
    
    public Result ReceiveOrder(ReceivingOrderDto model)
    {
        try
        {
            var products = _productRepository.GetAll();

            var production = Production.CreateProduction();

            production
                .SetCustomer(model.Customer)
                .SetOrder(model.Order);

            _productionRepository.Add(production);
            
            foreach (var item in model.Items)
            {
                var prod = products.FirstOrDefault(x => x.Name == item.Name);

                if (prod == null)
                {
                    prod = Product.CreateProduct();
                    prod.SetName(item.Name);
                    _productRepository.Add(prod);
                }

                var productionProduct = ProductionProduct.CreateProductionProduct();

                productionProduct
                    .SetProduction(production.Id)
                    .SetProduct(prod.Id);
                
                _productionProductRepository.Add(productionProduct);
            }
            return Result.SuccessResult();
        }
        catch (Exception e)
        {
            _logger.Log(LogLevel.Error, e.Message);
            return Result.FailResult(e.Message);
        }
    }
    
    public Result StartProduction(Guid productionId)
    {
        try
        {
            var production = _productionRepository.GetById(productionId);

            if (production == null)
                return Result.FailResult("Production not found");

            production
                .NextStatus()
                .SetUpdatedAt();
            
            _productionRepository.Update(production);
            return Result.SuccessResult();
        }
        catch (Exception e)
        {
            _logger.Log(LogLevel.Error, e.Message);
            return Result.FailResult(e.Message);
        }
    }

    public Result FinishProduction(Guid productionId)
    {
        try
        {
            var production = _productionRepository.GetById(productionId);

            if (production == null)
                return Result.FailResult("Production not found");

            production
                .NextStatus()
                .SetUpdatedAt();
            
            _productionRepository.Update(production);
            return Result.SuccessResult();
        }
        catch (Exception e)
        {
            _logger.Log(LogLevel.Error, e.Message);
            return Result.FailResult(e.Message);
        }
    }

    public Result CancelProduction(Guid productionId)
    {
        try
        {
            var model = new RabbitMqPublishModel<CancelProductionModel>()
            {
                ExchangeName = EventConstants.CANCEL_PRODUCTION_EXCHANGE,
                RoutingKey = string.Empty,
                Message = new CancelProductionModel()
                {
                    ProductionId = productionId
                }
            };

            _rabbitMqService.Publish(model);

            return Result.SuccessResult();
        }
        catch (Exception e)
        {
            _logger.Log(LogLevel.Error, e.Message);
            return Result.FailResult(e.Message);
        }
    }
}