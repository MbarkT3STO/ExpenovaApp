using ExpenseService.Infrastructure.Data.Entities;
using Messages.ExpenseServiceMessages.Category;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMqSettings;
using RabbitMqSettings.QueueRoutes;
using RabbitMqSettings.QueueRoutes.EventSourcerer;

namespace ExpenseService.Application.EventHandlers;

public class CategoryCreatedEventHandler: INotificationHandler<CategoryCreatedEvent>
{
	private readonly IBus _bus;
	private readonly RabbitMqOptions _rabbitMqOptions;
	private readonly AppDbContext _dbContext;

	public CategoryCreatedEventHandler(IBus bus, IOptions<RabbitMqOptions> rabbitMqOptions, AppDbContext dbContext)
	{
		_bus             = bus;
		_rabbitMqOptions = rabbitMqOptions.Value;
		_dbContext       = dbContext;
	}


	public async Task Handle(CategoryCreatedEvent notification, CancellationToken cancellationToken)
	{
		Log.Information("CategoryCreatedEventHandler.Handle - Start");

		var message = new CategoryCreatedMessage
		{
			EventId     = notification.EventDetails.EventId,
			Id          = notification.EventData.Id,
			Name        = notification.EventData.Name,
			Description = notification.EventData.Description,
			UserId      = notification.EventData.UserId,
			CreatedAt   = notification.EventDetails.OccurredOn,
			CreatedBy   = notification.EventDetails.OccurredBy
		};

		var categoryEventSourcererQueueName = _rabbitMqOptions.HostName + "/" + ExpenseServiceEventSourcererQueues.CategoryCreatedEventSourcererQueue;

		var jsonSerializerSettings = new JsonSerializerSettings
		{
			TypeNameHandling = TypeNameHandling.All
		};
		var serializedMessage = JsonConvert.SerializeObject(message, jsonSerializerSettings);
		var outboxEvent       = new OutboxMessage(nameof(CategoryCreatedEvent), serializedMessage, categoryEventSourcererQueueName );

		_dbContext.OutboxMessages.Add(outboxEvent);
		await _dbContext.SaveChangesAsync(cancellationToken);

		Log.Information("CategoryCreatedEventHandler.Handle - End");
	}
}

