
using ExpenseService.Domain.Events;
using Newtonsoft.Json;

namespace ExpenseService.Infrastructure.Repositories;

public class CategoryRepository: Repository, ICategoryRepository
{
	public CategoryRepository(AppDbContext dbContext, IMapper mapper): base(dbContext, mapper)
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
		return Task.Run(() => Add(entity));
	}


	public void Add(Category entity, CategoryCreatedEvent categoryCreatedEvent)
	{
		var transaction = _dbContext.Database.BeginTransaction();

		try
		{
			var categoryEntity = _mapper.Map<CategoryEntity>(entity);

			_dbContext.Categories.Add(categoryEntity);
			_dbContext.SaveChanges();

			var eventAsJson = JsonConvert.SerializeObject(categoryCreatedEvent);
			// var outboxEvent = new OutboxMessage(nameof(CategoryCreatedEvent), eventAsJson);

			// _dbContext.OutboxMessages.Add(outboxEvent);
			// _dbContext.SaveChanges();

			_dbContext.Database.CommitTransaction();
		}
		catch (Exception)
		{
			transaction.Rollback();
			throw;
		}
	}

	public Task AddAsync(Category entity, CategoryCreatedEvent categoryCreatedEvent)
	{
		return Task.Run(() => Add(entity, categoryCreatedEvent));
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

	// public async Task DeleteAsync(Category entity, CancellationToken cancellationToken)
	// {
	// 	_dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

	// 	var categoryEntity = _mapper.Map<CategoryEntity>(entity);

	// 	categoryEntity.IsDeleted = true;
	// 	_dbContext.Entry(categoryEntity).State = EntityState.Modified;

	// 	await _dbContext.SaveChangesAsync(cancellationToken);
	// }

	public async Task DeleteAsync(Category entity, CancellationToken cancellationToken)
	{
		var categoryEntity = await _dbContext.Categories.FirstOrDefaultAsync(c => c.Id == entity.Id, cancellationToken);

		if (categoryEntity != null)
		{
			categoryEntity.IsDeleted = true;
			categoryEntity.DeletedAt = entity.DeletedAt;
			categoryEntity.DeletedBy = entity.DeletedBy;

			await _dbContext.SaveChangesAsync(cancellationToken);
		}
	}

	public void Dispose()
	{
		GC.SuppressFinalize(this);
	}

	public IQueryable<Category> Get()

	{
		throw new NotImplementedException();
	}

	public async Task<IEnumerable<Category>> GetAsync(CancellationToken cancellationToken = default)
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
		// Stop tracking the entity so that the entity is not cached in the DbContext.
		// _dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

		var category = await _dbContext.Categories.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

		var domainCategory = _mapper.Map<Category>(category);

		return domainCategory;
	}

	public async Task<Category> GetByNameAndUserIdAsync(string name, string userId)
	{
		var category = await _dbContext.Categories.FirstOrDefaultAsync(c => c.Name == name && c.UserId == userId);

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

	public async Task<Category> GetByIdAndUserIdAsync(Guid id, string userId)
	{
		var category = await _dbContext.Categories.FirstOrDefaultAsync(c => c.Id == id && c.UserId == userId);

		var domainCategory = _mapper.Map<Category>(category);

		return domainCategory;
	}

	public async Task<Category> GetByIdOrThrowAsync(Guid id, CancellationToken cancellationToken = default)
	{
		var categoryEntity = await _dbContext.Categories.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

		if (categoryEntity is null) throw new NotFoundException($"The category with ID {id} was not found.");

		var category = _mapper.Map<Category>(categoryEntity);

		return category;
	}

	public async Task<Category> GetByIdAndUserOrThrowAsync(Guid id, string userId, CancellationToken cancellationToken = default)
	{
		var categoryEntity = await _dbContext.Categories.FirstOrDefaultAsync(c => c.Id == id && c.UserId == userId, cancellationToken);

		if (categoryEntity is null) throw new NotFoundException($"The category with ID #{id} and user ID #{userId} was not found.");

		var category = _mapper.Map<Category>(categoryEntity);

		return category;
	}

    public async Task ThrowIfNotExistAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var categoryEntity = await _dbContext.Categories.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

		if (categoryEntity is null) throw new NotFoundException($"The category with ID #{id} was not found.");
    }
}
