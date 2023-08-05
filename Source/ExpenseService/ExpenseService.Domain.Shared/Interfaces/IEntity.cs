using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseService.Domain.Shared.Interfaces;

/// <summary>
/// Represents an entity.
/// </summary>
public interface IEntity<T>
{
	T Id { get; }
}
