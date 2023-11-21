using MassTransit;
using Messages.ExpenseServiceMessages.Category;

namespace EventSourcererService.MessageConsumers.ExpenseService.Category;

public class ExpenseServiceCategoryUpdatedMessageConsumer : BaseConsumer, IConsumer<CategoryUpdatedMessage>
{
    public ExpenseServiceCategoryUpdatedMessageConsumer(AppDbContext dbContext) : base(dbContext)
    {
    };

    public async Task Consume(ConsumeContext<CategoryUpdatedMessage> context)
    {
        var message = context.Message;
        
        var eventData = new ExpenseServiceCategoryEventJsonData(message.Id, message.NewName, message.NewDescription, message.UserId, message.CreatedAt, message.CreatedBy, message.UpdatedAt, message.UpdatedBy);
        
        var categoryEvent = new ExpenseServiceCategoryEvent("Update", DateTime.UtcNow, message.UserId, eventData);
        
        await _dbContext.ExpenseService_CategoryEvents.AddAsync(categoryEvent);
        await _dbContext.SaveChangesAsync();
    }
}