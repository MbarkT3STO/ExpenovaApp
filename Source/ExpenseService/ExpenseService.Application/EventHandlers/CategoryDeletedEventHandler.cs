
using Messages.ExpenseServiceMessages.Category;
using Microsoft.Extensions.Options;
using RabbitMqSettings;
using RabbitMqSettings.QueueRoutes.EventSourcerer;

namespace ExpenseService.Application.EventHandlers;

public class CategoryDeletedEventHandler: INotificationHandler<CategoryDeletedEvent>
{
	private readonly IBus _bus;
	private readonly RabbitMqOptions _rabbitMqOptions;

	public CategoryDeletedEventHandler(IBus bus, IOptions<RabbitMqOptions> rabbitMqOptions)
	{
		_bus             = bus;
		_rabbitMqOptions = rabbitMqOptions.Value;
	}


	public async Task Handle(CategoryDeletedEvent notification, CancellationToken cancellationToken)
	{
		var eventDetails = notification.EventDetails;
		var eventData    = notification.EventData;
		
		var message = new CategoryDeletedMessage
		{
			Id          = eventData.Id,
			Name        = eventData.Name,
			Description = eventData.Description,
			UserId      = eventData.UserId,
			CreatedAt   = eventData.CreatedAt,
			CreatedBy   = eventData.CreatedBy,
			UpdatedAt   = eventData.UpdatedAt,
			UpdatedBy   = eventData.UpdatedBy,
			DeletedAt   = eventDetails.OccurredOn,
			DeletedBy   = eventDetails.OccurredBy
		};
		
		var categoryEventSourcererQueueName     = _rabbitMqOptions.HostName + "/" + ExpenseServiceEventSourcererQueues.CategoryDeletedEventSourcererQueue;
		var categoryEventSourcererQueue         = new Uri(categoryEventSourcererQueueName);
		var categoryEventSourcererQueueEndPoint = await _bus.GetSendEndpoint(categoryEventSourcererQueue);
		
		await categoryEventSourcererQueueEndPoint.Send(message, cancellationToken);
	}

}
