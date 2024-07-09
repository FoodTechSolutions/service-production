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
            
            return Ok(products);
        }
        catch (Exception e)
        {
            _logger.Log(LogLevel.Error, e.Message);
            return BadRequest();
        }
    }
    
    [HttpGet]
    public IActionResult GetIngredientsByProduct(Guid productId)
    {
        try
        {
            return Ok();
        }
        catch (Exception e)
        {
            _logger.Log(LogLevel.Error, e.Message);
            return BadRequest();
        }
    }
}