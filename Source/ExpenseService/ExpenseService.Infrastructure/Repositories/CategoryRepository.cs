namespace ExpenseService.Infrastructure.Repositories;

public class CategoryRepository : Repository, ICategoryRepository
{
	public CategoryRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
	{
	}

	public void Add(Category entity)
	{
		var categoryEntity = _mapper.Map<CategoryEntity>(entity);
		_dbContext.Categories.Add(categoryEntity);
		
		_dbContext.SaveChanges();
	}

	public Task AddAsync(Category entity)
	{
		return Task.Run(()=> Add(entity));
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
		GC.SuppressFinalize(this);
	}

	public IQueryable<Category> Get()
	{
		throw new NotImplementedException();
	}

	public async Task<IEnumerable<Category>> GetAsync()
	{
		var categories = await _dbContext.Categories.ToListAsync();
		
		var domainCategories = _mapper.Map<IEnumerable<Category>>(categories);
		
		return domainCategories;
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
