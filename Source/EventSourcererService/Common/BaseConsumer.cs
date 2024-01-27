using MassTransit;
using RabbitMqSettings.Interfaces;

namespace EventSourcererService.Common;

/// <summary>
/// Base class for Message Consumers.
/// </summary>
public abstract class BaseConsumer
{
	protected readonly AppDbContext _dbContext;

	protected BaseConsumer(AppDbContext dbContext)
	{
		_dbContext = dbContext;
	}
}


/// <summary>
/// Represents a base consumer class for handling messages of type TMessage.
/// </summary>
/// <typeparam name="TMessage">The type of the message.</typeparam>
public abstract class BaseConsumer<TMessage> : BaseConsumer, IConsumer<TMessage> where TMessage : class
{
	protected readonly IDeduplicationService _deduplicationService;

	protected BaseConsumer(AppDbContext dbContext, IDeduplicationService deduplicationService) : base(dbContext)
	{
		_deduplicationService = deduplicationService;
	}

	/// <summary>
	/// Consumes a message of type TMessage.
	/// </summary>
	/// <param name="context">The context of the consumed message.</param>
	/// <returns>A task representing the asynchronous operation.</returns>
	public abstract Task Consume(ConsumeContext<TMessage> context);
}