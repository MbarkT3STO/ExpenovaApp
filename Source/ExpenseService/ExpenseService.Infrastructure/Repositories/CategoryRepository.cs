namespace ExpenseService.Infrastructure.Repositories;

public class CategoryRepository : Repository, ICategoryRepository
{
	public CategoryRepository(AppDbContext dbContext) : base(dbContext)
	{
	}

    public void Add(Category entity)
    {
        throw new NotImplementedException();
    }

    public Task AddAsync(Category entity)
    {
        throw new NotImplementedException();
    }

    public void Delete(Category entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Category entity)
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }

    public IQueryable<Category> Get()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Category>> GetAsync()
    {
        throw new NotImplementedException();
    }

    public Category GetById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Category> GetByIdAsync(int id)
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

    public Task UpdateAsync(Category entity)
    {
        throw new NotImplementedException();
    }
}
