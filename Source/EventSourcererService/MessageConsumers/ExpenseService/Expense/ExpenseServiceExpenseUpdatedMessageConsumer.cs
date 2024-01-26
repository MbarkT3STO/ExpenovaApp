using MassTransit;
using Messages.ExpenseServiceMessages.Expense;

namespace EventSourcererService.MessageConsumers.ExpenseService.Expense;

public class ExpenseServiceExpenseUpdatedMessageConsumer : BaseConsumer<ExpenseUpdatedMessage>
{
	public ExpenseServiceExpenseUpdatedMessageConsumer(AppDbContext dbContext, IDeduplicationService deduplicationService) : base(dbContext, deduplicationService)
	{
	}

	public override async Task Consume(ConsumeContext<ExpenseUpdatedMessage> context)
	{
		var message      = context.Message;
		var hasProcessed = await _deduplicationService.HasProcessed(message.EventId);

		if (hasProcessed) return;

		await _deduplicationService.ProcessMessage(() => ProcessMessage(message));
	}


	/// <summary>
	/// Processes the ExpenseUpdatedMessage asynchronously.
	/// </summary>
	/// <param name="message">The ExpenseUpdatedMessage to process.</param>
	/// <returns>A task representing the asynchronous operation.</returns>
	async Task ProcessMessage(ExpenseUpdatedMessage message)
	{
		var eventData = new ExpenseServiceExpenseEventJsonData(message.Id, message.Amount, message.Description, message.Date, message.CategoryId, message.UserId,message.CreatedAt, message.CreatedBy, message.LastUpdatedAt, message.LastUpdatedBy);

		var expenseEvent = new ExpenseServiceExpenseEvent(message.EventId, "Update", DateTime.UtcNow, message.UserId, eventData);

		await _dbContext.ExpenseService_ExpenseEvents.AddAsync(expenseEvent);
		await _dbContext.SaveChangesAsync();
	}
}
