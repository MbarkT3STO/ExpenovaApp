using ExpenseService.Application.ApplicationServices;
using ExpenseService.Application.Category.Commands.Shared;
using ExpenseService.Application.Extensions;

namespace ExpenseService.Application.Category.Commands;

/// <summary>
/// Represents the result data transfer object for the delete category command.
/// </summary>
public class DeleteCategoryCommandResultDTO
{
	public Guid Id { get; private set; }
	public string Name { get; private set; }
	public string Description { get; private set; }
	public string UserId { get; private set; }
}


/// <summary>
/// Represents the result of a delete category command.
/// </summary>
public class DeleteCategoryCommandResult: CommandResult<DeleteCategoryCommandResultDTO, DeleteCategoryCommandResult>
{
	/// <summary>
	/// Initializes a new instance of the <see cref="DeleteCategoryCommandResult"/> class with a value.
	/// </summary>
	/// <param name="value">The value of the command result.</param>
	public DeleteCategoryCommandResult(DeleteCategoryCommandResultDTO? value): base(value)
	{
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="DeleteCategoryCommandResult"/> class with an error.
	/// </summary>
	/// <param name="error">The error of the command result.</param>
	public DeleteCategoryCommandResult(Error error): base(error)
	{
	}
}


/// <summary>
/// Represents a mapping profile for mapping the <see cref="Domain.Entities.Category"/> entity to the <see cref="DeleteCategoryCommandResultDTO"/> DTO.
/// </summary>
public class DeleteCategoryCommandResultMappingProfile: Profile
{
	public DeleteCategoryCommandResultMappingProfile()
	{
		CreateMap<Domain.Entities.Category, DeleteCategoryCommandResultDTO>();
	}
}




/// <summary>
/// Represents a command to delete a category.
/// </summary>
public class DeleteCategoryCommand: IRequest<DeleteCategoryCommandResult>
{
	/// <summary>
	/// Gets the ID of the category to be deleted.
	/// </summary>
	public Guid Id { get; private set; }

	/// <summary>
	/// Initializes a new instance of the <see cref="DeleteCategoryCommand"/> class.
	/// </summary>
	/// <param name="id">The ID of the category to be deleted.</param>
	public DeleteCategoryCommand(Guid id)
	{
		Id = id;
	}
}


public class DeleteCategoryCommandHandler: CategoryCommandHandler<DeleteCategoryCommand, DeleteCategoryCommandResult, DeleteCategoryCommandResultDTO>
{
	readonly IsValidCategoryForDeleteSpecification _isValidCategoryForDeleteSpecification;

	public DeleteCategoryCommandHandler(IMapper mapper, IMediator mediator, CategoryService categoryService, UserService userService, ICategoryRepository categoryRepository, IsValidCategoryForDeleteSpecification isValidCategoryForDeleteSpecification): base(categoryRepository, categoryService, userService, mapper, mediator)
	{
		_isValidCategoryForDeleteSpecification = isValidCategoryForDeleteSpecification;
	}

	public override async Task<DeleteCategoryCommandResult> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
	{
		var category = await GetCategoryIfExistOrThrowException(request.Id);

		await DeleteCategoryAsync(category);

		await PublishCategoryDeletedEvent(category);

		var resultDTO = _mapper.Map<DeleteCategoryCommandResultDTO>(category);

		return DeleteCategoryCommandResult.Succeeded(resultDTO);
	}


	/// <summary>
	/// Deletes the specified category.
	/// </summary>
	/// <param name="category">The category to delete.</param>
	/// <returns>A task representing the asynchronous operation.</returns>
	private async Task DeleteCategoryAsync(Domain.Entities.Category category)
	{
		category.WriteDeletedAudit(deletedBy: category.UserId, deletedAt: DateTime.UtcNow);

		category.Validate(_isValidCategoryForDeleteSpecification);

		await _categoryRepository.DeleteAsync(category);
	}


	/// <summary>
	/// Publishes a <see cref="CategoryDeletedEvent"/> for the given category.
	/// </summary>
	/// <param name="category">The category to publish the event for.</param>
	/// <returns>A task representing the asynchronous operation.</returns>
	private async Task PublishCategoryDeletedEvent(Domain.Entities.Category category)
	{
		var categoryDeletedEventDetails = new DomainEventDetails(nameof(CategoryDeletedEvent), category.UserId);
		var categoryDeletedEventData    = new CategoryDeletedEventData(category.Id, category.Name, category.Description, category.UserId);
		var categoryDeletedEvent        = CategoryDeletedEvent.Create(categoryDeletedEventDetails, categoryDeletedEventData);

		await _mediator.Publish(categoryDeletedEvent);
	}
}