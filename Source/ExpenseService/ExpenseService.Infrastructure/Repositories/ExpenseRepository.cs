
namespace ExpenseService.Infrastructure.Repositories;

public class ExpenseRepository: Repository, IExpenseRepository
{
	public ExpenseRepository(AppDbContext dbContext, IMapper mapper): base(dbContext, mapper)
	{
	}

	public void Add(Expense entity)
	{
		throw new NotImplementedException();
	}

	public async Task AddAsync(Expense entity)
	{
		var expenseEntity = _mapper.Map<ExpenseEntity>(entity);

		await _dbContext.Expenses.AddAsync(expenseEntity);
		await _dbContext.SaveChangesAsync();
	}

	public void Delete(Expense entity)
	{
		throw new NotImplementedException();
	}

	public Task DeleteAsync(Expense entity)
	{
		throw new NotImplementedException();
	}

	public Task DeleteAsync(Expense entity, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}

	public void Dispose()
	{
		var disposableDbContext = _dbContext as IDisposable;

		disposableDbContext?.Dispose();

		GC.SuppressFinalize(this);
	}

	public IQueryable<Expense> Get()
	{
		throw new NotImplementedException();
	}

	public async Task<IEnumerable<Expense>> GetAsync(CancellationToken cancellationToken = default)
	{
		var expenses    = await _dbContext.Expenses.Include(e => e.Category).Include(e => e.User).ToListAsync(cancellationToken: cancellationToken);
		var expensesDto = _mapper.Map<IEnumerable<Expense>>(expenses);

		return expensesDto;
	}

	public Expense GetById(int id)
	{
		throw new NotImplementedException();
	}

	public Task<Expense> GetByIdAsync(int id)
	{
		throw new NotImplementedException();
	}

	public Task<Expense> GetByIdAsync(int id, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}

	public Task<IQueryable<Expense>> GetExpensesByUserIdAndCategoryIdAsync(int userId, int categoryId)
	{
		throw new NotImplementedException();
	}

	public Task<IQueryable<Expense>> GetExpensesByUserIdAsync(int userId)
	{
		throw new NotImplementedException();
	}

	public void Update(Expense entity)
	{
		throw new NotImplementedException();
	}

	public Task UpdateAsync(Expense entity)
	{
		throw new NotImplementedException();
	}



}
