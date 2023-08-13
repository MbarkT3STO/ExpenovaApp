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

public class CreateCategoryCommandHandler: IRequestHandler<CreateCategoryCommand, ICommandResult<CreateCategoryCommandResultDTO>>
{
	private readonly ICategoryRepository _categoryRepository;
	private readonly IMapper _mapper;

	public CreateCategoryCommandHandler(ICategoryRepository categoryRepository, IMapper mapper)
	{
		_categoryRepository = categoryRepository;
		_mapper             = mapper;
	}

	public async Task<ICommandResult<CreateCategoryCommandResultDTO>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
	{
		try
		{
			var category = new Domain.Entities.Category(request.Name, request.Description, request.UserId);
			await _categoryRepository.AddAsync(category);
			
			var resultValue = _mapper.Map<CreateCategoryCommandResultDTO>(category);
			var result      = CreateCategoryCommandResult.CreateSucceeded(resultValue);
			
			return result;
		}
		catch (Exception e)
		{
			var result = CreateCategoryCommandResult.CreateFailed(Error.FromException(e));
			
			return result;
		}
	}
}
