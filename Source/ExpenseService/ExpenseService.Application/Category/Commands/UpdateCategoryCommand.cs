namespace ExpenseService.Application.Category.Commands;

public record UpdateCategoryCommandResultDTO
{
	public Guid Id { get; private set; }
	public string Name { get; private set; }
	public string Description { get; private set; }
	public string UserId { get; private set; }
}

public class UpdateCategoryCommandResult: CommandResult<UpdateCategoryCommandResultDTO>
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


public class UpdateCategoryCommandHandler: IRequestHandler<UpdateCategoryCommand, UpdateCategoryCommandResult>
{
	private readonly ICategoryRepository _categoryRepository;
	private readonly IMapper _mapper;
	
	public UpdateCategoryCommandHandler(ICategoryRepository categoryRepository, IMapper mapper)
	{
		_categoryRepository = categoryRepository;
		_mapper             = mapper;
	}
	
	public async Task<UpdateCategoryCommandResult> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
	{
		try
		{
			var category = await _categoryRepository.GetByIdAsync(request.Id);
			
			if (category == null)
			{
				var error = new Error($"Category with ID {request.Id} not found.");
			}

			category.UpdateName(request.NewName);
			category.UpdateDescription(request.NewDescription);
			
			await _categoryRepository.UpdateAsync(category);
			
			var resultDTO = _mapper.Map<UpdateCategoryCommandResultDTO>(category);
			var result    = new UpdateCategoryCommandResult(resultDTO);
			
			return result;
		}
		catch (Exception e)
		{
			var domainException = new DomainException(e.Message, e);
			var error = new Error(domainException.Message);
			
			return new UpdateCategoryCommandResult(error);
		}
	}
}