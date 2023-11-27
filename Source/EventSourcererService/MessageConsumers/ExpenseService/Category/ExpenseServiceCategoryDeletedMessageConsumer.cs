using MassTransit;
using Messages.ExpenseServiceMessages.Category;
using RabbitMqSettings.Interfaces;

namespace EventSourcererService.MessageConsumers.ExpenseService.Category;

public class ExpenseServiceCategoryDeletedMessageConsumer: BaseConsumer<CategoryDeletedMessage>
{
	public ExpenseServiceCategoryDeletedMessageConsumer(AppDbContext dbContext, IDeduplicationService deduplicationService): base(dbContext, deduplicationService)
	{
	}

	public override async Task Consume(ConsumeContext<CategoryDeletedMessage> context)
	{
		var message      = context.Message;
		var hasProcessed = await _deduplicationService.HasProcessed(message.EventId);

		if (hasProcessed)
		{
			return;
		}

		await _deduplicationService.ProcessMessage(() => ProcessMessage(message));
	}
	
	
	async Task ProcessMessage(CategoryDeletedMessage message)
	{
		var eventData = new ExpenseServiceCategoryEventJsonData(message.Id, message.Name, message.Description, message.UserId, message.CreatedAt, message.CreatedBy, message.UpdatedAt, message.UpdatedBy);
		
		var categoryEvent = new ExpenseServiceCategoryEvent(message.EventId, "Delete", DateTime.UtcNow, message.UserId, eventData);
		
		await _dbContext.ExpenseService_CategoryEvents.AddAsync(categoryEvent);
		await _dbContext.SaveChangesAsync();
	}
}
