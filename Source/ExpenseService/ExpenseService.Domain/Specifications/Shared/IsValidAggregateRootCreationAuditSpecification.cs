namespace ExpenseService.Domain.Specifications.Shared;

/// <summary>
/// Represents a specification that validates the creation audit properties of an entity.
/// </summary>
/// <typeparam name="T">The type of entity to validate.</typeparam>
public class IsValidAggregateRootCreationAuditSpecification<T, TId> : Specification<T> where T : AuditableAggregateRoot<TId>
{
    protected override void ConfigureRules()
    {
        AddRule(entity => entity.CreatedBy != null, "Created by must not be null");
        AddRule(entity => entity.CreatedBy != string.Empty, "Created by must not be empty");
        AddRule(entity => entity.CreatedBy.Length <= 50, "Created by must not be longer than 50 characters");

        AddRule(entity => entity.CreatedAt != null, "Created at must not be null");
        AddRule(entity => entity.CreatedAt != DateTime.MinValue, "Created at must not be DateTime.MinValue");
        AddRule(entity => entity.CreatedAt != DateTime.MaxValue, "Created at must not be DateTime.MaxValue");
        AddRule(entity => entity.CreatedAt.Date <= DateTime.UtcNow.Date, "Created at must be in the past");
    }
}
