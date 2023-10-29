namespace ExpenseService.Application.Category.Queries;

/// <summary>
/// Represents the result DTO for the GetCategoriesByUserIdQuery.
/// </summary>
public record GetCategoriesByUserIdQueryResultDTO
{
	public Guid Id { get; private set; }
	public string Name { get; private set; }
	public string Description { get; private set; }
	public string UserId { get; private set; }
}

/// <summary>
/// Represents the result of a query to retrieve categories by user ID.
/// </summary>
public class GetCategoriesByUserIdQueryResult: QueryResult<IEnumerable<GetCategoriesByUserIdQueryResultDTO>>
{
	public GetCategoriesByUserIdQueryResult(IEnumerable<GetCategoriesByUserIdQueryResultDTO>? value): base(value)
	{
	}
	
	public GetCategoriesByUserIdQueryResult(Error error): base(error)
	{
	}
}

/// <summary>
/// AutoMapper profile for mapping Category entity to GetCategoriesByUserIdQueryResultDTO.
/// </summary>
public class GetCategoriesByUserIdQueryResultMappingProfile: Profile
{
	public GetCategoriesByUserIdQueryResultMappingProfile()
	{
		CreateMap<Domain.Entities.Category, GetCategoriesByUserIdQueryResultDTO>();
	}
}




/// <summary>
/// Represents a query to retrieve categories by user ID.
/// </summary>
public class GetCategoriesByUserIdQuery: IRequest<GetCategoriesByUserIdQueryResult>
{
	public string UserId { get; private set; }

	public GetCategoriesByUserIdQuery(string userId)
	{
		UserId = userId;
	}
}


public class GetCategoriesByUserIdQueryHandler: IRequestHandler<GetCategoriesByUserIdQuery, GetCategoriesByUserIdQueryResult>
{
	private readonly ICategoryRepository _categoryRepository;
	private readonly IUserRepository _userRepository;
	private readonly IMapper _mapper;
	
	public GetCategoriesByUserIdQueryHandler(ICategoryRepository categoryRepository, IUserRepository userRepository, IMapper mapper)
	{
		_categoryRepository = categoryRepository;
		_userRepository     = userRepository;
		_mapper             = mapper;
	}
	
	public async Task<GetCategoriesByUserIdQueryResult> Handle(GetCategoriesByUserIdQuery request, CancellationToken cancellationToken)
	{
		try
		{
			var isUserExists = await _userRepository.IsExistAsync(request.UserId, cancellationToken);
			
			if(!isUserExists)
			{
				var error = new Error($"User with ID {request.UserId} does not exist.");
				
				return new GetCategoriesByUserIdQueryResult(error);
			}
			
			
			var categories    = await _categoryRepository.GetCategoriesByUserIdAsync(request.UserId, cancellationToken);
			var categoriesDTO = _mapper.Map<IEnumerable<GetCategoriesByUserIdQueryResultDTO>>(categories);
			var queryResult   = new GetCategoriesByUserIdQueryResult(categoriesDTO);
			
			return queryResult;
		}
		catch(Exception e)
		{
			var domainException = new DomainException(e.Message, e);
			var error           = new Error(e.Message, domainException);
			
			return new GetCategoriesByUserIdQueryResult(error);
		}
	}
}