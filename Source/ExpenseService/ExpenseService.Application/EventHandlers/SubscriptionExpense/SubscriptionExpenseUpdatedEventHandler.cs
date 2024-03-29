using ExpenseService.Application.Interfaces;
using ExpenseService.Infrastructure.Data.Entities;
using Messages.ExpenseServiceMessages.SubscriptionExpense;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMqSettings;
using RabbitMqSettings.QueueRoutes.EventSourcerer;

namespace ExpenseService.Application.EventHandlers;

public class SubscriptionExpenseUpdatedEventHandler: INotificationHandler<SubscriptionExpenseUpdatedEvent>
{
	private readonly RabbitMqOptions _rabbitMqOptions;
	private readonly IOutboxService _outboxService;

	public SubscriptionExpenseUpdatedEventHandler(IOptions<RabbitMqOptions> rabbitMqOptions, IOutboxService outboxService)
	{
		_rabbitMqOptions = rabbitMqOptions.Value;
		_outboxService   = outboxService;
	}

	public async Task Handle(SubscriptionExpenseUpdatedEvent notification, CancellationToken cancellationToken)
	{
		var hasProcessed = await _outboxService.HasProcessed(notification.EventDetails.EventId, cancellationToken);

		if (hasProcessed) return;


		var message = new SubscriptionExpenseUpdatedMessage
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

		var expenseEventSourcererQueueName = _rabbitMqOptions.HostName + "/" + ExpenseServiceEventSourcererQueues.SubscriptionExpenseUpdatedEventSourcererQueue;

		await _outboxService.SaveMessageIfNotExistsAsync(message, expenseEventSourcererQueueName, cancellationToken);
	}
}
