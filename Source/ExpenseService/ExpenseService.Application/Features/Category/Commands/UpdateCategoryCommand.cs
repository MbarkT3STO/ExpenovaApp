using ExpenseService.Application.ApplicationServices;
using ExpenseService.Application.Features.Category.Commands.Shared;
using ExpenseService.Application.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseService.Application.Features.Category.Commands;

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


public class UpdateCategoryCommandHandler: CategoryCommandHandler<UpdateCategoryCommand, UpdateCategoryCommandResult, UpdateCategoryCommandResultDTO>
{
	readonly ApplicationCategoryService _categoryService;
	readonly IsValidCategoryForUpdateSpecification _isValidCategoryForUpdateSpecification;


	public UpdateCategoryCommandHandler(IMapper mapper, IMediator mediator, ApplicationCategoryService categoryService, ApplicationUserService userService, ICategoryRepository categoryRepository, IsValidCategoryForUpdateSpecification isValidCategoryForUpdateSpecification): base(categoryRepository, categoryService, userService, mapper, mediator)
	{
		_categoryService                       = categoryService;
		_isValidCategoryForUpdateSpecification = isValidCategoryForUpdateSpecification;
	}

	public override async Task<UpdateCategoryCommandResult> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
	{
		var category = await GetCategoryIfExistOrThrowException(request.Id);

		await CheckIfUserExistsOrThrowException(category.UserId);
		await UpdateAndAuditCategoryAsync(category, request);
		await PublishCategoryUpdatedEvent(category);

		var resultDTO = _mapper.Map<UpdateCategoryCommandResultDTO>(category);
		var result    = UpdateCategoryCommandResult.Succeeded(resultDTO);

		return result;
	}


	/// <summary>
	/// Updates the specified category and performs an audit by writing an updated audit entry.
	/// </summary>
	/// <param name="category">The category to be updated.</param>
	/// <param name="request">The update category command.</param>
	private async Task UpdateAndAuditCategoryAsync(Domain.Entities.Category category, UpdateCategoryCommand request)
	{
		category.UpdateName(request.NewName);
		category.UpdateDescription(request.NewDescription);

		category.WriteUpdatedAudit(updatedBy: category.UserId, updatedAt: DateTime.UtcNow);

		category.Validate(_isValidCategoryForUpdateSpecification);

		await _categoryRepository.UpdateAsync(category);
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