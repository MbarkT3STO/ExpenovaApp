namespace ExpenseService.Infrastructure.Repositories;

public class UserRepository : Repository, IUserRepository
{
	public UserRepository(AppDbContext dbContext) : base(dbContext)
	{
	}

	public IQueryable<User> GetAll()
	{
		throw new NotImplementedException();
	}

	public User GetById(string id)
	{
		throw new NotImplementedException();
	}

	public void Add(User user)
	{
		// implementation here
	}

	public void Update(User user)
	{
		// implementation here
	}

	public void Delete(User user)
	{
		// implementation here
	}

	public void Dispose()
	{
		// implementation here
	}
}