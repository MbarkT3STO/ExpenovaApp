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
		_dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
		var outboxMessages = await _dbContext.OutboxMessages.Where(x => !x.IsProcessed).ToListAsync(cancellationToken);

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
		_dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
		var processedMessages = _dbContext.OutboxMessages.Where(x => x.IsProcessed).AsNoTracking();

		// Attach the already tracked entities to the context to avoid the exception.
		_dbContext.AttachRange(processedMessages);

		_dbContext.OutboxMessages.RemoveRange(processedMessages);
		await _dbContext.SaveChangesAsync(cancellationToken);
	}

	public async Task SaveMessageAsync<T>(T message, string queueName, CancellationToken cancellationToken) where T: BaseEventMessage
	{
		var serializedMessage = SerializeMessage(message);
		var outboxEvent       = new OutboxMessage(message.EventId, typeof(T).Name, serializedMessage, queueName);

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
			var outboxEvent = new OutboxMessage(message.EventId, typeof(T).Name, serializedMessage, queueName);

			_dbContext.OutboxMessages.Add(outboxEvent);
			await _dbContext.SaveChangesAsync(cancellationToken);
		}
	}
}
