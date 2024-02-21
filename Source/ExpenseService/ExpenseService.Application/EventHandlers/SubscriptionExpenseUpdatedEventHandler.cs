using ExpenseService.Application.Interfaces;
using ExpenseService.Infrastructure.Data.Entities;
using Messages.ExpenseServiceMessages.SubscriptionExpense;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMqSettings;
using RabbitMqSettings.QueueRoutes.EventSourcerer;

namespace ExpenseService.Application.EventHandlers;

public class SubscriptionExpenseUpdatedEventHandler: INotificationHandler<SubscriptionExpenseUpdatedEvent>
{
	private readonly IBus _bus;
	private readonly RabbitMqOptions _rabbitMqOptions;
	private readonly AppDbContext _dbContext;
	private readonly IDomainEventDeduplicationService _deduplicationService;

	public SubscriptionExpenseUpdatedEventHandler(IBus bus, IOptions<RabbitMqOptions> rabbitMqOptions, AppDbContext dbContext, DomainEventDatabaseDeduplicationService deduplicationService)
	{
		_bus                  = bus;
		_rabbitMqOptions      = rabbitMqOptions.Value;
		_dbContext            = dbContext;
		_deduplicationService = deduplicationService;
	}

	public async Task Handle(SubscriptionExpenseUpdatedEvent notification, CancellationToken cancellationToken)
	{
		var hasProcessed = await _deduplicationService.HasProcessed(notification.EventDetails.EventId);

		if (hasProcessed) return;


		var message = new SubscriptionExpenseUpdatedMessage
		{
			EventId            = notification.EventDetails.EventId,
			Id                 = notification.EventData.Id,
			Amount             = notification.EventData.Amount,
			Description        = notification.EventData.Description,
			StartDate          = notification.EventData.StartDate,
			EndDate            = notification.EventData.EndDate,
			RecurrenceInterval = (byte) notification.EventData.RecurrenceInterval,
			BillingAmount      = notification.EventData.BillingAmount,
			CategoryId         = notification.EventData.CategoryId,
			UserId             = notification.EventData.UserId,

			CreatedBy     = notification.EventData.CreatedBy,
			CreatedAt     = notification.EventData.CreatedAt,
			LastUpdatedAt = notification.EventData.LastUpdatedAt,
			LastUpdatedBy = notification.EventData.LastUpdatedBy,
			IsDeleted     = notification.EventData.IsDeleted,
			DeletedAt     = notification.EventData.DeletedAt,
			DeletedBy     = notification.EventData.DeletedBy
		};

		var expenseEventSourcererQueueName = _rabbitMqOptions.HostName + "/" + ExpenseServiceEventSourcererQueues.SubscriptionExpenseUpdatedEventSourcererQueue;

		await _deduplicationService.ProcessEventAsync(() => SaveOutboxMessageAsync(message, expenseEventSourcererQueueName, cancellationToken));
	}


	/// <summary>
	/// Saves the outbox message asynchronously.
	/// </summary>
	/// <param name="message">The subscription expense updated message.</param>
	/// <param name="queueName">The name of the queue.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>A task representing the asynchronous operation.</returns>
	private async Task SaveOutboxMessageAsync(SubscriptionExpenseUpdatedMessage message, string queueName, CancellationToken cancellationToken)
	{
		var jsonSerializerSettings = new JsonSerializerSettings
		{
			TypeNameHandling = TypeNameHandling.All
		};
		var serializedMessage = JsonConvert.SerializeObject(message, jsonSerializerSettings);
		var outboxEvent       = new OutboxMessage(message.EventId, nameof(SubscriptionExpenseUpdatedMessage), serializedMessage, queueName);

		_dbContext.OutboxMessages.Add(outboxEvent);
		await _dbContext.SaveChangesAsync(cancellationToken);
	}
}
