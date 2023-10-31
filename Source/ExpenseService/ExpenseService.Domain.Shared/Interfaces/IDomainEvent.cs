using MediatR;

namespace ExpenseService.Domain.Shared.Interfaces;

/// <summary>
/// Represents a domain event.
/// </summary>
public interface IDomainEvent<T> : INotification 
{
	/// <summary>
	/// Gets the details of the domain event.
	/// </summary>
	IDomainEventDetails EventDetails { get; set; }
	
	/// <summary>
	/// Gets the event data associated with the domain event.
	/// </summary>
	T EventData { get; set; }
}
