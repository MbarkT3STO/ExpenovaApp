using ExpenseService.Domain.Specifications.Shared;

namespace ExpenseService.Domain.Specifications.IncomeSpecifications.Composite;

/// <summary>
/// Represents a specification that checks if an income is valid for update.
/// </summary>
public class IsValidIncomeForUpdateSpecification : CompositeSpecification<Income>
{
	public IsValidIncomeForUpdateSpecification() : base(false)
	{
		ConfigureSpecifications();
	}

	public override void ConfigureSpecifications()
	{
		AddSpecification(new IsValidIncomeDateSpecification());
		AddSpecification(new IsValidIncomeDescriptionSpecification());
		AddSpecification(new IsValidIncomeAmountSpecification());

		AddSpecification(new IsValidIncomeUserSpecification());
		AddSpecification(new IsValidIncomeCategorySpecification());

		AddSpecification(new IsValidAggregateRootUpdateAuditSpecification<Income, Guid>());

	}
}
