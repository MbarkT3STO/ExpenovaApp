
namespace ExpenseService.Infrastructure.Repositories;

/// <summary>
/// Represents a repository for managing subscription expenses.
/// </summary>
public class SubscriptionExpenseRepository : Repository, ISubscriptionExpenseRepository
{
	public SubscriptionExpenseRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
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
