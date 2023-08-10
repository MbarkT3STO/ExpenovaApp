namespace ExpenseService.Infrastructure.Repositories;

public class CategoryRepository : Repository, ICategoryRepository
{
	public CategoryRepository(AppDbContext dbContext) : base(dbContext)
	{
	}

	public void Add(Category entity)
	{
		
	}

	public void Delete(Category entity)
	{
		throw new NotImplementedException();
	}
	
	public IQueryable<Category> GetAll()
	{
		throw new NotImplementedException();
	}

	public Category GetById(int id)
	{
		throw new NotImplementedException();
	}

	public Task<IQueryable<Category>> GetCategoriesByUserIdAsync(int userId)
	{
		throw new NotImplementedException();
	}

	public void Update(Category entity)
	{
		throw new NotImplementedException();
	}


	public void Dispose()
	{
		throw new NotImplementedException();
	}
}
