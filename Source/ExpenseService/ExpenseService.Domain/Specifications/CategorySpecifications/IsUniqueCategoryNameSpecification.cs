namespace ExpenseService.Domain.Specifications.CategorySpecifications;

/// <summary>
/// Represents a specification that checks if a category with the same name for the same user already exists.
/// </summary>
public class IsUniqueCategoryNameSpecification: Specification<Category>
{
	readonly ICategoryService _cCategoryService;

	public IsUniqueCategoryNameSpecification(ICategoryService categoryService)
	{
		_cCategoryService = categoryService;
	}

	protected override void ConfigureRules()
	{
		AddRule(category => _cCategoryService.IsUniqueCategoryNameAsync(category.Id, category.Name, category.UserId).Result, "Category name must be unique.");
	}
}
