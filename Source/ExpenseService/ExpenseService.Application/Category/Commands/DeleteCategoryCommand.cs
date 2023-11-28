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


public class DeleteCategoryCommandHandler: BaseCommandHandler<DeleteCategoryCommand, DeleteCategoryCommandResult, DeleteCategoryCommandResultDTO>
{
	private readonly ICategoryRepository _categoryRepository;

	public DeleteCategoryCommandHandler(ICategoryRepository categoryRepository, IMapper mapper, IMediator mediator): base(mediator, mapper)
	{
		_categoryRepository = categoryRepository;
	}

	public override async Task<DeleteCategoryCommandResult> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
	{
		try
		{
			var category = await _categoryRepository.GetByIdAsync(request.Id);

			if (category == null)
			{
				return DeleteCategoryCommandResult.Failed($"Category with ID {request.Id} not found.");
			}

			category.WriteDeletedAudit(deletedBy: category.UserId, deletedAt: DateTime.UtcNow);

			await _categoryRepository.DeleteAsync(category, cancellationToken);

			var resultDTO = _mapper.Map<DeleteCategoryCommandResultDTO>(category);
			
			await PublishCategoryDeletedEvent(category);

			return DeleteCategoryCommandResult.Succeeded(resultDTO);
		}
		catch (Exception e)
		{
			var error = new Error(e.Message);
			return DeleteCategoryCommandResult.Failed(error);
		}

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