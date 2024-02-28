using Messages.ExpenseServiceMessages.SubscriptionExpense;

namespace EventSourcererService.MessageConsumers.ExpenseService.SubscriptionExpense;

public class SubscriptionExpenseDeletedMessageConsumer: BaseConsumer<SubscriptionExpenseDeletedMessage>
{
    public SubscriptionExpenseDeletedMessageConsumer(AppDbContext dbContext, IDeduplicationService deduplicationService): base(dbContext, deduplicationService)
    {
    }

    protected override async Task ProcessMessage(SubscriptionExpenseDeletedMessage message)
    {
        var eventData    = new ExpenseServiceSubscriptionExpenseEventJsonData(message.Id, message.Amount, message.Description, message.UserId, message.CategoryId, message.StartDate, message.EndDate, message.RecurrenceInterval, message.BillingAmount, message.CreatedAt, message.CreatedBy, message.LastUpdatedAt, message.LastUpdatedBy, message.DeletedAt, message.DeletedBy);
        var expenseEvent = new ExpenseServiceSubscriptionExpenseEvent(message.EventId, "Delete", DateTime.UtcNow, message.UserId, eventData);

        await _dbContext.ExpenseService_SubscriptionExpenseEvents.AddAsync(expenseEvent);
        await _dbContext.SaveChangesAsync();
    }
}
