namespace ExpenseService.Domain.Repositories;

public interface ICategoryRepository : IRepository<Category, Guid>
{
	/// <summary>
	/// Retrieves a collection of categories associated with the specified user ID.
	/// </summary>
	/// <param name="userId">The ID of the user whose categories should be retrieved.</param>
	/// <returns>A collection of categories associated with the specified user ID.</returns>
	Task<IQueryable<Category>> GetCategoriesByUserIdAsync(int userId);
}
