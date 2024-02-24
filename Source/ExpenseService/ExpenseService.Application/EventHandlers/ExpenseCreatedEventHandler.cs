
using ExpenseService.Infrastructure.Data.Entities;
using Messages.ExpenseServiceMessages.Expense;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMqSettings;
using RabbitMqSettings.QueueRoutes.EventSourcerer;

namespace ExpenseService.Application.EventHandlers;

public class ExpenseCreatedEventHandler: INotificationHandler<ExpenseCreatedEvent>
{
	private readonly RabbitMqOptions _rabbitMqOptions;
	private readonly IOutboxService _outboxService;

	public ExpenseCreatedEventHandler(IOptions<RabbitMqOptions> rabbitMqOptions, IOutboxService outboxService)
	{
		_rabbitMqOptions = rabbitMqOptions.Value;
		_outboxService   = outboxService;
	}


	public async Task Handle(ExpenseCreatedEvent notification, CancellationToken cancellationToken)
	{
		var message = new ExpenseCreatedMessage
		{
			EventId     = notification.EventDetails.EventId,
			Id          = notification.EventData.Id,
			Amount      = notification.EventData.Amount,
			Description = notification.EventData.Description,
			Date        = notification.EventData.Date,
			CategoryId  = notification.EventData.CategoryId,
			UserId      = notification.EventData.UserId,
			CreatedAt   = notification.EventDetails.OccurredOn,
			CreatedBy   = notification.EventDetails.OccurredBy
		};

		var expenseEventSourcererQueueName = _rabbitMqOptions.HostName + "/" + ExpenseServiceEventSourcererQueues.ExpenseCreatedEventSourcererQueue;

		await _outboxService.SaveMessageIfNotExistsAsync(message, expenseEventSourcererQueueName, cancellationToken);
	}
}
