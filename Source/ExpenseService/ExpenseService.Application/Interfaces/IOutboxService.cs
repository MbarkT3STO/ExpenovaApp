using System.Linq.Expressions;
using ExpenseService.Infrastructure.Data.Entities;
using Messages.Abstractions;

namespace ExpenseService.Application.Interfaces;

/// <summary>
/// Represents a service for managing outbox messages.
/// </summary>
public interface IOutboxService
{
	/// <summary>
	/// Saves an outbox message to the specified queue.
	/// </summary>
	/// <typeparam name="T">The type of the message.</typeparam>
	/// <param name="message">The message to be saved.</param>
	/// <param name="queueName">The name of the queue.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	Task SaveMessageAsync<T>(T message, string queueName, CancellationToken cancellationToken) where T : BaseEventMessage;

	/// <summary>
	/// Processes an event asynchronously.
	/// </summary>
	/// <param name="action">The action to be performed.</param>
	Task ProcessEventAsync(Expression<Func<Task>> action);

	/// <summary>
	/// Checks if all messages related to the specified event have been processed.
	/// </summary>
	/// <param name="eventId">The ID of the event.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	Task<bool> HasProcessed(Guid eventId, CancellationToken cancellationToken = default);

	/// <summary>
	/// Checks if a message with the specified ID has been processed.
	/// </summary>
	/// <param name="messageId">The ID of the message to check.</param>
	/// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
	Task<bool> HasProcessed(int messageId, CancellationToken cancellationToken = default);

	/// <summary>
	/// Retrieves a collection of unprocessed outbox messages asynchronously.
	/// </summary>
	/// <param name="cancellationToken">The cancellation token.</param>
	Task<IEnumerable<OutboxMessage>> GetUnprocessedOutboxMessagesAsync(CancellationToken cancellationToken = default);

	/// <summary>
	/// Marks all messages related to the specified event as processed.
	/// </summary>
	/// <param name="eventId">The ID of the event.</param>
	Task MarkAsProcessedAsync(Guid eventId, CancellationToken cancellationToken = default);

	/// <summary>
	/// Marks a message as processed asynchronously.
	/// </summary>
	/// <param name="messageId">The ID of the message to mark as processed.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	Task MarkAsProcessedAsync(int messageId, CancellationToken cancellationToken = default);
}