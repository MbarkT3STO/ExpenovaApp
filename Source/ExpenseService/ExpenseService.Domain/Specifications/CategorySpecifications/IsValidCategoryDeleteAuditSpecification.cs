namespace ExpenseService.Domain.Specifications.CategorySpecifications;

/// <summary>
/// Represents a specification that checks if a category is valid for deletion audit.
/// </summary>
public class IsValidCategoryDeleteAuditSpecification : Specification<Category>
{
	protected override void ConfigureRules()
	{
		AddRule(category => category.DeletedAt != null && category.DeletedAt != default, "Category must have a Deleted At date.");
		AddRule(category => !string.IsNullOrWhiteSpace(category.DeletedBy), "Category must have a Deleted By user.");
		AddRule(category => category.IsDeleted == true, "Category must be marked as Deleted.");
	}
}
