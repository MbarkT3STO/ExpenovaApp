namespace ExpenseService.Domain.Specifications.Shared;

public class IsValidAggregateRootSoftDeleteAuditSpecification<T, TId>: Specification<T> where T: AuditableAggregateRoot<TId>
{
	protected override void ConfigureRules()
	{
		AddRule(entity => entity.IsDeleted , "Is deleted must be true");
	}
}