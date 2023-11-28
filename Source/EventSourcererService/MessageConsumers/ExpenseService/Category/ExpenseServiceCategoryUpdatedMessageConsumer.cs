using MassTransit;
using Messages.ExpenseServiceMessages.Category;

namespace EventSourcererService.MessageConsumers.ExpenseService.Category;

public class ExpenseServiceCategoryUpdatedMessageConsumer : BaseConsumer<CategoryUpdatedMessage>
{
	public ExpenseServiceCategoryUpdatedMessageConsumer(AppDbContext dbContext, IDeduplicationService deduplicationService) : base(dbContext, deduplicationService)
	{
	}

	public override async Task Consume(ConsumeContext<CategoryUpdatedMessage> context)
	{
		var message = context.Message;
		
		var hasProcessed = await _deduplicationService.HasProcessed(message.EventId);
		
		if (hasProcessed) return;
		
		await _deduplicationService.ProcessMessage(() => ProcessMessage(message));
	}
	
	
	/// <summary>
	/// Processes the CategoryUpdatedMessage asynchronously.
	/// </summary>
	/// <param name="message">The CategoryUpdatedMessage to process.</param>
	/// <returns>A task representing the asynchronous operation.</returns>
	async Task ProcessMessage(CategoryUpdatedMessage message)
	{
		var eventData = new ExpenseServiceCategoryEventJsonData(message.Id, message.NewName, message.NewDescription, message.UserId, message.CreatedAt, message.CreatedBy, message.UpdatedAt, message.UpdatedBy);

		var categoryEvent = new ExpenseServiceCategoryEvent(message.EventId, "Update", DateTime.UtcNow, message.UserId, eventData);

		await _dbContext.ExpenseService_CategoryEvents.AddAsync(categoryEvent);
		await _dbContext.SaveChangesAsync();
	}
}