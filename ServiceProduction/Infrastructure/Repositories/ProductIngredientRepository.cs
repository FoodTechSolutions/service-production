using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Context;
using Infrastructure.Repositories.Common;

namespace Infrastructure.Repositories;

public class ProductIngredientRepository : EfRepository<ProductIngredient>, IProductIngredientRepository
{
    public ProductIngredientRepository(ProductionContext context) : base(context)
    {
    }
}