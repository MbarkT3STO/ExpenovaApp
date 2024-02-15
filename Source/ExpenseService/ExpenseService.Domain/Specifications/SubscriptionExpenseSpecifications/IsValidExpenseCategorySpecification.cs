namespace ExpenseService.Domain.Specifications.SubscriptionExpenseSpecifications;

/// <summary>
/// Represents a specification that checks if an expense has a valid category.
/// </summary>
public class IsValidSubscriptionExpenseCategorySpecification : Specification<SubscriptionExpense>
{
    protected override void ConfigureRules()
    {
        AddRule(entity => entity.Category != null, "Invalid category for subscription expense");
        AddRule(entity => entity.Category.Id != null, "Category id must not be null for subscription expense");
        AddRule(entity => entity.Category.Id != Guid.Empty, "Category id must not be empty for subscription expense");
    }
}