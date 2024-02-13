using MassTransit;
using Messages.ExpenseServiceMessages.Expense;

namespace EventSourcererService.MessageConsumers.ExpenseService.Expense;

public class ExpenseServiceExpenseUpdatedMessageConsumer: BaseConsumer<ExpenseUpdatedMessage>
{
	public ExpenseServiceExpenseUpdatedMessageConsumer(AppDbContext dbContext, IDeduplicationService deduplicationService): base(dbContext, deduplicationService)
	{
	}

	public override async Task Consume(ConsumeContext<ExpenseUpdatedMessage> context)
	{
		try
		{
			var hasProcessed = await _deduplicationService.HasProcessed(context.Message.EventId);

			if (hasProcessed)
			{
				return;
			}

			await _deduplicationService.ProcessMessage(() => ProcessMessage(context.Message));
		}
		catch (Exception e)
		{
			if (e is DbUpdateException)
			{
				return;
			}

			if (e.InnerException != null && e.InnerException.Message.Contains("duplicate key value violates unique constraint"))
			{
				return;
			}
		}

	}


	/// <summary>
	/// Processes the ExpenseUpdatedMessage asynchronously.
	/// </summary>
	/// <param name="message">The ExpenseUpdatedMessage to process.</param>
	/// <returns>A task representing the asynchronous operation.</returns>
	protected override async Task ProcessMessage(ExpenseUpdatedMessage message)
	{
		var expense = await _dbContext.ExpenseService_ExpenseEvents.FindAsync(message.Id);

		if (expense != null)
		{
			return;
		}

		var eventData = new ExpenseServiceExpenseEventJsonData(message.Id, message.Amount, message.Description, message.Date, message.CategoryId, message.UserId, message.CreatedAt, message.CreatedBy, message.LastUpdatedAt, message.LastUpdatedBy);
		var expenseEvent = new ExpenseServiceExpenseEvent(message.EventId, "Update", DateTime.UtcNow, message.UserId, eventData);

		await _dbContext.ExpenseService_ExpenseEvents.AddAsync(expenseEvent);
		await _dbContext.SaveChangesAsync();
	}
}
