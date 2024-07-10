using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Context;
using Infrastructure.Repositories.Common;

namespace Infrastructure.Repositories;

public class IngredientRepository : EfRepository<Ingredient>, IIngredientRepository
{
    public IngredientRepository(ProductionContext context) : base(context)
    {
    }
}