using Messages.ExpenseServiceMessages.SubscriptionExpense;
using Microsoft.Extensions.Options;
using RabbitMqSettings;
using RabbitMqSettings.QueueRoutes.EventSourcerer;

namespace ExpenseService.Application.EventHandlers;

public class SubscriptionExpenseDeletedEventHandler: INotificationHandler<SubscriptionExpenseDeletedEvent>
{
	private readonly RabbitMqOptions _rabbitMqOptions;
	private readonly IOutboxService _outboxService;

	public SubscriptionExpenseDeletedEventHandler(IOptions<RabbitMqOptions> rabbitMqOptions, IOutboxService outboxService)
	{
		_rabbitMqOptions = rabbitMqOptions.Value;
		_outboxService   = outboxService;
	}

	public async Task Handle(SubscriptionExpenseDeletedEvent notification, CancellationToken cancellationToken)
	{
		var message = new SubscriptionExpenseDeletedMessage
		{
			EventId            = notification.EventDetails.EventId,
			EventName          = notification.EventDetails.EventName,
			Id                 = notification.EventData.Id,
			Amount             = notification.EventData.Amount,
			Description        = notification.EventData.Description,
			StartDate          = notification.EventData.StartDate,
			EndDate            = notification.EventData.EndDate,
			RecurrenceInterval = (byte) notification.EventData.RecurrenceInterval,
			BillingAmount      = notification.EventData.BillingAmount,
			CategoryId         = notification.EventData.CategoryId,
			UserId             = notification.EventData.UserId,

			CreatedBy          = notification.EventData.CreatedBy,
			CreatedAt          = notification.EventData.CreatedAt,
			LastUpdatedAt      = notification.EventData.LastUpdatedAt,
			LastUpdatedBy      = notification.EventData.LastUpdatedBy,
			IsDeleted          = notification.EventData.IsDeleted,
			DeletedAt          = notification.EventData.DeletedAt,
			DeletedBy          = notification.EventData.DeletedBy
		};

		var expenseEventSourcererQueueName = _rabbitMqOptions.HostName + "/" + ExpenseServiceEventSourcererQueues.SubscriptionExpenseDeletedEventSourcererQueue;

		await _outboxService.SaveMessageIfNotExistsAsync(message, expenseEventSourcererQueueName, cancellationToken);
	}
}
