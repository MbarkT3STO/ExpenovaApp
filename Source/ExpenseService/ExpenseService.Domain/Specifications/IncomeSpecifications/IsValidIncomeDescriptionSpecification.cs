namespace ExpenseService.Domain.Specifications.IncomeSpecifications;

public class IsValidIncomeDescriptionSpecification : Specification<Income>
{
    protected override void ConfigureRules()
    {
        AddRule(entity => entity.Description != null, "Description must not be null for income");
        AddRule(entity => entity.Description != string.Empty, "Description must not be empty for income");
        AddRule(entity => entity.Description.Length <= 50, "Description must not be longer than 100 characters for income");
    }
}
