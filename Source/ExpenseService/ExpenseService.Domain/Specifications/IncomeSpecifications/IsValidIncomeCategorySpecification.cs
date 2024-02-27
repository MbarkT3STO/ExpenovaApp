namespace ExpenseService.Domain.Specifications.IncomeSpecifications;

/// <summary>
/// Represents a specification that checks if an income category is valid.
/// </summary>
public class IsValidIncomeCategorySpecification : Specification<Income>
{
    protected override void ConfigureRules()
    {
        AddRule(entity => entity.Category != null, "Invalid category for expense");
        AddRule(entity => entity.Category.Id != null, "Category id must not be null for expense");
        AddRule(entity => entity.Category.Id != Guid.Empty, "Category id must not be empty for expense");
    }
}