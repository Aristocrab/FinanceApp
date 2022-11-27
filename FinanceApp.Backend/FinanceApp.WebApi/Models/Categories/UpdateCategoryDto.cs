namespace FinanceApp.WebApi.Models.Categories;

public class UpdateCategoryDto
{
    public Guid CategoryId { get; set; }
    public string Name { get; set; } = null!;
}