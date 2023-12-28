using ExpenseService.Application.ApplicationServices;
using ExpenseService.Domain.Entities;

namespace ExpenseService.Application.Features.Category.Commands.Shared;
/// <summary>
/// Base class for category command handlers.
/// </summary>
/// <typeparam name="TCommand">The type of the command.</typeparam>
/// <typeparam name="TResult">The type of the command result.</typeparam>
/// <typeparam name="TResultDTO">The type of the command result DTO.</typeparam>
public abstract class CategoryCommandHandler<TCommand, TResult, TResultDTO> : BaseCommandHandler<TCommand, TResult, TResultDTO> where TCommand : IRequest<TResult> where TResult : ICommandResult<TResultDTO>
{
	protected readonly ICategoryRepository _categoryRepository;
	readonly ApplicationCategoryService _categoryService;
	protected readonly UserService _userService;


	protected CategoryCommandHandler(ICategoryRepository categoryRepository, ApplicationCategoryService categoryService, UserService userService, IMapper mapper, IMediator mediator): base(mediator, mapper)
	{
		_categoryRepository = categoryRepository;
		_categoryService    = categoryService;
		_userService        = userService;
	}



	/// <summary>
	/// Retrieves a category by its ID.
	/// If the category does not exist, an exception is thrown.
	/// </summary>
	/// <param name="categoryId">The ID of the category to retrieve.</param>
	/// <returns>The category entity.</returns>
	/// <exception cref="Exception">Thrown if the category does not exist.</exception>
	protected async Task<Domain.Entities.Category> GetCategoryIfExistOrThrowException(Guid categoryId)
	{
		var category = await _categoryRepository.GetByIdAsync(categoryId);

		if (category == null)
		{
			throw new Exception($"Category with ID {categoryId} not found.");
		}

		return category;
	}



	/// <summary>
	/// Checks if a user exists or throws an exception.
	/// </summary>
	/// <param name="userId">The ID of the user to check.</param>
	/// <returns>A task representing the asynchronous operation.</returns>
	/// <exception cref="Exception">Thrown if the user does not exist.</exception>
	protected async Task CheckIfUserExistsOrThrowException(string userId)
	{
		var userExists = await _userService.IsUserExistsAsync(userId);

		if (!userExists)
		{
			throw new Exception($"User with ID {userId} not found.");
		}
	}


	/// <summary>
	/// Checks if a category exists based on the provided category ID.
	/// </summary>
	/// <param name="categoryId">The ID of the category to check.</param>
	/// <returns>A task representing the asynchronous operation.</returns>
	/// <exception cref="Exception">Thrown if the category does not exist.</exception>
	protected async Task CheckIfCategoryExists(Guid categoryId)
	{
		var isExist = await _categoryService.IsExistAsync(categoryId);
		if (!isExist)
		{
			throw new Exception($"Category with ID {categoryId} not found.");
		}
	}
}