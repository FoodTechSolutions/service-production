using System;
using Domain.DTO;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApi.Controllers;

[ApiController]
public class ProductionController : Controller
{
    private readonly ILogger<ProductionController> _logger;
    private readonly IProductionService _productionService;
    public ProductionController(ILogger<ProductionController> logger, IProductionService productionService)
    {
        _logger = logger;
        _productionService = productionService;
    }
    
    [HttpPost]
    [Route("ReciveOrder")]
    public IActionResult ReciveOrder(ReceivingOrderDto reciveOrderDto)
    {
        try
        {
            var result = _productionService.ReceiveOrder(reciveOrderDto);
            
            if(result.Success)
                return Ok(result.Message);

            return BadRequest(result.Message);
        }
        catch (Exception e)
        {
            _logger.Log(LogLevel.Error, e.Message);
            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    [Route("StartProduction")]
    public IActionResult StartProduction(Guid productionId)
    {
        try
        {
            var result = _productionService.StartProduction(productionId);
            
            if(result.Success)
                return Ok(result.Message);

            return BadRequest(result.Message);
        }
        catch (Exception e)
        {
            _logger.Log(LogLevel.Error, e.Message);
            return BadRequest(e.Message);
        }
    }
    
    [HttpPost]
    [Route("FinishProduction")]
    public IActionResult FinishProduction(Guid productionId)
    {
        try
        {
            var result = _productionService.FinishProduction(productionId);
            
            if(result.Success)
                return Ok(result.Message);

            return BadRequest(result.Message);
        }
        catch (Exception e)
        {
            _logger.Log(LogLevel.Error, e.Message);
            return BadRequest(e.Message);
        }
    }
    
    [HttpPost]
    [Route("CancelProduction")]
    public IActionResult CancelProduction(Guid productionId)
    {
        try
        {
            var result = _productionService.CancelProduction(productionId);
            
            if(result.Success)
                return Ok(result.Message);

            return BadRequest(result.Message);
        }
        catch (Exception e)
        {
            _logger.Log(LogLevel.Error, e.Message);
            return BadRequest(e.Message);
        }
    }
}