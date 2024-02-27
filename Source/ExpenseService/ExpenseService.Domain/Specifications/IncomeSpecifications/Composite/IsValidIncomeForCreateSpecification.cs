using ExpenseService.Domain.Specifications.Shared;

namespace ExpenseService.Domain.Specifications.IncomeSpecifications.Composite;

/// <summary>
/// Represents a specification that checks if an income is valid for creation.
/// </summary>
public class IsValidIncomeForCreateSpecification : CompositeSpecification<Income>
{
	public IsValidIncomeForCreateSpecification() : base( false )
	{
		ConfigureSpecifications();
	}

	public override void ConfigureSpecifications()
	{
		AddSpecification(new IsValidAggregateRootCreationAuditSpecification<Income, Guid>());
		AddSpecification(new IsValidIncomeAmountSpecification());
		AddSpecification(new IsValidIncomeDateSpecification());

		AddSpecification(new IsValidIncomeUserSpecification());
		AddSpecification(new IsValidIncomeCategorySpecification());
		AddSpecification(new IsValidIncomeDescriptionSpecification());
	}
}
