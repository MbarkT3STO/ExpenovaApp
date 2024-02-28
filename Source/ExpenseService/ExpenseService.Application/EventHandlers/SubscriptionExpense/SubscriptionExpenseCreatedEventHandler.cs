using ExpenseService.Infrastructure.Data.Entities;
using Messages.ExpenseServiceMessages.SubscriptionExpense;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMqSettings;
using RabbitMqSettings.QueueRoutes.EventSourcerer;

namespace ExpenseService.Application.EventHandlers;

public class SubscriptionExpenseCreatedEventHandler: INotificationHandler<SubscriptionExpenseCreatedEvent>
{
	private readonly RabbitMqOptions _rabbitMqOptions;
	private readonly IOutboxService _outboxService;

	public SubscriptionExpenseCreatedEventHandler(IOptions<RabbitMqOptions> rabbitMqOptions, IOutboxService outboxService)
	{
		_rabbitMqOptions = rabbitMqOptions.Value;
		_outboxService   = outboxService;
	}

	public async Task Handle(SubscriptionExpenseCreatedEvent notification, CancellationToken cancellationToken)
	{
		var message = new SubscriptionExpenseCreatedMessage
		{
			EventId            = notification.EventDetails.EventId,
			Id                 = notification.EventData.Id,
			Description        = notification.EventData.Description,
			Amount             = notification.EventData.Amount,
			UserId             = notification.EventData.UserId,
			CategoryId         = notification.EventData.CategoryId,
			StartDate          = notification.EventData.StartDate,
			EndDate            = notification.EventData.EndDate,
			RecurrenceInterval = (byte)notification.EventData.RecurrenceInterval,
			BillingAmount      = notification.EventData.BillingAmount,
			CreatedAt          = notification.EventData.CreatedAt,
			CreatedBy          = notification.EventData.CreatedBy,
			LastUpdatedAt      = notification.EventData.LastUpdatedAt,
			LastUpdatedBy      = notification.EventData.LastUpdatedBy,
			IsDeleted          = notification.EventData.IsDeleted,
			DeletedAt          = notification.EventData.DeletedAt,
			DeletedBy          = notification.EventData.DeletedBy
		};

		var expenseEventSourcererQueueName = _rabbitMqOptions.HostName + "/" + ExpenseServiceEventSourcererQueues.SubscriptionExpenseCreatedEventSourcererQueue;

		await _outboxService.SaveMessageIfNotExistsAsync(message, expenseEventSourcererQueueName, cancellationToken);
	}
}
