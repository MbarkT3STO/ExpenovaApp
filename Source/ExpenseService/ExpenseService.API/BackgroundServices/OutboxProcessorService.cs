using ExpenseService.Infrastructure.Data;
using ExpenseService.Infrastructure.Data.Entities;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ExpenseService.API.BackgroundServices;

public class OutboxProcessorService: BackgroundService
{
	readonly IServiceScopeFactory _serviceScopeFactory;
	readonly IMediator _mediator;
	readonly AppDbContext _dbContext;
	readonly IBus _bus;

	public OutboxProcessorService(IServiceScopeFactory serviceScopeFactory)
	{
		_serviceScopeFactory = serviceScopeFactory;

		var scope      = _serviceScopeFactory.CreateScope();
		    _mediator  = scope.ServiceProvider.GetRequiredService<IMediator>();
		    _dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
		    _bus       = scope.ServiceProvider.GetRequiredService<IBus>();
	}


	protected override async Task ExecuteAsync(CancellationToken stoppingToken)
	{
		while (!stoppingToken.IsCancellationRequested)
		{
			var outboxMessages = await _dbContext.OutboxMessages.ToListAsync(stoppingToken);

			foreach (var outboxEvent in outboxMessages)
			{
				await ProcessOutboxEvent(outboxEvent, stoppingToken);
			}
		}
	}

	private async Task ProcessOutboxEvent(OutboxMessage outboxMessage, CancellationToken cancellationToken)
	{
		var transaction = _dbContext.Database.BeginTransaction();

		try
		{
			var eventName     = outboxMessage.EventName;
			var eventMessage  = JsonConvert.DeserializeObject(outboxMessage.Data);
			var messageQueue  = new Uri(outboxMessage.QueueName);
			var queueEndPoint = await _bus.GetSendEndpoint(messageQueue);

			await queueEndPoint.Send(eventMessage, cancellationToken);

			_dbContext.OutboxMessages.Remove(outboxMessage);
			await _dbContext.SaveChangesAsync(cancellationToken);

			transaction.Commit();
		}
		catch (Exception)
		{
			transaction.Rollback();

			throw;
		}
	}



	// private INotification BuildEventFromOutboxEvent(OutboxMessage outboxMessage)
	// {
	// 	var eventName = outboxMessage.EventName;

	// 	var eventMessage = JsonConvert.DeserializeObject(outboxMessage.Data);

	// 	var messageQueue = new Uri(outboxMessage.QueueName);

	// 	var categoryEventSourcererQueueEndPoint = await _bus.GetSendEndpoint(messageQueue);

	// 	await categoryEventSourcererQueueEndPoint.Send(message, cancellationToken);


	// 	// switch (eventName)
	// 	// {
	// 	// 	case nameof(CategoryCreatedEvent): 
	// 	// 		{
	// 	// 			var categoryCreatedEvent = JsonConvert.DeserializeObject(outboxMessage.Data);

	// 	// 			return categoryCreatedEvent;
	// 	// 		}

	// 	// 	default: 
	// 	// 		throw new Exception("Event type not supported [From Outbox Event Processor]");
	// 	// }
	// }
}
