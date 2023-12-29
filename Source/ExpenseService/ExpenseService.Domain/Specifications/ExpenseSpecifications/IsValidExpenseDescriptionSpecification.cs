namespace ExpenseService.Domain.Specifications.ExpenseSpecifications;

public class IsValidExpenseDescriptionSpecification : Specification<Expense>
{
    protected override void ConfigureRules()
    {
        AddRule(entity => entity.Description != null, "Description must not be null for expense");
        AddRule(entity => entity.Description != string.Empty, "Description must not be empty for expense");
        AddRule(entity => entity.Description.Length <= 50, "Description must not be longer than 100 characters for expense");
    }
}
