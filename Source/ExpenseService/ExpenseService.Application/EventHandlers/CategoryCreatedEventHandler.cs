using Messages.ExpenseServiceMessages.Category;
using RabbitMqSettings;
using RabbitMqSettings.QueueRoutes;
using RabbitMqSettings.QueueRoutes.EventSourcerer;

namespace ExpenseService.Application.EventHandlers;

public class CategoryCreatedEventHandler: INotificationHandler<CategoryCreatedEvent>
{
	private readonly IBus _bus;
	private readonly RabbitMqOptions _rabbitMqOptions;
	private readonly ExpenseServiceRabbitMqEndpointOptions.Category _categoryRabbitMqEndpointOptions;

	public async Task Handle(CategoryCreatedEvent notification, CancellationToken cancellationToken)
	{
		var message = new CategoryCreatedMessage
		{
			Id          = notification.EventData.Id,
			Name        = notification.EventData.Name,
			Description = notification.EventData.Description,
			UserId      = notification.EventData.UserId,
			CreatedAt   = notification.EventDetails.OccurredOn,
			CreatedBy   = notification.EventDetails.OccurredBy
		};

		var categoryEventSourcererQueueName     = _rabbitMqOptions.HostName + "/" + ExpenseServiceEventSourcererQueues.CategoryEventSourcererQueue;
		var categoryEventSourcererQueue         = new Uri(categoryEventSourcererQueueName);
		var categoryEventSourcererQueueEndPoint = await _bus.GetSendEndpoint(categoryEventSourcererQueue);
		
		categoryEventSourcererQueueEndPoint.Send(message, cancellationToken);
	}
}

