using MassTransit;
using Messages.ExpenseServiceMessages.Category;

namespace EventSourcererService.MessageConsumers.ExpenseService.Category;

public class ExpenseServiceCategoryCreatedMessageConsumer : BaseConsumer<CategoryCreatedMessage>
{
	public ExpenseServiceCategoryCreatedMessageConsumer(AppDbContext dbContext, IDeduplicationService deduplicationService) : base(dbContext, deduplicationService)
	{
	}

	public override async Task Consume(ConsumeContext<CategoryCreatedMessage> context)
	{
		var message = context.Message;

		var hasProcessed = await _deduplicationService.HasProcessed(message.EventId);

		if (hasProcessed) return;

		await _deduplicationService.ProcessMessage(() => ProcessMessage(message));
	}


	/// <summary>
	/// Asynchronously processes the CategoryCreatedMessage.
	/// </summary>
	/// <param name="message">The CategoryCreatedMessage to process.</param>
	/// <returns>A task representing the asynchronous operation.</returns>
	async Task ProcessMessage(CategoryCreatedMessage message)
	{
		var eventData = new ExpenseServiceCategoryEventJsonData(message.Id, message.Name, message.Description, message.UserId, message.CreatedAt, message.CreatedBy);

		var categoryEvent = new ExpenseServiceCategoryEvent(message.EventId, "Create", DateTime.UtcNow, message.UserId, eventData);

		await _dbContext.ExpenseService_CategoryEvents.AddAsync(categoryEvent);
		await _dbContext.SaveChangesAsync();
	}
}
