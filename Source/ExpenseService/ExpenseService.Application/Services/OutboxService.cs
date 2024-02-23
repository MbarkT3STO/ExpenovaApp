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
public class OutboxService : IOutboxService
{
	readonly AppDbContext _dbContext;

	public OutboxService(AppDbContext dbContext)
	{
		_dbContext = dbContext;
	}

	public async Task<IEnumerable<OutboxMessage>> GetUnprocessedMessagesAsync(CancellationToken cancellationToken = default)
	{
		var outboxMessages = await _dbContext.OutboxMessages.Where(x => !x.IsProcessed).ToListAsync(cancellationToken);

		return outboxMessages;
	}

	public async Task<bool> HasProcessed(Guid eventId, CancellationToken cancellationToken = default)
	{
		var hasProcessed = await _dbContext.OutboxMessages.AnyAsync(x => x.EventId == eventId && x.IsProcessed, cancellationToken);

		return hasProcessed;
	}

	public async Task<bool> HasProcessed(int messageId, CancellationToken cancellationToken = default)
	{
		var hasProcessed = await _dbContext.OutboxMessages.AnyAsync(x => x.Id == messageId && x.IsProcessed, cancellationToken);

		return hasProcessed;
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

	public async Task SaveMessageAsync<T>(T message, string queueName, CancellationToken cancellationToken) where T : BaseEventMessage
	{
		var jsonSerializerSettings = new JsonSerializerSettings
		{
			TypeNameHandling = TypeNameHandling.All
		};
		var serializedMessage = JsonConvert.SerializeObject(message, jsonSerializerSettings);
		var outboxEvent = new OutboxMessage(message.EventId, nameof(T), serializedMessage, queueName);

		_dbContext.OutboxMessages.Add(outboxEvent);
		await _dbContext.SaveChangesAsync(cancellationToken);
	}
}
