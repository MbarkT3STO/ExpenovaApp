namespace ExpenseService.Domain.Specifications.ExpenseSpecifications;

/// <summary>
/// Represents a specification that checks if an expense has a valid user.
/// </summary>
public class IsValidExpenseUserSpecification : Specification<Expense>
{
    protected override void ConfigureRules()
    {
        AddRule(entity => entity.User != null, "Invalid user for expense");
        AddRule(entity => entity.User.Id != null, "User id must not be null for expense");
        AddRule(entity => entity.User.Id != string.Empty, "User id must not be empty for expense");
        AddRule(entity => entity.User.Id.Length <= 50, "User id must not be longer than 50 characters for expense");
    }
}
