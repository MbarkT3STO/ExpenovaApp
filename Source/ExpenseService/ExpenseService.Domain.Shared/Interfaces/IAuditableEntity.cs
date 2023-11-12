using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseService.Domain.Shared.Interfaces;

/// <summary>
/// Represents an entity that is auditable.
/// </summary>
public interface IAuditableEntity
{
	DateTime CreatedAt { get; set; }
	string CreatedBy { get; set; }
	DateTime? LastUpdatedAt { get; set; }
	string LastUpdatedBy { get; set; }
	bool IsDeleted { get; set; }
	DateTime? DeletedAt { get; set; }
	string DeletedBy { get; set; }
}
