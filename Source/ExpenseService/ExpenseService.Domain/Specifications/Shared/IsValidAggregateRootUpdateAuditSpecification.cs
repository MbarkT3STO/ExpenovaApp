namespace ExpenseService.Domain.Specifications.Shared;

/// <summary>
/// Represents a specification that validates the update audit properties of an aggregate root entity.
/// </summary>
/// <typeparam name="T">The type of the aggregate root entity.</typeparam>
/// <typeparam name="TId">The type of the aggregate root entity's identifier.</typeparam>
public class IsValidAggregateRootUpdateAuditSpecification<T, TId> : Specification<T> where T : AuditableAggregateRoot<TId>
{
    protected override void ConfigureRules()
    {
        AddRule(entity => entity.LastUpdatedBy != null, "Last updated by must not be null");
        AddRule(entity => entity.LastUpdatedBy != string.Empty, "Last updated by must not be empty");
        AddRule(entity => entity.LastUpdatedBy.Length <= 50, "Last updated by must not be longer than 50 characters");

        AddRule(entity => entity.LastUpdatedAt != null, "Last updated at must not be null");
        AddRule(entity => entity.LastUpdatedAt != DateTime.MinValue, "Last updated at must not be DateTime.MinValue");
        AddRule(entity => entity.LastUpdatedAt != DateTime.MaxValue, "Last updated at must not be DateTime.MaxValue");
        AddRule(entity => entity.LastUpdatedAt.Value.Date <= DateTime.UtcNow.Date, "Last updated at must be in the past");
    }
}