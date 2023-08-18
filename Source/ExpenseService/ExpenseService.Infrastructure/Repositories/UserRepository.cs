namespace ExpenseService.Infrastructure.Repositories;

public class UserRepository : Repository, IUserRepository
{
	public UserRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
	{
	}
	
	public void Add(User entity)
	{
		var userEntity = _mapper.Map<UserEntity>(entity);
		
		_dbContext.Users.Add(userEntity);
		
		_dbContext.SaveChanges();
	}

	public Task AddAsync(User entity)
	{
		return Task.Run(() => Add(entity));
	}

	public void Delete(User entity)
	{
		throw new NotImplementedException();
	}

	public Task DeleteAsync(User entity)
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

	public Task<IEnumerable<User>> GetAsync()
	{
		throw new NotImplementedException();
	}

	public User GetById(string id)
	{
		throw new NotImplementedException();
	}

	public Task<User> GetByIdAsync(string id)
	{
		throw new NotImplementedException();
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