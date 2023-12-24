using System.Linq.Expressions;

namespace ExpenseService.Domain.Specifications.CategorySpecifications;

/// <summary>
/// Represents a specification for validating the creation audit of a category.
/// </summary>
public class IsValidCategoryCreationAuditSpecification: Specification<Category>
{
	protected override void ConfigureRules()
	{
		AddRule(category => !string.IsNullOrEmpty(category.CreatedBy), "Category creation audit is invalid.");
		AddRule(category => !string.IsNullOrWhiteSpace(category.CreatedBy), "Category creation audit is invalid.");
		AddRule(category => category.CreatedAt != default, "Category creation audit is invalid.");
	}
}
