using ExpenseService.Application.Interfaces;
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
	readonly IOutboxService _outboxService;

	public OutboxProcessorService(IServiceScopeFactory serviceScopeFactory)
	{
		_serviceScopeFactory = serviceScopeFactory;

		var scope          = _serviceScopeFactory.CreateScope();

			_mediator      = scope.ServiceProvider.GetRequiredService<IMediator>();
			_dbContext     = scope.ServiceProvider.GetRequiredService<AppDbContext>();
			_bus           = scope.ServiceProvider.GetRequiredService<IBus>();
			_outboxService = scope.ServiceProvider.GetRequiredService<IOutboxService>();
	}


	protected override async Task ExecuteAsync(CancellationToken stoppingToken)
	{
		while (!stoppingToken.IsCancellationRequested)
		{
			var outboxMessages = await _outboxService.GetUnprocessedOutboxMessagesAsync(stoppingToken);

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
			var deserializationSettings = new JsonSerializerSettings()
			{
				TypeNameHandling = TypeNameHandling.All
			};

			var eventName     = outboxMessage.EventName;
			var eventMessage  = JsonConvert.DeserializeObject(outboxMessage.Data, deserializationSettings);
			var messageQueue  = new Uri(outboxMessage.QueueName);
			var queueEndPoint = await _bus.GetSendEndpoint(messageQueue);

			await queueEndPoint.Send(eventMessage, cancellationToken);

			await _outboxService.MarkAsProcessedAsync(outboxMessage.Id, cancellationToken);

			transaction.Commit();
		}
		catch (Exception)
		{
			transaction.Rollback();

			throw;
		}
	}
}
