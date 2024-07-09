using Domain.DTO;
using Domain.Entities;
using Domain.Repositories;
using Domain.Services;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly ILogger<ProductService> _logger;
    
    public ProductService(IProductRepository productRepository, ILogger<ProductService> logger)
    {
        _productRepository = productRepository;
        _logger = logger;
    }
    
    public Result GetById(Guid id)
    {
        try
        {
            var product = _productRepository.GetById(id);

            if(product == null)
                return Result.FailResult("Product not found");
            
            return Result.ObjectResult(product);
        }
        catch (Exception e)
        {
            _logger.Log(LogLevel.Error, e.Message);
            return Result.FailResult(e.Message);
        }
    }

    public IEnumerable<Product> GetAll()
    {
        try
        {
            var products = _productRepository.GetAll();

            return products;
        }
        catch (Exception e)
        {
            _logger.Log(LogLevel.Error, e.Message);
            return Enumerable.Empty<Product>();
        }
    }

    public Result CreateProduct(ProductDto.Create model)
    {
        try
        {
            var product = Product.CreateProduct();
            
            product
                .SetName(model.Name)
                .SetDescription(model.Description)
                .SetEstimative(model.Estimative)
                .SetPrice(model.Price)
                .SetCategoryId(model.CategoryId);
                
            _productRepository.Update(product);
            return Result.SuccessResult();
        }
        catch (Exception e)
        {
            _logger.Log(LogLevel.Error, e.Message);
            return Result.FailResult(e.Message);
        }
    }

    public Result UpdateProduct(ProductDto.Update model)
    {
        try
        {
            var product = _productRepository.GetById(model.Id);
            
            if (product == null) return Result.FailResult("Product not found");
            
            product
                .SetName(model.Name)
                .SetDescription(model.Description)
                .SetEstimative(model.Estimative)
                .SetPrice(model.Price)
                .SetCategoryId(model.CategoryId)
                .SetUpdatedAt();
                
            _productRepository.Update(product);
            return Result.SuccessResult();
        }
        catch (Exception e)
        {
            _logger.Log(LogLevel.Error, e.Message);
            return Result.FailResult(e.Message);
        }
    }

    public Result DeleteProduct(Guid id)
    {
        try
        {
            var product = _productRepository.GetById(id);

            if(product == null)
                return Result.FailResult("Product not found");

            product.SetDeletedAt();
            
            _productRepository.Update(product);
            
            return Result.SuccessResult();
        }
        catch (Exception e)
        {
            _logger.Log(LogLevel.Error, e.Message);
            return Result.FailResult(e.Message);
        }
    }

    public Result LinkIngredient(List<Guid> ingredientId, Guid productId)
    {
        try
        {
            var list = new List<ProductIngredient>();
            
            foreach (var id in ingredientId)
            {
                var item = ProductIngredient.Create();

                item
                    .SetIngredientId(id)
                    .SetProductId(productId);
                
                list.Add(item);
            }
            _productRepository.AddRangeProductIngredient(list);

            return Result.SuccessResult();
        }
        catch (Exception e)
        {
            _logger.Log(LogLevel.Error, e.Message);
            return Result.FailResult(e.Message);
        }
    }

    public Result RemoveIngredient(Guid productIngredientId)
    {
        var productIngredient = _productRepository.GetProductIngredient(productIngredientId);

        if (productIngredient == null)
            return Result.FailResult("Link not found!");

        _productRepository.RemoveProductIngredient(productIngredient);
        
        return Result.SuccessResult("Link removed successfully");
    }

    public Result GetAllWithIngredients()
    {
        var products = _productRepository.GetAll();
        var ingredients = _productRepository.
        
    }
}