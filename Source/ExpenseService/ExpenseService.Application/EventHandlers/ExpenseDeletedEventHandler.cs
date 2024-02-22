using ExpenseService.Infrastructure.Data.Entities;
using Messages.ExpenseServiceMessages.Expense;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMqSettings;
using RabbitMqSettings.QueueRoutes.EventSourcerer;

namespace ExpenseService.Application.EventHandlers;

public class ExpenseDeletedEventHandler: INotificationHandler<ExpenseDeletedEvent>
{
	private readonly IBus _bus;
	private readonly RabbitMqOptions _rabbitMqOptions;
	private readonly IOutboxService _outboxService;

	public ExpenseDeletedEventHandler(IBus bus, IOptions<RabbitMqOptions> rabbitMqOptions, IOutboxService outboxService)
	{
		_bus             = bus;
		_rabbitMqOptions = rabbitMqOptions.Value;
		_outboxService   = outboxService;
	}


	public async Task Handle(ExpenseDeletedEvent notification, CancellationToken cancellationToken)
	{
		var message = new ExpenseDeletedMessage
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

		var expenseEventSourcererQueueName = _rabbitMqOptions.HostName + "/" + ExpenseServiceEventSourcererQueues.ExpenseDeletedEventSourcererQueue;

		await _outboxService.SaveMessageAsync(message, expenseEventSourcererQueueName, cancellationToken);
	}
}
