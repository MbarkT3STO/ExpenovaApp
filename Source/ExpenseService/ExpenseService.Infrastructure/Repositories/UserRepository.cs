namespace ExpenseService.Infrastructure.Repositories;

public class UserRepository : Repository, IUserRepository
{
	public UserRepository(AppDbContext dbContext) : base(dbContext)
	{
	}

    public void Add(User entity)
    {
        throw new NotImplementedException();
    }

    public Task AddAsync(User entity)
    {
        throw new NotImplementedException();
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
        throw new NotImplementedException();
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