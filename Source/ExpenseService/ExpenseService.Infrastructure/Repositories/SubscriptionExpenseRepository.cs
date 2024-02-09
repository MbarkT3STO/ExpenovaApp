
namespace ExpenseService.Infrastructure.Repositories;

public class SubscriptionExpenseRepository : Repository, ISubscriptionExpenseRepository
{
	public SubscriptionExpenseRepository(AppDbContext dbContext) : base(dbContext)
	{
	}

	public void Add(SubscriptionExpense entity)
	{
		throw new NotImplementedException();
	}

	public Task AddAsync(SubscriptionExpense entity)
	{
		throw new NotImplementedException();
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
		throw new NotImplementedException();
	}

	public IQueryable<SubscriptionExpense> Get()
	{
		throw new NotImplementedException();
	}

	public Task<IEnumerable<SubscriptionExpense>> GetAsync(CancellationToken cancellationToken = default)
	{
		throw new NotImplementedException();
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

	public Task<SubscriptionExpense> GetByIdOrThrowAsync(Guid id, CancellationToken cancellationToken = default)
	{
		throw new NotImplementedException();
	}

	public Task<IQueryable<SubscriptionExpense>> GetSubscriptionExpensesByUserIdAndCategoryIdAsync(int userId, int categoryId)
	{
		throw new NotImplementedException();
	}

	public Task<IQueryable<SubscriptionExpense>> GetSubscriptionExpensesByUserIdAsync(int userId)
	{
		throw new NotImplementedException();
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

	public Task UpdateAsync(SubscriptionExpense entity)
	{
		throw new NotImplementedException();
	}
}
