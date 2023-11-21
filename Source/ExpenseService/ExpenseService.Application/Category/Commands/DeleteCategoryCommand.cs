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
public class DeleteCategoryCommand : IRequest<DeleteCategoryCommandResult>
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


public class DeleteCategoryCommandHandler: IRequestHandler<DeleteCategoryCommand, DeleteCategoryCommandResult>
{
	private readonly ICategoryRepository _categoryRepository;
	private readonly IMapper _mapper;
	private readonly IMediator _mediator;
	
	public DeleteCategoryCommandHandler(ICategoryRepository categoryRepository, IMapper mapper, IMediator mediator)
	{
		_categoryRepository = categoryRepository;
		_mapper             = mapper;
		_mediator           = mediator;
	}
	
	public async Task<DeleteCategoryCommandResult> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
	{
		try
		{
			var category = await _categoryRepository.GetByIdAsync(request.Id);
			
			if (category == null)
			{
				var error = new Error($"Category with ID {request.Id} not found.");
				return new DeleteCategoryCommandResult(error);
			}
			
			await _categoryRepository.DeleteAsync(category, cancellationToken);
			
			var categoryDTO = _mapper.Map<DeleteCategoryCommandResultDTO>(category);
			
			return new DeleteCategoryCommandResult(categoryDTO);
		}
		catch (Exception e)
		{
			var error = new Error(e.Message);
			return new DeleteCategoryCommandResult(error);
		}
		
	}
}