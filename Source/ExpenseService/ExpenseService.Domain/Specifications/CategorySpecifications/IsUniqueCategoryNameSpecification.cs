using ExpenseService.Domain.Services.Category;

namespace ExpenseService.Domain.Specifications.CategorySpecifications;

/// <summary>
/// Represents a specification that checks if a category with the same name for the same user already exists.
/// </summary>
public class IsUniqueCategoryNameSpecification: Specification<Category>
{
	readonly ICategoryUniquenessChecker _categoryUniquenessChecker;

	public IsUniqueCategoryNameSpecification(ICategoryUniquenessChecker categoryUniquenessChecker)
	{
		_categoryUniquenessChecker = categoryUniquenessChecker;
	}

	protected override void ConfigureConditions()
	{
		AddCondition(category =>
		category.Id == Guid.Empty ? _categoryUniquenessChecker.IsUniqueCategoryNameAsync(category.Name, category.UserId).Result
		: _categoryUniquenessChecker.IsUniqueCategoryNameAsync(category.Id, category.Name, category.UserId).Result, "Category name must be unique.");
	}
}
