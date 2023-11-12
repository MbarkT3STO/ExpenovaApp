using Messages.ExpenseServiceMessages.Category;
using RabbitMqSettings;
using RabbitMqSettings.QueueRoutes.EventSourcerer;

namespace ExpenseService.Application.EventHandlers;

public class CategoryUpdatedEventHandler: INotificationHandler<CategoryUpdatedEvent>
{
	private readonly IBus _bus;
	private readonly RabbitMqOptions _rabbitMqOptions;
	
	
	public async Task Handle(CategoryUpdatedEvent notification, CancellationToken cancellationToken)
	{
		var message = new CategoryUpdatedMessage
		{
			Id             = notification.EventData.Id,
			NewName        = notification.EventData.NewName,
			NewDescription = notification.EventData.NewDescription,
			UserId         = notification.EventData.UserId,
			UpdatedAt      = notification.EventDetails.OccurredOn,
			UpdatedBy      = notification.EventDetails.OccurredBy
		};

		var categoryEventSourcererQueueName     = _rabbitMqOptions.HostName + "/" + ExpenseServiceEventSourcererQueues.CategoryEventSourcererQueue;
		var categoryEventSourcererQueue         = new Uri(categoryEventSourcererQueueName);
		var categoryEventSourcererQueueEndPoint = await _bus.GetSendEndpoint(categoryEventSourcererQueue);
		
		categoryEventSourcererQueueEndPoint.Send(message, cancellationToken);
	}
}
