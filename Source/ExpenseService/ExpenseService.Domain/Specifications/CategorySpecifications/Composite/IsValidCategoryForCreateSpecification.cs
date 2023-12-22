namespace ExpenseService.Domain.Specifications.CategorySpecifications;

/// <summary>
/// Represents a specification for validating if a category is valid for creation.
/// </summary>
public class IsValidCategoryForCreateSpecification: CompositeSpecification<Category>
{

	readonly IsValidCategoryNameSpecification _isValidCategoryNameSpecification;
	readonly IsValidCategoryDescriptionSpecification _isValidCategoryDescriptionSpecification;
	readonly IsValidCategoryCreationAuditSpecification _isValidCategoryCreationAuditSpecification;
	readonly IsUniqueCategoryNameSpecification _isUniqueCategoryNameSpecification;


	public IsValidCategoryForCreateSpecification(IsValidCategoryNameSpecification isValidCategoryNameSpecification, IsValidCategoryDescriptionSpecification isValidCategoryDescriptionSpecification, IsValidCategoryCreationAuditSpecification isValidCategoryCreationAuditSpecification, IsUniqueCategoryNameSpecification isUniqueCategoryNameSpecification)
	{
		ShouldConfigureSpecificationsFromTheBase = false;

		_isValidCategoryNameSpecification          = isValidCategoryNameSpecification;
		_isValidCategoryDescriptionSpecification   = isValidCategoryDescriptionSpecification;
		_isValidCategoryCreationAuditSpecification = isValidCategoryCreationAuditSpecification;
		_isUniqueCategoryNameSpecification         = isUniqueCategoryNameSpecification;

		ConfigureSpecifications();
	}



	public override void ConfigureSpecifications()
	{
		AddSpecification(_isValidCategoryNameSpecification);
		AddSpecification(_isValidCategoryDescriptionSpecification);
		AddSpecification(_isValidCategoryCreationAuditSpecification);
		AddSpecification(_isUniqueCategoryNameSpecification);
	}
}
