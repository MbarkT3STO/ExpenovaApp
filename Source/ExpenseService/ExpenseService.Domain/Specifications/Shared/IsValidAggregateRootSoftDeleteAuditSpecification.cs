namespace ExpenseService.Domain.Specifications.Shared;

/// <summary>
/// Represents a specification that checks if an entity of type <typeparamref name="T"/> is a valid aggregate root with soft delete and audit functionality.
/// </summary>
/// <typeparam name="T">The type of the entity.</typeparam>
/// <typeparam name="TId">The type of the entity's identifier.</typeparam>
public class IsValidAggregateRootSoftDeleteAuditSpecification<T, TId>: Specification<T> where T: AuditableAggregateRoot<TId>
{
	protected override void ConfigureRules()
	{
		AddRule(entity => entity.IsDeleted , "Is deleted must be true");
		AddRule(entity => entity.DeletedBy != null, "Deleted by must not be null");
		AddRule(entity => entity.DeletedAt != null, "Deleted at must not be null");
	}
}