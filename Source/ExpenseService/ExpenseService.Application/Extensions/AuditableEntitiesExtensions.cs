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
	/// <param name="createdBy">The used ID of the user who created the entity.</param>
	public static void WriteCreatedAudit(this IAuditableEntity entity, string createdBy)
	{
		entity.CreatedAt = DateTime.UtcNow;
		entity.CreatedBy = createdBy;
	}


	/// <summary>
	/// Writes the created audit information to the auditable entity.
	/// </summary>
	/// <param name="entity">The auditable entity.</param>
	/// <param name="createdBy">The user ID of the user who created the entity.</param>
	/// <param name="createdAt">The date and time when the entity was created.</param>
	public static void WriteCreatedAudit(this IAuditableEntity entity, string createdBy, DateTime createdAt)
	{
		entity.CreatedAt = createdAt;
		entity.CreatedBy = createdBy;
	}



	/// <summary>
	/// Writes the updated audit information for an auditable entity.
	/// </summary>
	/// <param name="entity">The auditable entity.</param>
	/// <param name="updatedBy">The user who updated the entity.</param>
	public static void WriteUpdatedAudit(this IAuditableEntity entity, string updatedBy)
	{
		entity.LastUpdatedAt = DateTime.UtcNow;
		entity.LastUpdatedBy = updatedBy;
	}


	/// <summary>
	/// Writes the updated audit information for an <see cref="IAuditableEntity"/>.
	/// </summary>
	/// <param name="entity">The entity to write the audit information for.</param>
	/// <param name="updatedBy">The used ID of the user who updated the entity.</param>
	/// <param name="updatedAt">The date and time when the entity was updated.</param>
	public static void WriteUpdatedAudit(this IAuditableEntity entity, string updatedBy, DateTime updatedAt)
	{
		entity.LastUpdatedAt = updatedAt;
		entity.LastUpdatedBy = updatedBy;
	}



	/// <summary>
	/// Writes Deleted audit information for an <see cref="IAuditableEntity"/>.
	/// </summary>
	/// <param name="entity">The entity to write the audit information for.</param>
	/// <param name="deletedBy">The used ID of the user who deleted the entity.</param>
	/// <param name="deletedAt">The date and time when the entity was deleted.</param>
	public static void WriteDeletedAudit(this IAuditableEntity entity, string deletedBy, DateTime deletedAt)
	{
		entity.DeletedAt = deletedAt;
		entity.DeletedBy = deletedBy;
	}


	/// <summary>
	/// Writes Deleted audit information for an <see cref="IAuditableEntity"/>.
	/// </summary>
	/// <param name="entity">The entity to write the audit information for.</param>
	/// <param name="deletedBy">The used ID of the user who deleted the entity.</param>
	public static void WriteDeletedAudit(this IAuditableEntity entity, string deletedBy)
	{
		entity.DeletedAt = DateTime.UtcNow;
		entity.DeletedBy = deletedBy;
	}
}
