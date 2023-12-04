using ExpenseService.Domain.Shared.Common;

namespace ExpenseService.Domain.Specifications.CategorySpecifications.Composite;

public class IsValidCategoryForUpdateSpecification : CompositeSpecification<Category>
{
	readonly IsValidCategoryNameSpecification _isValidCategoryNameSpecification = new();
	readonly IsValidCategoryDescriptionSpecification _isValidCategoryDescriptionSpecification = new();
	readonly IsValidCategoryUpdateAuditSpecification _isValidCategoryUpdateAuditSpecification = new();
	public override void ConfigureSpecifications()
	{
		throw new NotImplementedException();
	}
}
