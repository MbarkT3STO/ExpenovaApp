
using ExpenseService.Infrastructure.Data.Entities;
using Messages.ExpenseServiceMessages.Expense;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMqSettings;
using RabbitMqSettings.QueueRoutes.EventSourcerer;

namespace ExpenseService.Application.EventHandlers;

public class ExpenseCreatedEventHandler : INotificationHandler<ExpenseCreatedEvent>
{
	private readonly IBus _bus;
	private readonly RabbitMqOptions _rabbitMqOptions;
	private readonly AppDbContext _dbContext;

	public ExpenseCreatedEventHandler(IBus bus, IOptions<RabbitMqOptions> rabbitMqOptions, AppDbContext dbContext)
	{
		_bus             = bus;
		_rabbitMqOptions = rabbitMqOptions.Value;
		_dbContext       = dbContext;
	}


	public async Task Handle(ExpenseCreatedEvent notification, CancellationToken cancellationToken)
	{
		var message = new ExpenseCreatedMessage
		{
			EventId     = notification.EventDetails.EventId,
			Id          = notification.EventData.Id,
			Amount      = notification.EventData.Amount,
			Description = notification.EventData.Description,
			Date        = notification.EventData.Date,
			CategoryId  = notification.EventData.CategoryId,
			UserId      = notification.EventData.UserId,
			CreatedAt   = notification.EventDetails.OccurredOn,
			CreatedBy   = notification.EventDetails.OccurredBy
		};

		var expenseEventSourcererQueueName = _rabbitMqOptions.HostName + "/" + ExpenseServiceEventSourcererQueues.ExpenseCreatedEventSourcererQueue;

		var jsonSerializerSettings = new JsonSerializerSettings
		{
			TypeNameHandling = TypeNameHandling.All
		};
		var serializedMessage = JsonConvert.SerializeObject(message, jsonSerializerSettings);
		var outboxEvent       = new OutboxMessage(nameof(ExpenseCreatedEvent), serializedMessage, expenseEventSourcererQueueName );

		_dbContext.OutboxMessages.Add(outboxEvent);
		await _dbContext.SaveChangesAsync(cancellationToken);
	}
}
