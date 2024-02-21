using System.Linq.Expressions;
using ExpenseService.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ExpenseService.Application.Common;

public class DomainEventDatabaseDeduplicationService: IDomainEventDeduplicationService
{
	private readonly AppDbContext _dbContext;

	public DomainEventDatabaseDeduplicationService(AppDbContext dbContext)
	{
		_dbContext = dbContext;
	}


	public async Task<bool> HasProcessed(Guid eventId)
	{
		return await _dbContext.OutboxMessages.AnyAsync(e => e.EventId == eventId);
	}

	public virtual Task ProcessEventAsync<TEvent>(TEvent @event) where TEvent : DomainEvent<TEvent>
	{
		throw new NotImplementedException();
	}

	public async Task ProcessEventAsync(Expression<Func<Task>> processEventFunc)
	{
		await processEventFunc.Compile().Invoke();
	}
}