using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Messages.ExpenseServiceMessages.Income;
using Microsoft.Extensions.Options;
using RabbitMqSettings;
using RabbitMqSettings.QueueRoutes.EventSourcerer;

namespace ExpenseService.Application.EventHandlers.Income;

public class IncomeUpdatedEventHandler: INotificationHandler<IncomeUpdatedEvent>
{
	readonly RabbitMqOptions _rabbitMqOptions;
	readonly IOutboxService _outboxService;


	public IncomeUpdatedEventHandler(IOptions<RabbitMqOptions> rabbitMqOptions, IOutboxService outboxService)
	{
		_rabbitMqOptions = rabbitMqOptions.Value;
		_outboxService   = outboxService;
	}


	public async Task Handle(IncomeUpdatedEvent notification, CancellationToken cancellationToken)
	{
		var message = new IncomeUpdatedMessage
		{
			EventId       = notification.EventDetails.EventId,
			Id            = notification.EventData.Id,
			Amount        = notification.EventData.Amount,
			Description   = notification.EventData.Description,
			Date          = notification.EventData.Date,
			CategoryId    = notification.EventData.CategoryId,
			UserId        = notification.EventData.UserId,
			LastUpdatedAt = notification.EventDetails.OccurredOn,
			LastUpdatedBy = notification.EventDetails.OccurredBy
		};

		var incomeEventSourcererQueueName = _rabbitMqOptions.HostName + "/" + ExpenseServiceEventSourcererQueues.IncomeUpdatedEventSourcererQueue;

		await _outboxService.SaveMessageIfNotExistsAsync(message, incomeEventSourcererQueueName, cancellationToken);
	}
}
