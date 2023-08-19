namespace EventSourcererService.Common;

/// <summary>
/// Base class for Message Consumers.
/// </summary>
public abstract class BaseConsumer
{
	protected readonly AppDbContext _dbContext;
	
	protected BaseConsumer(AppDbContext dbContext)
	{
		_dbContext = dbContext;
	}
}
