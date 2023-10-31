namespace ExpenseService.Domain.Shared.Interfaces;

/// <summary>
/// Represents the details of a domain event.
/// </summary>
public interface IDomainEventDetails
{
	/// <summary>
	/// The event id.
	/// </summary>
	Guid EventId { get; }
	
	/// <summary>
	/// The event name.
	/// </summary>
	string EventName { get; }
	
	/// <summary>
	/// The date and time the event occurred.
	/// </summary>
	DateTime OccurredOn { get; }

	/// <summary>
	/// Gets the ID of the user who caused the domain event to occur.
	/// </summary>
	string OccurredBy { get; }
}
