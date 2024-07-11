using Domain.DTO;
using Domain.Entities;

namespace Domain.Services;

public interface IProductService
{
    Result GetById(Guid id);
    IEnumerable<Product> GetAll();
    Result CreateProduct(ProductDto.Create model);
    Result UpdateProduct(ProductDto.Update model);
    Result DeleteProduct(Guid id);
    Result LinkIngredient(List<Guid> ingredientId, Guid productId);
    Result RemoveIngredient(Guid productIngredientId);
    Result GetAllWithIngredients();
    Result GetByProductId(Guid productId);
}