using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class ProductionProduct : BaseEntity
{
    public ProductionProduct()
    {
        SetCreatedAt();
        SetUpdatedAt();
    }
    
    [ForeignKey("Product")]
    public Guid ProductId { get; private set; }
    [ForeignKey("Production")]
    public Guid ProductionId { get; private set; }
    
    public static ProductionProduct CreateProductionProduct()
    {
        var result = new ProductionProduct();
        return result;
    }

    public ProductionProduct SetProduction(Guid productionId)
    {
        ProductionId = productionId;
        return this;
    }

    public ProductionProduct SetProduct(Guid productId)
    {
        ProductId = productId;
        return this;
    }
}