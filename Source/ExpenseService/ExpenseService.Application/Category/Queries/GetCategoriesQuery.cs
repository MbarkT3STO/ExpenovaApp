namespace ExpenseService.Application.Category.Queries;

public record GetCategoriesQueryResultDTO
{
	public Guid Id { get; private set; }
	public string Name { get; private set; }
	public string Description { get; private set; }
	public string UserId { get; private set; }
}

public class GetCategoriesQueryResult: QueryResult<IEnumerable<GetCategoriesQueryResultDTO>, GetCategoriesQueryResult>
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

public class GetCategoriesQueryHandler: BaseQueryHandler<GetCategoriesQuery, GetCategoriesQueryResult>
{
	private readonly ICategoryRepository _categoryRepository;

	public GetCategoriesQueryHandler(ICategoryRepository categoryRepository, IMapper mapper) : base(mapper)
	{
		_categoryRepository = categoryRepository;
	}

	public override async Task<GetCategoriesQueryResult> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
	{
		try
		{
			var categories    = await _categoryRepository.GetAsync();
			var categoriesDTO = _mapper.Map<IEnumerable<GetCategoriesQueryResultDTO>>(categories);

			var result = GetCategoriesQueryResult.Succeeded(categoriesDTO);

			return result;
		}
		catch (Exception e)
		{
			var error = new Error(e.Message);
			return GetCategoriesQueryResult.Failed(error);
		}
	}
}