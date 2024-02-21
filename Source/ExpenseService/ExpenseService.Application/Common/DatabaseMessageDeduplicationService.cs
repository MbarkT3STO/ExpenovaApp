using System.Linq.Expressions;
using ExpenseService.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ExpenseService.Application.Common;

public class DatabaseMessageDeduplicationService<TEvent, TEventData>: IDeduplicationService
	where TEvent: DomainEvent<TEventData>
	where TEventData: class
{
	private readonly AppDbContext _dbContext;

	public DatabaseMessageDeduplicationService(AppDbContext dbContext)
	{
		_dbContext = dbContext;
	}


	public async Task<bool> HasProcessed(Guid eventId)
	{
		return await _dbContext.OutboxMessages.AnyAsync(e => e. == eventId);
	}

	public virtual Task ProcessEventAsync(TEvent @event)
	{
		throw new NotImplementedException();
	}

	public async Task ProcessEventAsync(Expression<Func<Task>> processEventFunc)
	{
		await processEventFunc.Compile().Invoke();
	}

    public async Task ProcessEventAsync<TEvent1>(TEvent1 @event) where TEvent1 : class
    {
        await ProcessEventAsync(@event);
    }
}