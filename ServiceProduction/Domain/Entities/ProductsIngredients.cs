using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class ProductIngredient : BaseEntity
{
    [ForeignKey("Product")]
    public Guid ProductId { get; private set; }
    [ForeignKey("Ingredient")]
    public Guid IngredientId { get; private set; }
    
    public static ProductIngredient Create()
    {
        var result = new ProductIngredient();
        result.SetCreatedAt();
        result.SetUpdatedAt();
        return result;
    }
    
    public ProductIngredient SetProductId(Guid productId)
    {
        ProductId = productId;
        return this;
    }
    
    public ProductIngredient SetIngredientId(Guid ingredientId)
    {
        ProductId = ingredientId;
        return this;
    }
    
    
    #region Virtual
    public virtual Ingredient Ingredient { get; set; }
    public virtual Product Product { get; set; }
    #endregion
}