namespace ExpenseService.Infrastructure.Repositories;

/// <summary>
/// Base class for all repositories
/// </summary>
public abstract class Repository
{
	protected readonly AppDbContext _dbContext;
	protected readonly IMapper _mapper;
	
	protected Repository(AppDbContext dbContext)
	{
		_dbContext = dbContext;
	}
	
	protected Repository(AppDbContext dbContext, IMapper mapper)
	{
		_dbContext = dbContext;
		_mapper = mapper;
	}
}
