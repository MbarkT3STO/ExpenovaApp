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

	public SubscriptionExpenseUpdatedEventHandler(IBus bus, IOptions<RabbitMqOptions> rabbitMqOptions, AppDbContext dbContext)
	{
		_bus             = bus;
		_rabbitMqOptions = rabbitMqOptions.Value;
		_dbContext       = dbContext;
	}


	public async Task Handle(SubscriptionExpenseUpdatedEvent notification, CancellationToken cancellationToken)
	{
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

			CreatedBy		  = notification.EventData.CreatedBy,
			CreatedAt		  = notification.EventData.CreatedAt,
			LastUpdatedAt	  = notification.EventData.LastUpdatedAt,
			LastUpdatedBy	  = notification.EventData.LastUpdatedBy,
			IsDeleted		  = notification.EventData.IsDeleted,
			DeletedAt		  = notification.EventData.DeletedAt,
			DeletedBy		  = notification.EventData.DeletedBy
		};

		var expenseEventSourcererQueueName = _rabbitMqOptions.HostName + "/" + ExpenseServiceEventSourcererQueues.SubscriptionExpenseUpdatedEventSourcererQueue;

		var jsonSerializerSettings = new JsonSerializerSettings
		{
			TypeNameHandling = TypeNameHandling.All
		};
		var serializedMessage = JsonConvert.SerializeObject(message, jsonSerializerSettings);
		var outboxEvent       = new OutboxMessage(nameof(SubscriptionExpenseUpdatedEvent), serializedMessage, expenseEventSourcererQueueName );

		_dbContext.OutboxMessages.Add(outboxEvent);
		await _dbContext.SaveChangesAsync(cancellationToken);
	}
}
