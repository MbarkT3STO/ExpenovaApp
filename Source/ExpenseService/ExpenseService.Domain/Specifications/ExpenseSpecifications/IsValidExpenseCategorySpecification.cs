namespace ExpenseService.Domain.Specifications.ExpenseSpecifications;

/// <summary>
/// Represents a specification that checks if an expense has a valid category.
/// </summary>
public class IsValidExpenseCategorySpecification : Specification<Expense>
{
    protected override void ConfigureRules()
    {
        AddRule(entity => entity.Category != null, "Invalid category for expense");
        AddRule(entity => entity.Category.Id != null, "Category id must not be null for expense");
        AddRule(entity => entity.Category.Id != Guid.Empty, "Category id must not be empty for expense");
    }
}