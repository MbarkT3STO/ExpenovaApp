using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseService.Infrastructure.Repositories;

/// <summary>
/// Represents a repository for managing income entities.
/// </summary>
public class IncomeRepository : Repository, IIncomeRepository
{
	public IncomeRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
	{
	}

	public async Task<Income> AddAsync(Income entity, CancellationToken cancellationToken = default)
	{
		var income = _mapper.Map<IncomeEntity>(entity);

		await _dbContext.Incomes.AddAsync(income, cancellationToken);

		await _dbContext.SaveChangesAsync(cancellationToken);
		// Reload the object with User and Category
		await _dbContext.Entry(income).Reference(i => i.Category).LoadAsync(cancellationToken);
		await _dbContext.Entry(income).Reference(i => i.User).LoadAsync(cancellationToken);

		var result = _mapper.Map<Income>(income);

		return result;
	}

	public void Delete(Income entity)
	{
		throw new NotImplementedException();
	}

	public Task DeleteAsync(Income entity)
	{
		throw new NotImplementedException();
	}

	public async Task DeleteAsync(Income entity, CancellationToken cancellationToken)
	{
		// Soft delete
		var income = _mapper.Map<IncomeEntity>(entity);

		_dbContext.Incomes.Update(income);
		await _dbContext.SaveChangesAsync(cancellationToken);
	}

	public void Dispose()
	{
		var disposableDbContext = _dbContext as IDisposable;

		disposableDbContext?.Dispose();

		GC.SuppressFinalize(this);
	}

	public IQueryable<Income> Get()
	{
		throw new NotImplementedException();
	}

	public async Task<IEnumerable<Income>> GetAsync(CancellationToken cancellationToken = default)
	{
		var incomes = await _dbContext.Incomes
										.Include(i => i.Category)
										.Include(i => i.User)
										.ToListAsync(cancellationToken);

		var mappedIncomes = _mapper.Map<IEnumerable<Income>>(incomes);

		return mappedIncomes;
	}

	public Income GetById(Guid id)
	{
		throw new NotImplementedException();
	}

	public Task<Income> GetByIdAsync(Guid id)
	{
		throw new NotImplementedException();
	}

	public Task<Income> GetByIdAsync(Guid id, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}

	public async Task<Income> GetByIdOrThrowAsync(Guid id, CancellationToken cancellationToken = default)
	{
		var income = await _dbContext.Incomes
									.Include(i => i.Category)
									.Include(i => i.User)
									.FirstOrDefaultAsync(i => i.Id == id, cancellationToken);

		if (income == null)
			throw new NotFoundException($"The income with ID #{id} does not exist.");

		var result = _mapper.Map<Income>(income);

		return result;
	}

	public Task ThrowIfNotExistAsync(Guid id, CancellationToken cancellationToken = default)
	{
		throw new NotImplementedException();
	}

	public void Update(Income entity)
	{
		throw new NotImplementedException();
	}

	public async Task UpdateAsync(Income entity, CancellationToken cancellationToken = default)
	{
		var income = _mapper.Map<IncomeEntity>(entity);

		_dbContext.Incomes.Update(income);

		await _dbContext.SaveChangesAsync(cancellationToken);
	}
}
