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
	DateTime CreatedDate { get; set; }
	string CreatedBy { get; set; }
	DateTime? LastUpdatedDate { get; set; }
	string LastUpdatedBy { get; set; }
	bool IsDeleted { get; set; }
}
