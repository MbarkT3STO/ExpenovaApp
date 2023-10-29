namespace ExpenseService.Domain.Repositories;

public interface IUserRepository : IRepository<User, string>
{
	/// <summary>
	/// Checks if a user with the specified ID exists in the repository.
	/// </summary>
	/// <param name="id">The ID of the user to check for.</param>
	/// <returns>True if a user with the specified ID exists in the repository, false otherwise.</returns>
	bool IsExist(string id);

	/// <summary>
	/// Checks if a user with the specified ID exists in the repository.
	/// </summary>
	/// <param name="id">The ID of the user to check.</param>
	/// <returns>A boolean indicating whether the user exists in the repository.</returns>
	Task<bool> IsExistAsync(string id);

	/// <summary>
	/// Checks if a user with the given ID exists in the repository.
	/// </summary>
	/// <param name="id">The ID of the user to check.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>A boolean indicating whether the user exists or not.</returns>
	Task<bool> IsExistAsync(string id, CancellationToken cancellationToken);
	
}
