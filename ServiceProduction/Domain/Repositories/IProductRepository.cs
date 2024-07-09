using Domain.Entities;
using Domain.Repositories.Common;

namespace Domain.Repositories;

public interface IProductRepository : IAsyncRepository<Product>
{
    void AddRangeProductIngredient(List<ProductIngredient> itens);
    void RemoveProductIngredient(ProductIngredient item);
}