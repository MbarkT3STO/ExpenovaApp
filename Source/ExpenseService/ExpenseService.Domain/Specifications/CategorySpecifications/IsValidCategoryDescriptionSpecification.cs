using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ExpenseService.Domain.Specifications.CategorySpecifications;

/// <summary>
/// Represents a specification that checks if a category description is valid.
/// </summary>
public class IsValidCategoryDescriptionSpecification : Specification<Category>
{
	protected override void ConfigureConditions()
	{
		AddCondition(category => !string.IsNullOrEmpty(category.Description), "Category description is invalid.");
		AddCondition(category => !string.IsNullOrWhiteSpace(category.Description), "Category description is invalid.");
		AddCondition(category => category.Description.Length <= 255, "Category description cannot be longer than 255 characters.");
	}
}
