namespace ExpenseService.Infrastructure.Repositories;

public class ExpenseRepository: Repository, IExpenseRepository
{
	public ExpenseRepository(AppDbContext dbContext, IMapper mapper): base(dbContext, mapper)
	{
	}

	public async Task<Expense> AddAsync(Expense entity, CancellationToken cancellationToken = default)
	{
		var expenseEntity = _mapper.Map<ExpenseEntity>(entity);

		await _dbContext.Expenses.AddAsync(expenseEntity, cancellationToken);
		await _dbContext.SaveChangesAsync(cancellationToken);

		// Reload the expense entity to get the generated id
		await _dbContext.Entry(expenseEntity).ReloadAsync(cancellationToken);
		entity.SetId(expenseEntity.Id);

		return entity;
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

	public Expense GetById(Guid id)
	{
		var expenseEntity = _dbContext.Expenses.Include(e => e.Category).Include(e => e.User).FirstOrDefault(e => e.Id == id);
		var expense       = _mapper.Map<Expense>(expenseEntity);

		return expense;
	}

	public async Task<Expense> GetByIdAsync(Guid id)
	{
		var expenseEntity = await _dbContext.Expenses.Include(e => e.Category).Include(e => e.User).FirstOrDefaultAsync(e => e.Id == id);
		var expense       = _mapper.Map<Expense>(expenseEntity);

		return expense;
	}

	public Task<Expense> GetByIdAsync(Guid id, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}

	public async Task<Expense> GetByIdOrThrowAsync(Guid id, CancellationToken cancellationToken = default)
	{
		var expenseEntity = await _dbContext.Expenses.Include(e => e.Category).Include(e => e.User).FirstOrDefaultAsync(e => e.Id == id, cancellationToken);

		if (expenseEntity is null) throw new NotFoundException($"The expense with ID {id} was not found.");


		var expense = _mapper.Map<Expense>(expenseEntity);

		return expense;
	}

	public async Task<IEnumerable<Expense>> GetExpensesByCategoryAsync(Guid categoryId, CancellationToken cancellationToken = default)
	{
		var expenses    = await _dbContext.Expenses.Include(e => e.Category).Include(e => e.User).Where(e => e.CategoryId == categoryId).ToListAsync(cancellationToken: cancellationToken);
		var expensesDto = _mapper.Map<IEnumerable<Expense>>(expenses);

		return expensesDto;
	}

	public Task<IQueryable<Expense>> GetExpensesByUserIdAndCategoryIdAsync(string userId, Guid categoryId)
	{
		throw new NotImplementedException();
	}

	public async Task<IEnumerable<Expense>> GetExpensesByUserAndCategoryAsync(string userId, Guid categoryId, CancellationToken cancellationToken = default)
	{
		var expenses    = await _dbContext.Expenses.Include(e => e.Category).Include(e => e.User).Where(e => e.UserId == userId && e.CategoryId == categoryId).ToListAsync(cancellationToken: cancellationToken);
		var expensesDto = _mapper.Map<IEnumerable<Expense>>(expenses);

		return expensesDto;
	}

	public async Task<IEnumerable<Expense>> GetExpensesByUserAsync(string userId, CancellationToken cancellationToken = default)
	{
		var expenses    = await _dbContext.Expenses.Include(e => e.Category).Include(e => e.User).Where(e => e.UserId == userId).ToListAsync(cancellationToken: cancellationToken);
		var expensesDto = _mapper.Map<IEnumerable<Expense>>(expenses);

		return expensesDto;
	}


	public async Task SoftDeleteAsync(Expense expense, CancellationToken cancellationToken = default)
	{
		var expenseEntity = _mapper.Map<ExpenseEntity>(expense);

		_dbContext.Expenses.Update(expenseEntity);
		await _dbContext.SaveChangesAsync(cancellationToken);
	}

	public void Update(Expense entity)
	{
		throw new NotImplementedException();
	}

	public async Task UpdateAsync(Expense entity)
	{
		var expenseEntity = _mapper.Map<ExpenseEntity>(entity);

		_dbContext.Expenses.Update(expenseEntity);
		await _dbContext.SaveChangesAsync();
	}

	public async Task ThrowIfNotExistAsync(Guid id, CancellationToken cancellationToken = default)
	{
		var exists = await _dbContext.Expenses.AnyAsync(e => e.Id == id, cancellationToken);

		if (!exists) throw new NotFoundException($"The expense with ID #{id} was not found.");
	}
}
