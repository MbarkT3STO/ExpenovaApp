namespace ExpenseService.Domain.Specifications.SubscriptionExpenseSpecifications;

public class IsValidSubscriptionExpenseDescriptionSpecification : Specification<SubscriptionExpense>
{
    protected override void ConfigureRules()
    {
        AddRule(entity => entity.Description != null, "Description must not be null for subscription expense");
        AddRule(entity => entity.Description != string.Empty, "Description must not be empty for subscription expense");
        AddRule(entity => entity.Description.Length <= 50, "Description must not be longer than 100 characters for subscription expense");
    }
}
