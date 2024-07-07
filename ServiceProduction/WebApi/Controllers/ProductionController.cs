using Domain.DTO;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
public class ProductionController : Controller
{
    private readonly ILogger<ProductionController> _logger;
    
    public ProductionController(ILogger<ProductionController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IActionResult GetProducts()
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
    
    [HttpPost]
    public IActionResult ReciveOrder(ReceivingOrderDto reciveOrderDto)
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

    [HttpPost]
    public IActionResult FinishProduction(Guid productionId)
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