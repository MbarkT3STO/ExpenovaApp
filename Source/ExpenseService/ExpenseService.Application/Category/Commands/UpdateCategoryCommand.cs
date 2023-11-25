using ExpenseService.Application.Extensions;

namespace ExpenseService.Application.Category.Commands;

public record UpdateCategoryCommandResultDTO
{
	public Guid Id { get; private set; }
	public string Name { get; private set; }
	public string Description { get; private set; }
	public string UserId { get; private set; }
}

public class UpdateCategoryCommandResult: CommandResult<UpdateCategoryCommandResultDTO, UpdateCategoryCommandResult>
{
	public UpdateCategoryCommandResult(UpdateCategoryCommandResultDTO? value): base(value)
	{
	}
	
	public UpdateCategoryCommandResult(Error error): base(error)
	{
	}
}

public class UpdateCategoryCommandResultMappingProfile: Profile
{
	public UpdateCategoryCommandResultMappingProfile()
	{
		CreateMap<Domain.Entities.Category, UpdateCategoryCommandResultDTO>();
	}
}




public class UpdateCategoryCommand: IRequest<UpdateCategoryCommandResult>
{
	public Guid Id { get; private set; }
	public string NewName { get; set; }
	public string NewDescription { get; set; }

	public UpdateCategoryCommand(Guid id, string newName, string newDescription)
	{
		Id             = id;
		NewName        = newName;
		NewDescription = newDescription;
	}
}


public class UpdateCategoryCommandHandler: BaseCommandHandler<UpdateCategoryCommand, UpdateCategoryCommandResult, UpdateCategoryCommandResultDTO>
{
	private readonly ICategoryRepository _categoryRepository;
	
	public UpdateCategoryCommandHandler(ICategoryRepository categoryRepository, IMapper mapper, IMediator mediator) : base(mediator, mapper)
	{
		_categoryRepository = categoryRepository;
	}
	
	public override async Task<UpdateCategoryCommandResult> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
	{
		try
		{
			var category = await _categoryRepository.GetByIdAsync(request.Id);
			
			if (category == null)
				return UpdateCategoryCommandResult.Failed($"Category with ID {request.Id} not found.");
			

			category.UpdateName(request.NewName);
			category.UpdateDescription(request.NewDescription);
			
			category.WriteUpdatedAudit(updatedBy: category.UserId, updatedAt: DateTime.UtcNow);
			
			await _categoryRepository.UpdateAsync(category);
			
			var resultDTO = _mapper.Map<UpdateCategoryCommandResultDTO>(category);
			var result    = UpdateCategoryCommandResult.Succeeded(resultDTO);
			
			await PublishCategoryUpdatedEvent(category);
			
			return result;
		}
		catch (Exception e)
		{
			return UpdateCategoryCommandResult.Failed(e.Message);
		}
	}
	
	
	
	/// <summary>
	/// Publishes a CategoryUpdatedEvent for the given category.
	/// </summary>
	/// <param name="category">The category to publish the event for.</param>
	/// <returns>A task that represents the asynchronous operation.</returns>
	private async Task PublishCategoryUpdatedEvent(Domain.Entities.Category category)
	{
		var categoryUpdatedEventDetails = new DomainEventDetails(nameof(CategoryUpdatedEvent), category.UserId);
		var categoryUpdatedEventData    = new CategoryUpdatedEventData(category.Id, category.Name, category.Description, category.UserId);
		var categoryUpdatedEvent        = CategoryUpdatedEvent.Create(categoryUpdatedEventDetails, categoryUpdatedEventData);
		
		await _mediator.Publish(categoryUpdatedEvent);
	}
}