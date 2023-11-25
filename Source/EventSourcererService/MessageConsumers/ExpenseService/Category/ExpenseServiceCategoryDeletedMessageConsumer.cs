using MassTransit;
using Messages.ExpenseServiceMessages.Category;

namespace EventSourcererService.MessageConsumers.ExpenseService.Category;

public class ExpenseServiceCategoryDeletedMessageConsumer : BaseConsumer<CategoryDeletedMessage>
{
    public ExpenseServiceCategoryDeletedMessageConsumer(AppDbContext dbContext) : base(dbContext)
    {
    }

    public override async Task Consume(ConsumeContext<CategoryDeletedMessage> context)
    {
        var message = context.Message;
        
        var eventData = new ExpenseServiceCategoryEventJsonData(message.Id, message.Name, message.Description, message.UserId, message.CreatedAt, message.CreatedBy, message.UpdatedAt, message.UpdatedBy);
        
        var categoryEvent = new ExpenseServiceCategoryEvent("Delete", DateTime.UtcNow, message.UserId, eventData);
        
        await _dbContext.ExpenseService_CategoryEvents.AddAsync(categoryEvent);
        await _dbContext.SaveChangesAsync();
    }
}
