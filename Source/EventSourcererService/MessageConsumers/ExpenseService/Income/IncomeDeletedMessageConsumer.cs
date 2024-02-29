using Messages.ExpenseServiceMessages.Income;

namespace EventSourcererService.MessageConsumers.ExpenseService.Income;

public class IncomeDeletedMessageConsumer: BaseConsumer<IncomeDeletedMessage>
{
    public IncomeDeletedMessageConsumer(AppDbContext dbContext, IDeduplicationService deduplicationService): base(dbContext, deduplicationService)
    {
    }

    protected override async Task ProcessMessage(IncomeDeletedMessage message)
    {
        var eventData    = new ExpenseServiceIncomeEventJsonData(message.Id, message.Amount, message.Description, message.Date, message.CategoryId, message.UserId, message.CreatedAt, message.CreatedBy, message.LastUpdatedAt, message.LastUpdatedBy, message.DeletedAt, message.DeletedBy);
        var expenseEvent = new ExpenseServiceIncomeEvent(message.EventId, "Delete", DateTime.UtcNow, message.UserId, eventData);

        await _dbContext.ExpenseService_IncomeEvents.AddAsync(expenseEvent);
        await _dbContext.SaveChangesAsync();
    }
}
