using System.Linq.Expressions;

namespace ExpenseService.Domain.Specifications.CategorySpecifications;

/// <summary>
/// Represents a specification for validating the creation audit of a category.
/// </summary>
public class IsValidCategoryCreationAuditSpecification: Specification<Category>
{
	protected override void ConfigureConditions()
	{
		AddCondition(category => !string.IsNullOrEmpty(category.CreatedBy) == false, "Category creation audit is invalid.");
		AddCondition(category => !string.IsNullOrWhiteSpace(category.CreatedBy) == false, "Category creation audit is invalid.");
		AddCondition(category => category.CreatedAt != default, "Category creation audit is invalid.");
	}
}
