using System.Linq.Expressions;
using ExpenseService.Domain.Shared.Common;

namespace ExpenseService.Domain.Specifications.CategorySpecifications;

/// <summary>
/// Represents a specification for validating if a category is valid for creation.
/// </summary>
public class IsValidCategoryForCreateSpecification: CompositeSpecification<Category>
{
	readonly IsValidCategoryNameSpecification          _isValidCategoryNameSpecification          = new();
	readonly IsValidCategoryDescriptionSpecification   _isValidCategoryDescriptionSpecification   = new();
	readonly IsValidCategoryCreationAuditSpecification _isValidCategoryCreationAuditSpecification = new();

	// protected override void ConfigureConditions()
	// {
	// 	var isValidCategoryNameConditions		  = _isValidCategoryNameSpecification.GetConditions();
	// 	var isValidCategoryDescriptionConditions = _isValidCategoryDescriptionSpecification.GetConditions();
	// 	var isValidCategoryCreationAuditConditions = _isValidCategoryCreationAuditSpecification.GetConditions();
		
	// 	var combinedConditions = isValidCategoryNameConditions
	// 		.Concat(isValidCategoryDescriptionConditions)
	// 		.Concat(isValidCategoryCreationAuditConditions);

		
	// 	foreach (var (condition, errorMessage) in combinedConditions)
	// 	{
	// 		AddCondition(condition, errorMessage);
	// 	}
	// }
	
	public override void ConfigureSpecifications()
	{
		AddSpecification(_isValidCategoryNameSpecification);
		AddSpecification(_isValidCategoryDescriptionSpecification);
		AddSpecification(_isValidCategoryCreationAuditSpecification);
	}
}
