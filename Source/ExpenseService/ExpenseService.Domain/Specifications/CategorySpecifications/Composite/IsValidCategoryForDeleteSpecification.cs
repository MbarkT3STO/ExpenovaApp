namespace ExpenseService.Domain.Specifications.CategorySpecifications.Composite;

/// <summary>
/// Represents a specification that determines if a category is valid for deletion.
/// </summary>
public class IsValidCategoryForDeleteSpecification : CompositeSpecification<Category>
{
	readonly IsValidCategoryCreationAuditSpecification _isValidCategoryCreationAuditSpecification;
	readonly IsValidCategoryDeleteAuditSpecification _isValidCategoryDeleteAuditSpecification;

	public IsValidCategoryForDeleteSpecification(IsValidCategoryCreationAuditSpecification isValidCategoryCreationAuditSpecification, IsValidCategoryDeleteAuditSpecification isValidCategoryDeleteAuditSpecification): base(false)
	{
		_isValidCategoryCreationAuditSpecification = isValidCategoryCreationAuditSpecification;
		_isValidCategoryDeleteAuditSpecification = isValidCategoryDeleteAuditSpecification;

		ConfigureSpecifications();
	}

	public override void ConfigureSpecifications()
	{
		AddSpecification(_isValidCategoryCreationAuditSpecification);
		AddSpecification(_isValidCategoryDeleteAuditSpecification);
	}
}
