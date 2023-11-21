
namespace ExpenseService.Infrastructure.Repositories;

public class SubscriptionExpenseRepository : Repository, ISubscriptionExpenseRepository
{
    public SubscriptionExpenseRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public void Add(SubscriptionExpense entity)
    {
        throw new NotImplementedException();
    }

    public Task AddAsync(SubscriptionExpense entity)
    {
        throw new NotImplementedException();
    }

    public void Delete(SubscriptionExpense entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(SubscriptionExpense entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(SubscriptionExpense entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }

    public IQueryable<SubscriptionExpense> Get()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<SubscriptionExpense>> GetAsync()
    {
        throw new NotImplementedException();
    }

    public SubscriptionExpense GetById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<SubscriptionExpense> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<SubscriptionExpense> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<IQueryable<SubscriptionExpense>> GetSubscriptionExpensesByUserIdAndCategoryIdAsync(int userId, int categoryId)
    {
        throw new NotImplementedException();
    }

    public Task<IQueryable<SubscriptionExpense>> GetSubscriptionExpensesByUserIdAsync(int userId)
    {
        throw new NotImplementedException();
    }

    public void Update(SubscriptionExpense entity)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(SubscriptionExpense entity)
    {
        throw new NotImplementedException();
    }
}
