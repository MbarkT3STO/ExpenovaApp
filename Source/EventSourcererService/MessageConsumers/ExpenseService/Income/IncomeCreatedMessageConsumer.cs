using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Messages.ExpenseServiceMessages.Income;

namespace EventSourcererService.MessageConsumers.ExpenseService.Income;

public class IncomeCreatedMessageConsumer : BaseConsumer<IncomeUpdatedMessage>
{
	public IncomeCreatedMessageConsumer(AppDbContext dbContext, IDeduplicationService deduplicationService) : base(dbContext, deduplicationService)
	{
	}

	protected override async Task ProcessMessage(IncomeUpdatedMessage message)
	{
		var eventData = new ExpenseServiceIncomeEventJsonData(message.Id, message.Amount, message.Description, message.Date, message.CategoryId, message.UserId, message.CreatedAt, message.CreatedBy, message.LastUpdatedAt, message.LastUpdatedBy, message.DeletedAt, message.DeletedBy);
		var expenseEvent = new ExpenseServiceIncomeEvent(message.EventId, "Create", DateTime.UtcNow, message.UserId, eventData);

		await _dbContext.ExpenseService_IncomeEvents.AddAsync(expenseEvent);
		await _dbContext.SaveChangesAsync();
	}
}
