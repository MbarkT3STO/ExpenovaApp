
namespace ExpenseService.Infrastructure.Repositories;

/// <summary>
/// Represents a repository for managing subscription expenses.
/// </summary>
public class SubscriptionExpenseRepository: Repository, ISubscriptionExpenseRepository
{
	public SubscriptionExpenseRepository(AppDbContext dbContext, IMapper mapper): base(dbContext, mapper)
	{
	}

	public async Task<SubscriptionExpense> AddAsync(SubscriptionExpense entity, CancellationToken cancellationToken = default)
	{
		var mappedEntity = _mapper.Map<SubscriptionExpenseEntity>(entity);

		await _dbContext.SubscriptionExpenses.AddAsync(mappedEntity, cancellationToken);
		await _dbContext.SaveChangesAsync(cancellationToken);

		// Reload the expense entity to get the generated id
		await _dbContext.Entry(mappedEntity).ReloadAsync(cancellationToken);
		entity.SetId(mappedEntity.Id);

		return entity;
	}

	public void Delete(SubscriptionExpense entity)
	{
		throw new NotImplementedException();
	}

	public Task DeleteAsync(SubscriptionExpense entity)
	{
		throw new NotImplementedException();
	}

	public Task DeleteAsync(SubscriptionExpense entity, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}

	public void Dispose()
	{
		_dbContext.Dispose();

		GC.SuppressFinalize(this);
	}

	public IQueryable<SubscriptionExpense> Get()
	{
		throw new NotImplementedException();
	}

	public async Task<IEnumerable<SubscriptionExpense>> GetAsync(CancellationToken cancellationToken = default)
	{
		var entities = await _dbContext.SubscriptionExpenses.Include(e => e.Category).Include(e => e.User).ToListAsync(cancellationToken);
		var expenses = _mapper.Map<IEnumerable<SubscriptionExpense>>(entities);

		return expenses;
	}

	public SubscriptionExpense GetById(Guid id)
	{
		throw new NotImplementedException();
	}

	public Task<SubscriptionExpense> GetByIdAsync(Guid id)
	{
		throw new NotImplementedException();
	}

	public Task<SubscriptionExpense> GetByIdAsync(Guid id, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}

	public async Task<SubscriptionExpense> GetByIdOrThrowAsync(Guid id, CancellationToken cancellationToken = default)
	{
		var entity = await _dbContext.SubscriptionExpenses.Include(e => e.Category).Include(e => e.User).FirstOrDefaultAsync(e => e.Id == id, cancellationToken);

		if (entity == null) throw new NotFoundException($"Subscription expense with id #{id} does not exist.");

		var expense = _mapper.Map<SubscriptionExpense>(entity);

		return expense;
	}

	public async Task<IEnumerable<SubscriptionExpense>> GetSubscriptionExpensesByCategoryAsync(Guid categoryId, CancellationToken cancellationToken = default)
	{
		var expenses = await _dbContext.SubscriptionExpenses
										.Where(e => e.CategoryId == categoryId)
										.Include(e => e.Category)
										.Include(e => e.User)
										.ToListAsync(cancellationToken);

		var mappedExpenses = _mapper.Map<IEnumerable<SubscriptionExpense>>(expenses);

		return mappedExpenses.AsQueryable();
	}

	public async Task<IEnumerable<SubscriptionExpense>> GetSubscriptionExpensesByUserAndCategoryAsync(string userId, Guid categoryId, CancellationToken cancellationToken = default)
	{
		var expenses = await _dbContext.SubscriptionExpenses
										.Where(e => e.UserId == userId && e.CategoryId == categoryId)
										.Include(e => e.Category)
										.Include(e => e.User)
										.ToListAsync(cancellationToken);

		var mappedExpenses = _mapper.Map<IEnumerable<SubscriptionExpense>>(expenses);

		return mappedExpenses;
	}

	public async Task<IEnumerable<SubscriptionExpense>> GetSubscriptionExpensesByUserAsync(string userId, CancellationToken cancellationToken = default)
	{
		var expenses = await _dbContext.SubscriptionExpenses
										.Where(e => e.UserId == userId)
										.Include(e => e.Category)
										.Include(e => e.User)
										.ToListAsync(cancellationToken);

		var mappedExpenses = _mapper.Map<IEnumerable<SubscriptionExpense>>(expenses);

		return mappedExpenses;
	}

	public async Task ThrowIfNotExistAsync(Guid id, CancellationToken cancellationToken = default)
	{
		var exists = await _dbContext.SubscriptionExpenses.AnyAsync(e => e.Id == id, cancellationToken);

		if (!exists) throw new NotFoundException($"Subscription expense with id #{id} does not exist.");
	}

	public void Update(SubscriptionExpense entity)
	{
		throw new NotImplementedException();
	}

	public async Task UpdateAsync(SubscriptionExpense entity, CancellationToken cancellationToken)
	{
		var mappedEntity = _mapper.Map<SubscriptionExpenseEntity>(entity);

		_dbContext.SubscriptionExpenses.Update(mappedEntity);
		await _dbContext.SaveChangesAsync(cancellationToken);
	}
}
