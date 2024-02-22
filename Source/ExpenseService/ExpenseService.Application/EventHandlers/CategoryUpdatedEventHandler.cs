using Messages.ExpenseServiceMessages.Category;
using Microsoft.Extensions.Options;
using RabbitMqSettings;
using RabbitMqSettings.QueueRoutes.EventSourcerer;

namespace ExpenseService.Application.EventHandlers;

public class CategoryUpdatedEventHandler: INotificationHandler<CategoryUpdatedEvent>
{
	private readonly IBus _bus;
	private readonly RabbitMqOptions _rabbitMqOptions;
	private readonly IOutboxService _outboxService;


	public CategoryUpdatedEventHandler(IBus bus, IOptions<RabbitMqOptions> rabbitMqOptions, IOutboxService outboxService)
	{
		_bus             = bus;
		_rabbitMqOptions = rabbitMqOptions.Value;
		_outboxService   = outboxService;
	}


	public async Task Handle(CategoryUpdatedEvent notification, CancellationToken cancellationToken)
	{
		var message = new CategoryUpdatedMessage
		{
			EventId        = notification.EventDetails.EventId,
			Id             = notification.EventData.Id,
			NewName        = notification.EventData.NewName,
			NewDescription = notification.EventData.NewDescription,
			UserId         = notification.EventData.UserId,
			UpdatedAt      = notification.EventDetails.OccurredOn,
			UpdatedBy      = notification.EventDetails.OccurredBy
		};

		var categoryEventSourcererQueueName = _rabbitMqOptions.HostName + "/" + ExpenseServiceEventSourcererQueues.CategoryUpdatedEventSourcererQueue;

		await _outboxService.SaveMessageAsync(message, categoryEventSourcererQueueName, cancellationToken);
	}
}
