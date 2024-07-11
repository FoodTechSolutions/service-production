using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Context;
using Infrastructure.Repositories.Common;

namespace Infrastructure.Repositories;

public class ProductionProductRepository : EfRepository<ProductionProduct>, IProductionProductRepository
{
    public ProductionProductRepository(ProductionContext context) : base(context)
    {
    }
}