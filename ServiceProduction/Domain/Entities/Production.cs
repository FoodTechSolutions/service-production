using Domain.Enums;

namespace Domain.Entities;

public class Production : BaseEntity
{
    public string Code { get; private set; }
    public string Order { get; private set; }
    public string Customer { get; private set; }
    public StatusProduction Status { get; set; }
}