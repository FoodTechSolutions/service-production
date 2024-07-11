namespace Application.ViewModel;

public class ProductViewModel
{
    public string Name { get; set; }
    public string Description { get; set; }
    public List<IngredientViewModel> Ingredients { get; set; }
    public class IngredientViewModel
    {
        public string Name { get; set; }
    }
}