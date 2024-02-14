using ExpenseService.Infrastructure.Data.Entities;
using Messages.ExpenseServiceMessages.SubscriptionExpense;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMqSettings;
using RabbitMqSettings.QueueRoutes.EventSourcerer;

namespace ExpenseService.Application.EventHandlers;

public class SubscriptionExpenseCreatedEventHandler: INotificationHandler<SubscriptionExpenseCreatedEvent>
{
	private readonly IBus _bus;
	private readonly RabbitMqOptions _rabbitMqOptions;
	private readonly AppDbContext _dbContext;

	public SubscriptionExpenseCreatedEventHandler(IBus bus, IOptions<RabbitMqOptions> rabbitMqOptions, AppDbContext dbContext)
	{
		_bus             = bus;
		_rabbitMqOptions = rabbitMqOptions.Value;
		_dbContext       = dbContext;
	}


	public async Task Handle(SubscriptionExpenseCreatedEvent notification, CancellationToken cancellationToken)
	{
		var message = new SubscriptionExpenseCreatedMessage
		{
			EventId            = notification.EventDetails.EventId,
			Id                 = notification.EventData.Id,
			Description        = notification.EventData.Description,
			Amount             = notification.EventData.Amount,
			UserId             = notification.EventData.UserId,
			CategoryId         = notification.EventData.CategoryId,
			StartDate          = notification.EventData.StartDate,
			EndDate            = notification.EventData.EndDate,
			RecurrenceInterval = (byte)notification.EventData.RecurrenceInterval,
			BillingAmount      = notification.EventData.BillingAmount,
			CreatedAt          = notification.EventData.CreatedAt,
			CreatedBy          = notification.EventData.CreatedBy,
			LastUpdatedAt      = notification.EventData.LastUpdatedAt,
			LastUpdatedBy      = notification.EventData.LastUpdatedBy,
			IsDeleted          = notification.EventData.IsDeleted,
			DeletedAt          = notification.EventData.DeletedAt,
			DeletedBy          = notification.EventData.DeletedBy
		};

		var expenseEventSourcererQueueName = _rabbitMqOptions.HostName + "/" + ExpenseServiceEventSourcererQueues.SubscriptionExpenseCreatedEventSourcererQueue;

		var jsonSerializerSettings = new JsonSerializerSettings
		{
			TypeNameHandling = TypeNameHandling.All
		};
		var serializedMessage = JsonConvert.SerializeObject(message, jsonSerializerSettings);
		var outboxEvent       = new OutboxMessage(nameof(SubscriptionExpenseCreatedEvent), serializedMessage, expenseEventSourcererQueueName);

		_dbContext.OutboxMessages.Add(outboxEvent);
		await _dbContext.SaveChangesAsync(cancellationToken);
	}
}
