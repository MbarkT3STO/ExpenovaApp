using System.Linq.Expressions;
using ExpenseService.Infrastructure.Data.Entities;
using Messages.Abstractions;

namespace ExpenseService.Application.Interfaces;

/// <summary>
/// Represents a service for managing outbox messages.
/// </summary>
public interface IOutboxService /*: IDisposable, IAsyncDisposable*/
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
	/// Saves a message if it does not already exist in the outbox.
	/// </summary>
	/// <typeparam name="T">The type of the message.</typeparam>
	/// <param name="message">The message to save.</param>
	/// <param name="queueName">The name of the queue.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	Task SaveMessageIfNotExistsAsync<T>(T message, string queueName, CancellationToken cancellationToken) where T : BaseEventMessage;

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
	Task<IEnumerable<OutboxMessage>> GetUnprocessedMessagesAsync(CancellationToken cancellationToken = default);

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


	/// <summary>
	/// Checks if a message with the specified ID exists in the outbox.
	/// </summary>
	/// <param name="messageId">The ID of the message to check.</param>
	/// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
	Task<bool> IsMessageExistsAsync(int messageId, CancellationToken cancellationToken = default);


	/// <summary>
	/// Checks if a message with the specified event ID exists in the specified queue.
	/// </summary>
	/// <param name="eventId">The ID of the event.</param>
	/// <param name="queueName">The name of the queue.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	Task<bool> IsMessageExistsAsync(Guid eventId, string queueName, CancellationToken cancellationToken = default);


	/// <summary>
	/// Checks if a message with the specified event ID exists in the specified queue and has not been processed.
	/// </summary>
	/// <param name="eventId">The ID of the event.</param>
	/// <param name="queueName">The name of the queue.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	Task<bool> IsMessageExistsAndNotProcessedAsync(Guid eventId, string queueName, CancellationToken cancellationToken = default);


	/// <summary>
	/// Checks if a message with the specified event ID, queue name, and body exists in the outbox and has not been processed.
	/// </summary>
	/// <param name="eventId">The ID of the event.</param>
	/// <param name="queueName">The name of the queue.</param>
	/// <param name="body">The body of the message.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>A task that represents the asynchronous operation. The task result contains a boolean value indicating whether the message exists and has not been processed.</returns>
	Task<bool> IsMessageExistsAndNotProcessedAsync(Guid eventId, string queueName, string body, CancellationToken cancellationToken = default);


	/// <summary>
	/// Checks if a message with the specified event ID exists in the specified queue and has not been processed.
	/// </summary>
	/// <typeparam name="T">The type of the message.</typeparam>
	/// <param name="eventId">The ID of the event.</param>
	/// <param name="queueName">The name of the queue.</param>
	/// <param name="message">The message to check.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	Task<bool> IsMessageExistsAndNotProcessedAsync<T>(Guid eventId, string queueName, T message, CancellationToken cancellationToken = default) where T : BaseEventMessage;


	/// <summary>
	/// Purges all processed messages from the outbox.
	/// </summary>
	Task PurgeProcessedMessagesAsync(CancellationToken cancellationToken = default);






	/// <summary>
	/// Serializes the specified message.
	/// </summary>
	/// <param name="message">The message to be serialized.</param>
	/// <returns>The serialized message.</returns>
	string SerializeMessage(object message);

	/// <summary>
	/// Deserializes a message object from a string representation.
	/// </summary>
	/// <param name="message">The string representation of the message.</param>
	/// <returns>The deserialized message object.</returns>
	object DeserializeMessage(string message);
}