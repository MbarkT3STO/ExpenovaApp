using System.Linq.Expressions;

namespace ExpenseService.Domain.Specifications.CategorySpecifications;

/// <summary>
/// Represents a specification for validating if a category is valid for creation.
/// </summary>
public class IsValidCategoryForCreateSpecification: Specification<Category>
{
    protected override string UnSatisfiedSpecificationErrorMessage => $"Category is invalid for creation. {GetError().Message}";

    public override Expression<Func<Category, bool>> ToExpression()
	{
		var isValidCategoryNameSpecification          = new IsValidCategoryNameSpecification();
		var isValidCategoryDescriptionSpecification   = new IsValidCategoryDescriptionSpecification();
		var isValidCategoryCreationAuditSpecification = new IsValidCategoryCreationAuditSpecification();
		var globalCategorySpecification               = isValidCategoryDescriptionSpecification.And(isValidCategoryNameSpecification).And(isValidCategoryCreationAuditSpecification);
		
		return globalCategorySpecification.ToExpression();
	}
}
