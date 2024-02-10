namespace ExpenseService.Infrastructure.Repositories;

public class UserRepository : Repository, IUserRepository
{
	public UserRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
	{
	}

	public async Task<User> AddAsync(User entity, CancellationToken cancellationToken = default)
	{
		var userEntity = _mapper.Map<UserEntity>(entity);

		await _dbContext.Users.AddAsync(userEntity, cancellationToken);
		await _dbContext.SaveChangesAsync(cancellationToken);

		// Reload the user entity to get the generated id
		await _dbContext.Entry(userEntity).ReloadAsync(cancellationToken);

		return entity;
	}

	public void Delete(User entity)
	{
		throw new NotImplementedException();
	}

	public Task DeleteAsync(User entity)
	{
		throw new NotImplementedException();
	}

	public Task DeleteAsync(User entity, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}

	public void Dispose()
	{
		GC.SuppressFinalize(this);
	}

	public IQueryable<User> Get()
	{
		throw new NotImplementedException();
	}

	public Task<IEnumerable<User>> GetAsync(CancellationToken cancellationToken = default)
	{
		throw new NotImplementedException();
	}

	public User GetById(string id)
	{
		throw new NotImplementedException();
	}

	public async Task<User> GetByIdAsync(string id)
	{
		var userEntity = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);

		var user = _mapper.Map<User>(userEntity);

		return user;
	}

	public Task<User> GetByIdAsync(string id, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}

	public async Task<User> GetByIdOrThrowAsync(string id, CancellationToken cancellationToken = default)
	{
		var userEntity = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id, cancellationToken);

		if (userEntity is null) throw new NotFoundException($"The user with ID {id} does not exist.");

		var user = _mapper.Map<User>(userEntity);

		return user;
	}

	public bool IsExist(string id)
	{
		var user = _dbContext.Users.FirstOrDefault(u => u.Id == id);

		return user != null;
	}

	public async Task<bool> IsExistAsync(string id)
	{
		var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);

		return user != null;
	}

	public async Task<bool> IsExistAsync(string id, CancellationToken cancellationToken)
	{
		var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id, cancellationToken);

		return user != null;
	}

	public async Task<bool> IsUserExistsAsync(string id, CancellationToken cancellationToken)
	{
		var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id, cancellationToken);

		return user != null;
	}

	public async Task ThrowIfNotExistAsync(string id, CancellationToken cancellationToken = default)
	{
		var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id, cancellationToken);

		if (user is null) throw new NotFoundException($"The user with ID #{id} does not exist.");
	}

	public void Update(User entity)
	{
		throw new NotImplementedException();
	}

	public Task UpdateAsync(User entity)
	{
		throw new NotImplementedException();
	}
}
