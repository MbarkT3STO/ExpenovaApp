using ExpenseService.Infrastructure.Data.Entities;
using Messages.ExpenseServiceMessages.Expense;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMqSettings;
using RabbitMqSettings.QueueRoutes.EventSourcerer;

namespace ExpenseService.Application.EventHandlers;

public class ExpenseUpdatedEventHandler: INotificationHandler<ExpenseUpdatedEvent>
{
	private readonly RabbitMqOptions _rabbitMqOptions;
	private readonly IOutboxService _outboxService;

	public ExpenseUpdatedEventHandler(IOptions<RabbitMqOptions> rabbitMqOptions, IOutboxService outboxService)
	{
		_rabbitMqOptions = rabbitMqOptions.Value;
		_outboxService   = outboxService;
	}

	public async Task Handle(ExpenseUpdatedEvent notification, CancellationToken cancellationToken)
	{
		var message = new ExpenseUpdatedMessage
		{
			EventId       = notification.EventDetails.EventId,
			EventName     = notification.EventDetails.EventName,
			Id            = notification.EventData.Id,
			Amount        = notification.EventData.Amount,
			Description   = notification.EventData.Description,
			Date          = notification.EventData.Date,
			CategoryId    = notification.EventData.CategoryId,
			UserId        = notification.EventData.UserId,
			LastUpdatedAt = notification.EventDetails.OccurredOn,
			LastUpdatedBy = notification.EventDetails.OccurredBy
		};

		var expenseEventSourcererQueueName = _rabbitMqOptions.HostName + "/" + ExpenseServiceEventSourcererQueues.ExpenseUpdatedEventSourcererQueue;

		await _outboxService.SaveMessageIfNotExistsAsync(message, expenseEventSourcererQueueName, cancellationToken);
	}

}
