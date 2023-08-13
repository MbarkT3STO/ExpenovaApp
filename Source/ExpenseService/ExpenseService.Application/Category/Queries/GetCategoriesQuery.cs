namespace ExpenseService.Application.Category.Queries;

public record GetCategoriesQueryResultDTO
{
	public string Name { get; private set; }
	public string Description { get; private set; }
}

public class GetCategoriesQueryResult : QueryResult<IEnumerable<GetCategoriesQueryResultDTO>>
{
	public GetCategoriesQueryResult(IEnumerable<GetCategoriesQueryResultDTO>? value) : base(value)
	{
	}
	
	public GetCategoriesQueryResult(Error error) : base(error)
	{
	}
}

public record GetCategoriesQuery : IRequest<GetCategoriesQueryResult>
{
	
}

public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, IQueryResult>
{
	private readonly ICategoryRepository _categoryRepository;
	private readonly IMapper _mapper;

	public GetCategoriesQueryHandler(ICategoryRepository categoryRepository, IMapper mapper)
	{
		_categoryRepository = categoryRepository;
		_mapper = mapper;
	}
	
	public async Task<IQueryResult> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
	{
		try
		{
			var categories = await _categoryRepository.GetAsync();
			var categoriesDTO = _mapper.Map<IEnumerable<GetCategoriesQueryResultDTO>>(categories);
			
			var result = GetCategoriesQueryResult.CreateSucceeded(categoriesDTO);
			
			return result;
		}
		catch (Exception e)
		{
			return GetCategoriesQueryResult.CreateFailed(Error.FromException(e));
		}
	}
}