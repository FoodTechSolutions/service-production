using Application.ViewModel;
using Domain.DTO;
using Domain.Entities;
using Domain.Repositories;
using Domain.Services;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IProductIngredientRepository _productIngredientRepository;
    private readonly ILogger<ProductService> _logger;

    public ProductService(IProductRepository productRepository,
        IProductIngredientRepository productIngredientRepository, ILogger<ProductService> logger)
    {
        _productRepository = productRepository;
        _productIngredientRepository = productIngredientRepository;
        _logger = logger;
    }

    public Result GetById(Guid id)
    {
        try
        {
            var product = _productRepository.GetById(id);

            if (product == null)
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

            if (product == null)
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
        var productsIngredients = _productIngredientRepository.GetAll(c => c.Ingredient);

        if (!productsIngredients.Any())
            return Result.FailResult("Ingredients not found");

        var list = new List<ProductViewModel>();

        foreach (var item in products)
        {
            var product = new ProductViewModel();

            var ingredientsLinked = productsIngredients.Where(x => x.ProductId == item.Id);

            var ingredients = new List<ProductViewModel.IngredientViewModel>();

            foreach (var ingredientLink in ingredientsLinked)
            {
                var ingredient = new ProductViewModel.IngredientViewModel
                {
                    Name = ingredientLink.Ingredient.Name
                };

                ingredients.Add(ingredient);
            }

            product.Name = item.Name;
            product.Description = item.Description;
            product.Ingredients = ingredients;

            list.Add(product);
        }

        return Result.ObjectResult(list);
    }

    public Result GetByProductId(Guid productId)
    {
        var product = _productRepository.GetById(productId);

        if (product == null)
            return Result.FailResult("Product not found");
        
        var productsIngredients = _productIngredientRepository
            .GetAll(c => c.Ingredient, c => c.Product)
            .Where(x => x.ProductId == productId);

        var result = new ProductViewModel();

        var ingredients = new List<ProductViewModel.IngredientViewModel>();

        foreach (var ingredientLink in productsIngredients)
        {
            var ingredient = new ProductViewModel.IngredientViewModel
            {
                Name = ingredientLink.Ingredient.Name
            };

            ingredients.Add(ingredient);
        }

        result.Name = product.Name;
        result.Description = product.Description;
        result.Ingredients = ingredients;

        return Result.ObjectResult(result);
    }
}