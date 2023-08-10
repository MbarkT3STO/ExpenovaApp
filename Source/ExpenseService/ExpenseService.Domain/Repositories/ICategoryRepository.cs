namespace ExpenseService.Domain.Repositories;

public interface ICategoryRepository : IRepository<Category, int>
{
	Task<IQueryable<Category>> GetCategoriesByUserIdAsync(int userId);
}
