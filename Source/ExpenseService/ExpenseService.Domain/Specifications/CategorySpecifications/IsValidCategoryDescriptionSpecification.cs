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
    public override Expression<Func<Category, bool>> ToExpression()
    {
        return category => !string.IsNullOrWhiteSpace(category.Description)
                           && category.Description.Length <= 255;
    }
}
