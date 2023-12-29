using ExpenseService.Application.Extensions;
using ExpenseService.Domain.Entities;

namespace ExpenseService.Application.ApplicationServices;

/// <summary>
/// Represents a service for managing user-related operations.
/// </summary>
public class UserService
{
	readonly IUserRepository _userRepository;

	public UserService(IUserRepository userRepository)
	{
		_userRepository = userRepository;
	}

	/// <summary>
	/// Retrieves a user by their ID asynchronously.
	/// </summary>
	/// <param name="id">The ID of the user.</param>
	/// <returns>The user with the specified ID.</returns>
	public async Task<User> GetUserByIdAsync(string id)
	{
		return await _userRepository.GetByIdAsync(id);
	}

	/// <summary>
	/// Checks if a user with the specified ID exists asynchronously.
	/// </summary>
	/// <param name="id">The ID of the user.</param>
	/// <returns>True if the user exists, false otherwise.</returns>
	public async Task<bool> IsUserExistsAsync(string id)
	{
		return await _userRepository.IsExistAsync(id);
	}

	/// <summary>
	/// Retrieves a user by ID and throws an exception if the user does not exist.
	/// </summary>
	/// <param name="id">The ID of the user to retrieve.</param>
	/// <returns>The user with the specified ID.</returns>
	public async Task<User> GetUserOrThrowExceptionIfNotExistsAsync(string id)
	{
		var user = await GetUserByIdAsync(id);

		if (user is null)
			throw new NotFoundException($"User with the ID {id} does not exist");

		return user;
	}
}
