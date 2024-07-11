using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Context;
using Infrastructure.Repositories.Common;

namespace Infrastructure.Repositories;

public class ProductionRepository : EfRepository<Production>, IProductionRepository
{
    public ProductionRepository(ProductionContext context) : base(context)
    {
    }
}