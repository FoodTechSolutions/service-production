using Domain.Entities;
using Domain.Repositories.Common;

namespace Domain.Repositories;

public interface IIngredientRepository : IAsyncRepository<Ingredient>
{
    
}