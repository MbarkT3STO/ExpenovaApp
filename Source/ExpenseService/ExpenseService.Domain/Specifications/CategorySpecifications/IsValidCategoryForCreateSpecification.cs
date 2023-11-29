using System.Linq.Expressions;

namespace ExpenseService.Domain.Specifications.CategorySpecifications;

/// <summary>
/// Represents a specification for validating if a category is valid for creation.
/// </summary>
public class IsValidCategoryForCreateSpecification : Specification<Category>
{
	public override Expression<Func<Category, bool>> ToExpression()
	{
		var isValidCategoryNameSpecification = new IsValidCategoryNameSpecification();
		var isValidCategoryDescriptionSpecification = new IsValidCategoryDescriptionSpecification();
		var globalCategorySpecification = isValidCategoryDescriptionSpecification.And(isValidCategoryNameSpecification);
		
		// return category => category.Id == Guid.Empty
		//                    && !string.IsNullOrWhiteSpace(category.Name)
		//                    && category.Name.Length <= 50
		//                    && !string.IsNullOrWhiteSpace(category.Description)
		//                    && category.Description.Length <= 500
		//                    && category.CreatedAt != default
		//                    && category.CreatedBy != Guid.Empty
		//                    && category.LastUpdatedAt == default
		//                    && category.LastUpdatedBy == Guid.Empty
		//                    && category.DeletedAt == default
		//                    && category.DeletedBy == Guid.Empty;
		
	}
}
