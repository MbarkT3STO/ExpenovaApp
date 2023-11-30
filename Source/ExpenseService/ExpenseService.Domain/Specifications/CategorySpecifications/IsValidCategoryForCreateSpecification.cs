using System.Linq.Expressions;
using ExpenseService.Domain.Shared.Common;

namespace ExpenseService.Domain.Specifications.CategorySpecifications;

/// <summary>
/// Represents a specification for validating if a category is valid for creation.
/// </summary>
public class IsValidCategoryForCreateSpecification: CompositeSpecification<Category>
{
	IsValidCategoryNameSpecification          _isValidCategoryNameSpecification          = new();
	IsValidCategoryDescriptionSpecification   _isValidCategoryDescriptionSpecification   = new();
	IsValidCategoryCreationAuditSpecification _isValidCategoryCreationAuditSpecification = new();


    /// <inheritdoc cref="CompositeSpecification{T}.ToExpression"/>
    public override Expression<Func<Category, bool>> ToExpression()
	{
		var isValidCategoryForCreateSpecification = _isValidCategoryNameSpecification.And(_isValidCategoryDescriptionSpecification).And(_isValidCategoryCreationAuditSpecification);

		var expression = isValidCategoryForCreateSpecification.ToExpression();
		
		return expression;
	}


	/// <inheritdoc cref="CompositeSpecification{T}.GetError"/>
	public override Error GetError()
	{
		var isValidCategoryNameSpecification          = new IsValidCategoryNameSpecification();
		var isValidCategoryDescriptionSpecification   = new IsValidCategoryDescriptionSpecification();
		var isValidCategoryCreationAuditSpecification = new IsValidCategoryCreationAuditSpecification();

		var isValidCategoryForCreateSpecification = isValidCategoryNameSpecification.And(isValidCategoryDescriptionSpecification).And(isValidCategoryCreationAuditSpecification);

		return isValidCategoryForCreateSpecification.GetError();
	}
}
