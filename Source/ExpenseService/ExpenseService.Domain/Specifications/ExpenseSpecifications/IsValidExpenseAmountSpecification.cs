namespace ExpenseService.Domain.Specifications.ExpenseSpecifications;

/// <summary>
/// Represents a specification that checks if an expense amount is valid.
/// </summary>
public class IsValidExpenseAmountSpecification : Specification<Expense>
{
    protected override void ConfigureRules()
    {
        AddRule(expense => expense.Amount > 0, "Amount must be greater than 0");
    }
}