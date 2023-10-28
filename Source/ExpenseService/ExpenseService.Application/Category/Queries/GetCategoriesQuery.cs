namespace ExpenseService.Application.Category.Queries;

public record GetCategoriesQueryResultDTO
{
	public Guid Id { get; private set; }
	public string Name { get; private set; }
	public string Description { get; private set; }
	public string UserId { get; private set; }
}

public class GetCategoriesQueryResult: QueryResult<IEnumerable<GetCategoriesQueryResultDTO>>
{
	public GetCategoriesQueryResult(IEnumerable<GetCategoriesQueryResultDTO>? value): base(value)
	{
	}
	
	public GetCategoriesQueryResult(Error error): base(error)
	{
	}
}

public class GetCategoriesQueryResultMappingProfile: Profile
{
	public GetCategoriesQueryResultMappingProfile()
	{
		CreateMap<Domain.Entities.Category, GetCategoriesQueryResultDTO>();
	}
}




public record GetCategoriesQuery: IRequest<GetCategoriesQueryResult>
{
	
}

public class GetCategoriesQueryHandler: IRequestHandler<GetCategoriesQuery, GetCategoriesQueryResult>
{
	private readonly ICategoryRepository _categoryRepository;
	private readonly IMapper _mapper;

	public GetCategoriesQueryHandler(ICategoryRepository categoryRepository, IMapper mapper)
	{
		_categoryRepository = categoryRepository;
		_mapper             = mapper;
	}
	
	public async Task<GetCategoriesQueryResult> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
	{
		try
		{
			var categories    = await _categoryRepository.GetAsync();
			var categoriesDTO = _mapper.Map<IEnumerable<GetCategoriesQueryResultDTO>>(categories);
			
			var result = new GetCategoriesQueryResult(categoriesDTO);

			return result;
		}
		catch (Exception e)
		{
			return new GetCategoriesQueryResult(new Error(e.Message));
		}
	}
}