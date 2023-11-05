using ExpenseService.Domain.Shared.Interfaces;

namespace ExpenseService.Application.Extensions;

/// <summary>
/// Provides extension methods for <see cref="IAuditableEntity"/> to write audit information.
/// </summary>
public static class AuditableEntitiesExtensions
{
	/// <summary>
	/// Writes the created audit information for an <see cref="IAuditableEntity"/>.
	/// </summary>
	/// <param name="entity">The entity to write the audit information for.</param>
	/// <param name="createdBy">The name of the user who created the entity.</param>
	public static void WriteCreatedAudit(this IAuditableEntity entity, string createdBy)
	{
		entity.CreatedAt = DateTime.UtcNow;
		entity.CreatedBy = createdBy;
	}


	/// <summary>
	/// Writes the created audit information to the auditable entity.
	/// </summary>
	/// <param name="entity">The auditable entity.</param>
	/// <param name="createdBy">The user who created the entity.</param>
	/// <param name="createdAt">The date and time when the entity was created.</param>
	public static void WriteCreatedAudit(this IAuditableEntity entity, string createdBy, DateTime createdAt)
	{
		entity.CreatedAt = createdAt;
		entity.CreatedBy = createdBy;
	}
}
