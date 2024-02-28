
using Messages.ExpenseServiceMessages.Category;
using Microsoft.Extensions.Options;
using RabbitMqSettings;
using RabbitMqSettings.QueueRoutes.EventSourcerer;

namespace ExpenseService.Application.EventHandlers;

public class CategoryDeletedEventHandler: INotificationHandler<CategoryDeletedEvent>
{
	private readonly RabbitMqOptions _rabbitMqOptions;
	private readonly IOutboxService _outboxService;

	public CategoryDeletedEventHandler(IOptions<RabbitMqOptions> rabbitMqOptions, IOutboxService outboxService)
	{
		_rabbitMqOptions = rabbitMqOptions.Value;
		_outboxService   = outboxService;
	}

	public async Task Handle(CategoryDeletedEvent notification, CancellationToken cancellationToken)
	{
		var eventDetails = notification.EventDetails;
		var eventData    = notification.EventData;

		var message = new CategoryDeletedMessage
		{
			EventId     = eventDetails.EventId,
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

		var categoryEventSourcererQueueName = _rabbitMqOptions.HostName + "/" + ExpenseServiceEventSourcererQueues.CategoryDeletedEventSourcererQueue;

		await _outboxService.SaveMessageIfNotExistsAsync(message, categoryEventSourcererQueueName, cancellationToken);
	}

}
