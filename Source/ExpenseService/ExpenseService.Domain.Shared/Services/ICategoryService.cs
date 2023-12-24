namespace ExpenseService.Domain.Shared.Services;

/// <summary>
/// Represents a service for managing categories.
/// </summary>
public interface ICategoryService
{
	/// <summary>
	/// Checks if the specified category name is unique for the given user, excluding the category with the specified ID.
	/// </summary>
	/// <param name="id">The ID of the category.</param>
	/// <param name="name">The name of the category.</param>
	/// <param name="userId">The ID of the user.</param>
	/// <returns>A task representing the asynchronous operation. The task result contains a boolean value indicating whether the category name is unique.</returns>
	Task<bool> IsUniqueCategoryNameAsync(Guid id, string name, string userId);
}
