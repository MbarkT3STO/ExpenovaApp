using ExpenseService.Application.ApplicationServices;
using ExpenseService.Application.Category.Commands.Shared;
using ExpenseService.Application.Extensions;

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

public class CreateCategoryCommandHandler: CategoryCommandHandler<CreateCategoryCommand, CreateCategoryCommandResult, CreateCategoryCommandResultDTO>
{
	public CreateCategoryCommandHandler(ICategoryRepository categoryRepository, CategoryService categoryService, UserService userService, IMapper mapper, IMediator mediator): base(categoryRepository, categoryService, userService, mapper, mediator)
	{
	}

	public override async Task<CreateCategoryCommandResult> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
	{
		try
		{
			Log.Information("CreateCategoryCommandHandler.Handle - Start Creating a new category with name: {Name}, description: {Description}, and user id: {UserId}", request.Name, request.Description, request.UserId);

			await CheckIfUserExistsOrThrowException(request.UserId);

			var category                              = CreateAndAuditCategory(request);
			var isValidCategoryForCreateSpecification = new IsValidCategoryForCreateSpecification();

			category.Validate(isValidCategoryForCreateSpecification);


			await _categoryRepository.AddAsync(category);
			await PublishCategoryCreatedEvent(category);


			var resultValue = _mapper.Map<CreateCategoryCommandResultDTO>(category);
			var result      = CreateCategoryCommandResult.Succeeded(resultValue);

			Log.Information("CreateCategoryCommandHandler.Handle - End Creating a new category with name: {Name}, description: {Description}, and user id: {UserId}", request.Name, request.Description, request.UserId);

			return result;
		}
		catch (Exception e)
		{
			var result = CreateCategoryCommandResult.Failed(e.Message);

			Log.Error(e, "CreateCategoryCommandHandler.Handle - Failed to create a new category with name: {Name}, description: {Description}, and user id: {UserId}", request.Name, request.Description, request.UserId);

			return result;
		}
	}



	/// <summary>
	/// Creates a new category based on the provided command and performs an audit trail for the creation.
	/// </summary>
	/// <param name="request">The command containing the category details.</param>
	/// <returns>The newly created category.</returns>
	private static Domain.Entities.Category CreateAndAuditCategory(CreateCategoryCommand request)
	{
		var category = new Domain.Entities.Category(request.Name, request.Description, request.UserId);
		category.WriteCreatedAudit(createdBy: request.UserId);

		return category;
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
