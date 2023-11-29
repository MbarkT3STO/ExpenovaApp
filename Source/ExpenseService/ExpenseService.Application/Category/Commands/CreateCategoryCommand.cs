using ExpenseService.Application.Extensions;
using ExpenseService.Domain.Events;
using ExpenseService.Domain.Specifications.CategorySpecifications;

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

public class CreateCategoryCommandResult: CommandResult<CreateCategoryCommandResultDTO, CreateCategoryCommandResult>
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

public class CreateCategoryCommandHandler: BaseCommandHandler<CreateCategoryCommand, CreateCategoryCommandResult, CreateCategoryCommandResultDTO>
{
	private readonly ICategoryRepository _categoryRepository;
	
	public CreateCategoryCommandHandler(ICategoryRepository categoryRepository, IMapper mapper, IMediator mediator) : base(mediator, mapper)
	{
		_categoryRepository = categoryRepository;
	}

	public override async Task<CreateCategoryCommandResult> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
	{
		try
		{
			var category = new Domain.Entities.Category(request.Name, request.Description, request.UserId);
			
			category.WriteCreatedAudit(createdBy: request.UserId);
			
			var isValidCategoryForCreateSpecification = new IsValidCategoryForCreateSpecification();
			var isValidCategoryForCreate              = isValidCategoryForCreateSpecification.IsSatisfiedBy(category);
			
			// if (!isValidCategoryForCreate)
			// {
			// 	var errors = isValidCategoryForCreateSpecification.GetErrors();
			// 	var result = CreateCategoryCommandResult.Failed(errors);
				
			// 	return result;
			// }
			
			await _categoryRepository.AddAsync(category);
			
			var resultValue = _mapper.Map<CreateCategoryCommandResultDTO>(category);
			var result      = CreateCategoryCommandResult.Succeeded(resultValue);
			
			await PublishCategoryCreatedEvent(category);
			
			return result;
		}
		catch (Exception e)
		{
			var result = CreateCategoryCommandResult.Failed(e.Message);
			
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
