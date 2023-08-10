using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseService.Domain.Shared.Entities;

/// <summary>
/// Represents the base class for all aggregate root entities.
/// </summary>
/// <typeparam name="T">The type of the entity identifier.</typeparam>
public abstract class AggregateRoot<T> : Entity<T>
{
	
}
