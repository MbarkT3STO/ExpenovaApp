namespace ExpenseService.Domain.Specifications.CategorySpecifications;

/// <summary>
/// Represents a specification for validating if a category is valid for creation.
/// </summary>
public class IsValidCategoryForCreateSpecification: CompositeSpecification<Category>
{
	readonly IsValidCategoryNameSpecification          _isValidCategoryNameSpecification          = new();
	readonly IsValidCategoryDescriptionSpecification   _isValidCategoryDescriptionSpecification   = new();
	readonly IsValidCategoryCreationAuditSpecification _isValidCategoryCreationAuditSpecification = new();
	
	public override void ConfigureSpecifications()
	{
		AddSpecification(_isValidCategoryNameSpecification);
		AddSpecification(_isValidCategoryDescriptionSpecification);
		AddSpecification(_isValidCategoryCreationAuditSpecification);
	}
}
