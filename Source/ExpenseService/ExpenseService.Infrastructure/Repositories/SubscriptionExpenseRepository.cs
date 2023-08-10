namespace ExpenseService.Infrastructure.Repositories;

public class SubscriptionExpenseRepository : Repository, ISubscriptionExpenseRepository
{
    public SubscriptionExpenseRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<IQueryable<SubscriptionExpense>> GetSubscriptionExpensesByUserIdAsync(int userId)
    {
        // implementation here
        return await Task.FromResult(Enumerable.Empty<SubscriptionExpense>().AsQueryable());
    }

    public async Task<IQueryable<SubscriptionExpense>> GetSubscriptionExpensesByUserIdAndCategoryIdAsync(int userId, int categoryId)
    {
        // implementation here
        return await Task.FromResult(Enumerable.Empty<SubscriptionExpense>().AsQueryable());
    }

    public IQueryable<SubscriptionExpense> GetAll()
    {
        // implementation here
        return Enumerable.Empty<SubscriptionExpense>().AsQueryable();
    }

    public SubscriptionExpense GetById(int id)
    {
        // implementation here
        return null;
    }

    public void Add(SubscriptionExpense entity)
    {
        // implementation here
    }

    public void Update(SubscriptionExpense entity)
    {
        // implementation here
    }

    public void Delete(SubscriptionExpense entity)
    {
        // implementation here
    }

    public void Dispose()
    {
        // implementation here
        GC.SuppressFinalize(this);
    }
}
