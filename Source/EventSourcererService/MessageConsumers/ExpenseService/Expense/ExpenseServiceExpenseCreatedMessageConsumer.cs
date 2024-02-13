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
			if (e is DbUpdateException || (e.InnerException != null && e.InnerException.Message.Contains("duplicate key value violates unique constraint")))
			{
				return;
			}
		}
	}



	protected override async Task ProcessMessage(ExpenseCreatedMessage message)
	{
		var eventData = new ExpenseServiceExpenseEventJsonData(message.Id, message.Amount, message.Description, message.Date, message.CategoryId, message.UserId, message.CreatedAt, message.CreatedBy);

		var expenseEvent = new ExpenseServiceExpenseEvent(message.EventId, "Create", DateTime.UtcNow, message.UserId, eventData);

		await _dbContext.ExpenseService_ExpenseEvents.AddAsync(expenseEvent);
		await _dbContext.SaveChangesAsync();
	}
}
