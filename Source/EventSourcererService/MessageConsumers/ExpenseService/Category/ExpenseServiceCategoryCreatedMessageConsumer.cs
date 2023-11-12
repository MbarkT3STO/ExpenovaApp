using MassTransit;
using Messages.ExpenseServiceMessages.Category;

namespace EventSourcererService.MessageConsumers.ExpenseService.Category;

public class ExpenseServiceCategoryCreatedMessageConsumer : BaseConsumer, IConsumer<CategoryCreatedMessage>
{
	public ExpenseServiceCategoryCreatedMessageConsumer(AppDbContext dbContext) : base(dbContext)
	{
	}

	public async Task Consume(ConsumeContext<CategoryCreatedMessage> context)
	{
		var message = context.Message;
		
		var eventData = new ExpenseServiceCategoryEventJsonData(message.Id, message.Name, message.Description, message.UserId, message.CreatedAt, message.CreatedBy);
		
		var categoryEvent = new ExpenseServiceCategoryEvent("Create", DateTime.UtcNow, message.UserId, eventData);
		
		await _dbContext.ExpenseService_CategoryEvents.AddAsync(categoryEvent);
		await _dbContext.SaveChangesAsync();
	}
}
