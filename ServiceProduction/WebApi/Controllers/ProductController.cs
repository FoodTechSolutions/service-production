using Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

public class ProductController : Controller
{
    private readonly ILogger<ProductionController> _logger;
    private readonly IProductService _productService;
    
    public ProductController(ILogger<ProductionController> logger, IProductService productService)
    {
        _logger = logger;
        _productService = productService;
    }
    
    [HttpGet]
    public IActionResult GetProducts()
    {
        try
        {
            var products = _productService.GetAllWithIngredients();

            if (products.Success)
                return Ok(products.Object);
            
            return BadRequest(products.Message);
        }
        catch (Exception e)
        {
            _logger.Log(LogLevel.Error, e.Message);
            return BadRequest(e.Message);
        }
    }
    
    [HttpGet]
    public IActionResult GetIngredientsByProduct(Guid productId)
    {
        try
        {
            var product = _productService.GetByProductId(productId);

            if (product.Success)
                return Ok(product.Object);
                
            return BadRequest(product.Message);
        }
        catch (Exception e)
        {
            _logger.Log(LogLevel.Error, e.Message);
            return BadRequest(e.Message);
        }
    }
}