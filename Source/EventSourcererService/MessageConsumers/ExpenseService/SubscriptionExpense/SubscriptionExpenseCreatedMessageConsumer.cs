using Messages.ExpenseServiceMessages.SubscriptionExpense;

namespace EventSourcererService.MessageConsumers.ExpenseService.SubscriptionExpense;

public class SubscriptionExpenseCreatedMessageConsumer : BaseConsumer<SubscriptionExpenseCreatedMessage>
{
    public SubscriptionExpenseCreatedMessageConsumer(AppDbContext dbContext, IDeduplicationService deduplicationService) : base(dbContext, deduplicationService)
    {
    }

    protected override async Task ProcessMessage(SubscriptionExpenseCreatedMessage message)
    {
        var eventData = new ExpenseServiceSubscriptionExpenseEventJsonData(message.Id, message.Amount, message.Description, message.UserId, message.CategoryId, message.StartDate, message.EndDate, message.RecurrenceInterval, message.BillingAmount, message.CreatedAt, message.CreatedBy, message.LastUpdatedAt, message.LastUpdatedBy, message.DeletedAt, message.DeletedBy);

        var expenseEvent = new ExpenseServiceSubscriptionExpenseEvent(message.EventId, "Create", DateTime.UtcNow, message.UserId, eventData);

        await _dbContext.ExpenseServiceSubscription_ExpenseEvents.AddAsync(expenseEvent);
        await _dbContext.SaveChangesAsync();
    }
}
