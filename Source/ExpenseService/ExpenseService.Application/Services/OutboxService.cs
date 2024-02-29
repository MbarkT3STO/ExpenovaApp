using System.Linq.Expressions;
using ExpenseService.Application.Interfaces;
using ExpenseService.Infrastructure.Data.Entities;
using Messages.Abstractions;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ExpenseService.Application.Services;

/// <summary>
/// Represents a service for managing outbox messages.
/// </summary>
public class OutboxService: IOutboxService
{
	readonly AppDbContext _dbContext;

	public OutboxService(AppDbContext dbContext)
	{
		_dbContext = dbContext;
	}

	public async Task<IEnumerable<OutboxMessage>> GetUnprocessedMessagesAsync(CancellationToken cancellationToken = default)
	{
		var outboxMessages = await _dbContext.OutboxMessages.Where(x => !x.IsProcessed).AsNoTracking().ToListAsync(cancellationToken);

		return outboxMessages;
	}

	public async Task<bool> HasProcessed(Guid eventId, CancellationToken cancellationToken = default)
	{
		var hasProcessed = await _dbContext.OutboxMessages.AsNoTracking().AnyAsync(x => x.EventId == eventId && x.IsProcessed, cancellationToken);

		return hasProcessed;
	}

	public async Task<bool> HasProcessed(int messageId, CancellationToken cancellationToken = default)
	{
		var hasProcessed = await _dbContext.OutboxMessages.AsNoTracking().AnyAsync(x => x.Id == messageId && x.IsProcessed, cancellationToken);

		return hasProcessed;
	}

	public async Task<bool> IsMessageExistsAndNotProcessedAsync(Guid eventId, string queueName, CancellationToken cancellationToken = default)
	{
		var isExists = await _dbContext.OutboxMessages.AsNoTracking().AnyAsync(x => x.EventId == eventId && x.QueueName == queueName && !x.IsProcessed, cancellationToken);

		return isExists;
	}

	public async Task<bool> IsMessageExistsAndNotProcessedAsync(Guid eventId, string queueName, string body, CancellationToken cancellationToken = default)
	{
		var isExists = await _dbContext.OutboxMessages.AsNoTracking().AnyAsync(x => x.EventId == eventId && x.QueueName == queueName && x.Data == body && !x.IsProcessed, cancellationToken);

		return isExists;
	}

	public async Task<bool> IsMessageExistsAsync(int messageId, CancellationToken cancellationToken = default)
	{
		var isExists = await _dbContext.OutboxMessages.AsNoTracking().AnyAsync(x => x.Id == messageId, cancellationToken);

		return isExists;
	}

	public async Task<bool> IsMessageExistsAsync(Guid eventId, string queueName, CancellationToken cancellationToken = default)
	{
		var isExists = await _dbContext.OutboxMessages.AsNoTracking().AnyAsync(x => x.EventId == eventId && x.QueueName == queueName, cancellationToken);

		return isExists;
	}

	public async Task<bool> IsMessageExistsAndNotProcessedAsync<T>(Guid eventId, string queueName, T message, CancellationToken cancellationToken = default) where T : BaseEventMessage
	{
		var serializedMessage = SerializeMessage(message);
		var isExists          = await _dbContext.OutboxMessages.AsNoTracking().AnyAsync(x => x.EventId == eventId && x.QueueName == queueName && x.Data == serializedMessage && !x.IsProcessed, cancellationToken);

		return isExists;
	}

	public async Task MarkAsProcessedAsync(Guid eventId, CancellationToken cancellationToken = default)
	{
		var outboxMessages = await _dbContext.OutboxMessages.Where(x => x.EventId == eventId).ToListAsync(cancellationToken);

		outboxMessages.ForEach(x => x.IsProcessed = true);

		await _dbContext.SaveChangesAsync(cancellationToken);
	}

	public async Task MarkAsProcessedAsync(int messageId, CancellationToken cancellationToken = default)
	{
		var outboxMessage = await _dbContext.OutboxMessages.FirstOrDefaultAsync(x => x.Id == messageId, cancellationToken);

		if (outboxMessage != null)
		{
			outboxMessage.IsProcessed = true;

			_dbContext.OutboxMessages.Update(outboxMessage);
			await _dbContext.SaveChangesAsync(cancellationToken);
		}
	}

	public async Task ProcessEventAsync(Expression<Func<Task>> action)
	{
		await action.Compile().Invoke();
	}

	public async Task PurgeProcessedMessagesAsync(CancellationToken cancellationToken = default)
	{
		// Get the IDs of the processed messages
		// var processedMessageIds = _dbContext.OutboxMessages
		// 	.Where(x => x.IsProcessed)
		// 	.Select(x => x.Id)
		// 	.ToList();

		var processedMessages = _dbContext.OutboxMessages.Where(x => x.IsProcessed);

		// Detach all OutboxMessage entities
		foreach (var entry in _dbContext.ChangeTracker.Entries<OutboxMessage>())
		{
			entry.State = EntityState.Detached;
		}

		 _dbContext.RemoveRange(processedMessages);

		// For each ID, create a new OutboxMessage instance with that ID and mark it for deletion
		// foreach (var id in processedMessageIds)
		// {
		// 	var message =  await _dbContext.OutboxMessages.FindAsync(new object[] { id }, cancellationToken);

		// 	if (message != null)
		// 	{
		// 		_dbContext.OutboxMessages.Remove(message);
		// 	}
		// }

		// Save changes
		await _dbContext.SaveChangesAsync(cancellationToken);
	}

	public async Task SaveMessageAsync<T>(T message, string queueName, CancellationToken cancellationToken) where T: BaseEventMessage
	{
		var serializedMessage = SerializeMessage(message);
		var outboxEvent       = new OutboxMessage(message.EventId, message.EventName, serializedMessage, queueName);

		_dbContext.OutboxMessages.Add(outboxEvent);
		await _dbContext.SaveChangesAsync(cancellationToken);
	}





	public string SerializeMessage(object message)
	{
		var serializationSettings = new JsonSerializerSettings()
		{
			TypeNameHandling = TypeNameHandling.All
		};

		var serializedMessage = JsonConvert.SerializeObject(message, serializationSettings);

		return serializedMessage;
	}


	public object DeserializeMessage(string serializedMessage)
	{
		var deserializationSettings = new JsonSerializerSettings()
		{
			TypeNameHandling = TypeNameHandling.All
		};

		var deserializedMessage = JsonConvert.DeserializeObject(serializedMessage, deserializationSettings);

		return deserializedMessage;
	}

	public async Task SaveMessageIfNotExistsAsync<T>(T message, string queueName, CancellationToken cancellationToken) where T : BaseEventMessage
	{
		var serializedMessage = SerializeMessage(message);
		var isExists = await IsMessageExistsAndNotProcessedAsync(message.EventId, queueName, serializedMessage, cancellationToken);

		if (!isExists)
		{
			var outboxEvent = new OutboxMessage(message.EventId, message.EventName, serializedMessage, queueName);

			_dbContext.OutboxMessages.Add(outboxEvent);
			await _dbContext.SaveChangesAsync(cancellationToken);
		}
	}



	// public void Dispose()
	// {
	// 	var disposableDbContext = _dbContext as IDisposable;

	// 	disposableDbContext?.Dispose();

	// 	GC.SuppressFinalize(this);
	// }

	// public async ValueTask DisposeAsync()
	// {
	// 	await _dbContext.DisposeAsync();
	// 	GC.SuppressFinalize(this);
	// }
}
