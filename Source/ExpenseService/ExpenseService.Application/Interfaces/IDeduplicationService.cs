using System.Linq.Expressions;

namespace ExpenseService.Application.Interfaces;

/// <summary>
/// Represents a service that checks for and processes duplicate Domain Events.
/// </summary>
public interface IDomainEventDeduplicationService
{
	/// <summary>
	/// Checks if an Event has been processed.
	/// </summary>
	/// <param name="eventId">The ID of the Event to check.</param>
	Task<bool> HasProcessed(Guid eventId);


	/// <summary>
	/// Processes an event asynchronously.
	/// </summary>
	/// <param name="processEventFunc">The function that processes the event.</param>
	Task ProcessEventAsync(Expression<Func<Task>> processEventFunc);
}
