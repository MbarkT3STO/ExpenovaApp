using MassTransit;
using Messages.ExpenseServiceMessages.Expense;

namespace EventSourcererService.MessageConsumers.ExpenseService.Expense;

public class ExpenseServiceExpenseCreatedMessageConsumer: BaseConsumer<ExpenseCreatedMessage>
{
	public ExpenseServiceExpenseCreatedMessageConsumer(AppDbContext dbContext, IDeduplicationService deduplicationService): base(dbContext, deduplicationService)
	{
	}

	public override async Task Consume(ConsumeContext<ExpenseCreatedMessage> context)
	{
		var message      = context.Message;
		var hasProcessed = await _deduplicationService.HasProcessed(message.EventId);

		if (hasProcessed) return;

		await _deduplicationService.ProcessMessage(() => ProcessMessage(message));
	}


	/// <summary>
	/// Processes the ExpenseCreatedMessage asynchronously.
	/// </summary>
	/// <param name="message">The ExpenseCreatedMessage to process.</param>
	/// <returns>A task representing the asynchronous operation.</returns>
	async Task ProcessMessage(ExpenseCreatedMessage message)
	{
		var eventData = new ExpenseServiceExpenseEventJsonData(message.Id, message.Amount, message.Description, message.Date, message.CategoryId, message.UserId, message.CreatedAt, message.CreatedBy);

		var expenseEvent = new ExpenseServiceExpenseEvent(message.EventId, "Create", DateTime.UtcNow, message.UserId, eventData);

		await _dbContext.ExpenseService_ExpenseEvents.AddAsync(expenseEvent);
		await _dbContext.SaveChangesAsync();
	}
}
