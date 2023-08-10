
namespace ExpenseService.Infrastructure.Repositories;

public class ExpenseRepository : Repository, IExpenseRepository
{
	public ExpenseRepository(AppDbContext dbContext) : base(dbContext)
	{
	}

	public void Add(Expense entity)
	{
		throw new NotImplementedException();
	}

	public void Delete(Expense entity)
	{
		throw new NotImplementedException();
	}

	public IQueryable<Expense> GetAll()
	{
		throw new NotImplementedException();
	}

	public Expense GetById(int id)
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

	public void Dispose()
	{
		throw new NotImplementedException();
	}
}
