namespace ExpenseService.Domain.Specifications.CategorySpecifications.Composite;

/// <summary>
/// Represents a specification that determines if a category is valid for update.
/// </summary>
public class IsValidCategoryForUpdateSpecification: CompositeSpecification<Category>
{
	readonly IsValidCategoryNameSpecification _isValidCategoryNameSpecification;
	readonly IsValidCategoryDescriptionSpecification _isValidCategoryDescriptionSpecification;
	readonly IsValidCategoryUpdateAuditSpecification _isValidCategoryUpdateAuditSpecification;
	readonly IsUniqueCategoryNameSpecification _isUniqueCategoryNameSpecification;

	public IsValidCategoryForUpdateSpecification(IsValidCategoryNameSpecification isValidCategoryNameSpecification, IsValidCategoryDescriptionSpecification isValidCategoryDescriptionSpecification, IsValidCategoryUpdateAuditSpecification isValidCategoryUpdateAuditSpecification, IsUniqueCategoryNameSpecification isUniqueCategoryNameSpecification): base(false)
	{
		_isValidCategoryNameSpecification        = isValidCategoryNameSpecification;
		_isValidCategoryDescriptionSpecification = isValidCategoryDescriptionSpecification;
		_isValidCategoryUpdateAuditSpecification = isValidCategoryUpdateAuditSpecification;
		_isUniqueCategoryNameSpecification       = isUniqueCategoryNameSpecification;

		ConfigureSpecifications();
	}

	public override void ConfigureSpecifications()
	{
		AddSpecification(_isValidCategoryNameSpecification);
		AddSpecification(_isValidCategoryDescriptionSpecification);
		AddSpecification(_isValidCategoryUpdateAuditSpecification);
		AddSpecification(_isUniqueCategoryNameSpecification);
	}
}
