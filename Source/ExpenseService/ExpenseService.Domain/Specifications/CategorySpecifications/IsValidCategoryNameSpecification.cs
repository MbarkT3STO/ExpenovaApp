using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ExpenseService.Domain.Specifications.CategorySpecifications;

/// <summary>
/// Represents a specification that checks if a category name is valid.
/// </summary>
public class IsValidCategoryNameSpecification : Specification<Category>
{
    protected override void ConfigureConditions()
    {
        AddCondition(category => !string.IsNullOrWhiteSpace(category.Name), "Category name cannot be empty.");
        AddCondition(category => category.Name.Length <= 50, "Category name cannot be longer than 50 characters.");
    }
}
