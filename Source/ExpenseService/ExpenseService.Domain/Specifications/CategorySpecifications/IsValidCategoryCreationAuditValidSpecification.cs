using System.Linq.Expressions;

namespace ExpenseService.Domain.Specifications.CategorySpecifications;

/// <summary>
/// Represents a specification for validating the creation audit of a category.
/// </summary>
public class IsValidCategoryCreationAuditValidSpecification : Specification<Category>
{
    public override Expression<Func<Category, bool>> ToExpression()
    {
        return category => category.CreatedAt != default && category.CreatedBy != Guid.Empty.ToString();
    }
}
