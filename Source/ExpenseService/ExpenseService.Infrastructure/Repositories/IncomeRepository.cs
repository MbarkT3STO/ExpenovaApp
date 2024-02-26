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

    public Task<Income> AddAsync(Income entity, CancellationToken cancellationToken = default)
	{
		throw new NotImplementedException();
	}

	public void Delete(Income entity)
	{
		throw new NotImplementedException();
	}

	public Task DeleteAsync(Income entity)
	{
		throw new NotImplementedException();
	}

	public Task DeleteAsync(Income entity, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}

	public void Dispose()
	{
		throw new NotImplementedException();
	}

	public IQueryable<Income> Get()
	{
		throw new NotImplementedException();
	}

	public Task<IEnumerable<Income>> GetAsync(CancellationToken cancellationToken = default)
	{
		throw new NotImplementedException();
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

	public Task<Income> GetByIdOrThrowAsync(Guid id, CancellationToken cancellationToken = default)
	{
		throw new NotImplementedException();
	}

	public Task ThrowIfNotExistAsync(Guid id, CancellationToken cancellationToken = default)
	{
		throw new NotImplementedException();
	}

	public void Update(Income entity)
	{
		throw new NotImplementedException();
	}

	public Task UpdateAsync(Income entity, CancellationToken cancellationToken = default)
	{
		throw new NotImplementedException();
	}
}
