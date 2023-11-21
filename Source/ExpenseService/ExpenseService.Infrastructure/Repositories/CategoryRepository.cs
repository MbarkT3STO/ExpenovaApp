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

	public async Task DeleteAsync(Category entity)
	{
		var categoryEntity = _mapper.Map<CategoryEntity>(entity);
		
		_dbContext.Categories.Remove(categoryEntity);
		await _dbContext.SaveChangesAsync();
	}

	public async Task DeleteAsync(Category entity, CancellationToken cancellationToken)
	{
		var categoryEntity = _mapper.Map<CategoryEntity>(entity);
		
		_dbContext.Categories.Remove(categoryEntity);
		await _dbContext.SaveChangesAsync(cancellationToken);
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

	public Category GetById(Guid id)
	{
		var category = _dbContext.Categories.FirstOrDefault(c => c.Id == id);
		
		var domainCategory = _mapper.Map<Category>(category);
		
		return domainCategory;
	}

	public async Task<Category> GetByIdAsync(Guid id)
	{
		var category = await _dbContext.Categories.FirstOrDefaultAsync(c => c.Id == id);
		
		var domainCategory = _mapper.Map<Category>(category);
		
		return domainCategory;
	}
	
	public async Task<Category> GetByIdAsync(Guid id, CancellationToken cancellationToken)
	{
		var category = await _dbContext.Categories.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
		
		var domainCategory = _mapper.Map<Category>(category);
		
		return domainCategory;
	}


	public async Task<IEnumerable<Category>> GetCategoriesByUserIdAsync(string userId)
	{
		var categories = await _dbContext.Categories.Where(c => c.UserId == userId).ToListAsync();
		
		var domainCategories = _mapper.Map<IEnumerable<Category>>(categories);
		
		return domainCategories;
	}

	public async Task<IEnumerable<Category>> GetCategoriesByUserIdAsync(string userId, CancellationToken cancellationToken)
	{
		var categories = await _dbContext.Categories.Where(c => c.UserId == userId).ToListAsync(cancellationToken);
		
		var domainCategories = _mapper.Map<IEnumerable<Category>>(categories);
		
		return domainCategories;
	}
	

	public void Update(Category entity)
	{
		var categoryEntity = _mapper.Map<CategoryEntity>(entity);
		
		_dbContext.Categories.Update(categoryEntity);
		
		_dbContext.SaveChanges();
	}

	public async Task UpdateAsync(Category entity)
	{
		var categoryEntity = await _dbContext.Categories.FirstOrDefaultAsync(c => c.Id == entity.Id);
		
		_mapper.Map(entity, categoryEntity);
		
		_dbContext.Categories.Update(categoryEntity);
		
		await _dbContext.SaveChangesAsync();
	}


	public async Task UpdateAsync(Category entity, CancellationToken cancellationToken)
	{
		var categoryEntity = _mapper.Map<CategoryEntity>(entity);
		
		_dbContext.Categories.Update(categoryEntity);
		
		await _dbContext.SaveChangesAsync(cancellationToken);
	}


}
