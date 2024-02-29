using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Messages.ExpenseServiceMessages.Income;
using Microsoft.Extensions.Options;
using RabbitMqSettings;
using RabbitMqSettings.QueueRoutes.EventSourcerer;

namespace ExpenseService.Application.EventHandlers.Income;

public class IncomeCreatedEventHandler: INotificationHandler<IncomeCreatedEvent>
{
	readonly RabbitMqOptions _rabbitMqOptions;
	readonly IOutboxService _outboxService;

	public IncomeCreatedEventHandler(IOptions<RabbitMqOptions> rabbitMqOptions, IOutboxService outboxService)
	{
		_rabbitMqOptions = rabbitMqOptions.Value;
		_outboxService   = outboxService;
	}

	public async Task Handle(IncomeCreatedEvent notification, CancellationToken cancellationToken)
	{
		var message = new IncomeCreatedMessage
		{
			EventId     = notification.EventDetails.EventId,
			EventName   = notification.EventDetails.EventName,
			Id          = notification.EventData.Id,
			Amount      = notification.EventData.Amount,
			Description = notification.EventData.Description,
			Date        = notification.EventData.Date,
			CategoryId  = notification.EventData.CategoryId,
			UserId      = notification.EventData.UserId,
			CreatedAt   = notification.EventDetails.OccurredOn,
			CreatedBy   = notification.EventDetails.OccurredBy
		};

		var incomeEventSourcererQueueName = _rabbitMqOptions.HostName + "/" + ExpenseServiceEventSourcererQueues.IncomeCreatedEventSourcererQueue;

		await _outboxService.SaveMessageIfNotExistsAsync(message, incomeEventSourcererQueueName, cancellationToken);
	}
}
