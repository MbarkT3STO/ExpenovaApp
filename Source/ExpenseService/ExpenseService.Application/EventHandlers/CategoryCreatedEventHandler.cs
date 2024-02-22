using Microsoft.Extensions.Options;

using ExpenseService.Infrastructure.Data.Entities;

using Messages.ExpenseServiceMessages.Category;

using Newtonsoft.Json;

using RabbitMqSettings;
using RabbitMqSettings.QueueRoutes.EventSourcerer;

namespace ExpenseService.Application.EventHandlers;

public class CategoryCreatedEventHandler: INotificationHandler<CategoryCreatedEvent>
{
	private readonly IBus _bus;
	private readonly RabbitMqOptions _rabbitMqOptions;
	private readonly IOutboxService _outboxService;

	public CategoryCreatedEventHandler(IBus bus, IOptions<RabbitMqOptions> rabbitMqOptions, IOutboxService outboxService)
	{
		_bus             = bus;
		_rabbitMqOptions = rabbitMqOptions.Value;
		_outboxService   = outboxService;
	}


	public async Task Handle(CategoryCreatedEvent notification, CancellationToken cancellationToken)
	{
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

		await _outboxService.SaveMessageAsync(message, categoryEventSourcererQueueName, cancellationToken);
	}
}

