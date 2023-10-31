using ExpenseService.Domain.Events;

namespace ExpenseService.Application.Category.Commands;

public class CreateCategoryCommandResultDTO
{
	public Guid Id { get; set; }
	public string Name { get; set; }
	public string Description { get; set; }
	public string UserId { get; set; }
}

public class MappingProfile: Profile
{
	public MappingProfile()
	{
		CreateMap<Domain.Entities.Category, CreateCategoryCommandResultDTO>();
	}
}

public class CreateCategoryCommandResult: CommandResult<CreateCategoryCommandResultDTO>
{
	public CreateCategoryCommandResult(CreateCategoryCommandResultDTO data): base(data)
	{
	}
	
	public CreateCategoryCommandResult(Error error): base(error)
	{
	}
}

public record CreateCategoryCommand: IRequest<CreateCategoryCommandResult>
{
	public string Name { get; init; }
	public string Description { get; init; }
	public string UserId { get; init; }
}

public class CreateCategoryCommandHandler: IRequestHandler<CreateCategoryCommand, CreateCategoryCommandResult>
{
	private readonly ICategoryRepository _categoryRepository;
	private readonly IMapper _mapper;
	private readonly IMediator _mediator;

	public CreateCategoryCommandHandler(ICategoryRepository categoryRepository, IMapper mapper, IMediator mediator)
	{
		_categoryRepository = categoryRepository;
		_mapper             = mapper;
		_mediator           = mediator;
	}

	public async Task<CreateCategoryCommandResult> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
	{
		try
		{
			var category = new Domain.Entities.Category(request.Name, request.Description, request.UserId);
			await _categoryRepository.AddAsync(category);
			
			var resultValue = _mapper.Map<CreateCategoryCommandResultDTO>(category);
			var result      = new CreateCategoryCommandResult(resultValue);
			
			return result;
		}
		catch (Exception e)
		{
			var result = new CreateCategoryCommandResult(Error.FromException(e));
			
			return result;
		}
	}
	
	
	/// <summary>
	/// Publishes a CategoryCreatedEvent using the mediator.
	/// </summary>
	/// <param name="category">The category entity to be used in the event data.</param>
	public async Task PublishCategoryCreatedEvent(Domain.Entities.Category category)
	{
		var eventDetails         = new DomainEventDetails(nameof(CategoryCreatedEvent), category.UserId);
		var eventData            = new CategoryCreatedEventData(category.Id, category.Name, category.Description, category.UserId);
		var categoryCreatedEvent = CategoryCreatedEvent.Create(eventDetails, eventData);
		
		
		await _mediator.Publish(categoryCreatedEvent);
	}
}
