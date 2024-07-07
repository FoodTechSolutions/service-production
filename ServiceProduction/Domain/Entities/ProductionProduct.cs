using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class ProductionProduct : BaseEntity
{
    [ForeignKey("ProductIngredient")]
    public Guid ProductIngredientId { get; set; }
    [ForeignKey("Production")]
    public Guid ProductionId { get; set; }
}