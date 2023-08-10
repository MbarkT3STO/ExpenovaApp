namespace ExpenseService.Infrastructure.Repositories;

/// <summary>
/// Base class for all repositories
/// </summary>
public abstract class Repository
{
	protected readonly AppDbContext _dbContext;
	
	protected Repository(AppDbContext dbContext)
	{
		_dbContext = dbContext;
	}
}
