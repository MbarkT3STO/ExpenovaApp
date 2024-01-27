using System.Linq.Expressions;

namespace EventSourcererService.Services;

public class DatabaseMessageDeduplicationService<TEntity> : IDeduplicationService where TEntity : class, IEvent
{
	private readonly AppDbContext _dbContext;

	public DatabaseMessageDeduplicationService(AppDbContext dbContext)
	{
		_dbContext = dbContext;
	}


	public async Task<bool> HasProcessed(Guid messageId)
	{
		return await _dbContext.Set<TEntity>().AnyAsync(e => e.Id == messageId);
	}


	public Task ProcessMessage<TMessage>(TMessage message)
	{
		throw new NotImplementedException();
	}


	public async Task ProcessMessage(Expression<Func<Task>> processMessageFunc)
	{
		await processMessageFunc.Compile().Invoke();
	}
}