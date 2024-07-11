using Application.ViewModel;

namespace Domain.DTO;

public class ProductionDto
{
    public string Order { get; private set; }
    public string Customer { get; private set; }
    public List<ProductViewModel> Products { get; set; }
}