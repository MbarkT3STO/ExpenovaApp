namespace ExpenseService.Domain.Specifications.IncomeSpecifications;

/// <summary>
/// Represents a specification that checks if an income amount is valid.
/// </summary>
public class IsValidIncomeAmountSpecification : Specification<Income>
{
    protected override void ConfigureRules()
    {
        AddRule(expense => expense.Amount > 0, "Amount must be greater than 0");
    }
}