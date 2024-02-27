namespace ExpenseService.Domain.Specifications.IncomeSpecifications;

/// <summary>
/// Represents a specification that checks if an income user is valid.
/// </summary>
public class IsValidIncomeUserSpecification : Specification<Income>
{
    protected override void ConfigureRules()
    {
        AddRule(entity => entity.User != null, "Invalid user for income");
        AddRule(entity => entity.User.Id != null, "User id must not be null for income");
        AddRule(entity => entity.User.Id != string.Empty, "User id must not be empty for income");
        AddRule(entity => entity.User.Id.Length <= 50, "User id must not be longer than 50 characters for income");
    }
}
