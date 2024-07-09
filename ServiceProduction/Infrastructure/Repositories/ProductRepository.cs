using System;
using System.Collections.Generic;
using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Context;
using Infrastructure.Repositories.Common;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Repositories;

public class ProductRepository : EfRepository<Product>, IProductRepository
{
    private readonly ProductionContext _context;
    private readonly ILogger<ProductRepository> _logger;
    
    public ProductRepository(ProductionContext context, ILogger<ProductRepository> logger) : base(context)
    {
        _context = context;
        _logger = logger;
    }

    public ProductIngredient? GetProductIngredient(Guid id)
    {
        try
        {
            var result = _context.ProductsIngredients.FirstOrDefault(x => x.Id == id);
            
            return result;
        }
        catch (Exception e)
        {
            _logger.Log(LogLevel.Error, e.Message);
            return null;
        }
    }

    public void AddRangeProductIngredient(List<ProductIngredient> itens)
    {
        try
        {
            _context.ProductsIngredients.AddRange(itens);
            _context.SaveChanges();
        }
        catch (Exception e)
        {
            _logger.Log(LogLevel.Error, e.Message);
        }
    }

    public void RemoveProductIngredient(ProductIngredient item)
    {
        try
        {
            _context.ProductsIngredients.Remove(item);
            _context.SaveChanges();
        }
        catch (Exception e)
        {
            _logger.Log(LogLevel.Error, e.Message);
        }
    }
}