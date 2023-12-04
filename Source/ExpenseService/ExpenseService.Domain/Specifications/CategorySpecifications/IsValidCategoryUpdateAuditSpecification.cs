namespace ExpenseService.Domain.Specifications.CategorySpecifications;

/// <summary>
/// Represents a specification that checks if a category has valid update audit information.
/// </summary>
public class IsValidCategoryUpdateAuditSpecification : Specification<Category>
{
	protected override void ConfigureConditions()
	{
		AddCondition(category => category.LastUpdatedAt != null && category.LastUpdatedAt != default, "Category must have a last updated at date.");
		AddCondition(category => !string.IsNullOrWhiteSpace(category.LastUpdatedBy), "Category must have a last updated by user.");
	}
}
