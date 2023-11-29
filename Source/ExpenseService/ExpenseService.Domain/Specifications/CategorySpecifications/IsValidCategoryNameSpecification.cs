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
    public override Expression<Func<Category, bool>> ToExpression()
    {
        return category => !string.IsNullOrWhiteSpace(category.Name)
                           && category.Name.Length <= 50;
    }
}
