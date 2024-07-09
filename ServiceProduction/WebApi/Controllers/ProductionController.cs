using System;
using Domain.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApi.Controllers;

[ApiController]
public class ProductionController : Controller
{
    private readonly ILogger<ProductionController> _logger;
    
    public ProductionController(ILogger<ProductionController> logger)
    {
        _logger = logger;
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