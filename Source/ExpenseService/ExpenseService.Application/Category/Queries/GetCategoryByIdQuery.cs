using System.Reflection.Metadata;
namespace ExpenseService.Application.Category.Queries;

/// <summary>
/// Represents the data transfer object for the result of the GetCategoryByIdQuery.
/// </summary>
public record GetCategoryByIdQueryResultDTO
{
	public Guid Id { get; private set; }
	public string Name { get; private set; }
	public string Description { get; private set; }
	public string UserId { get; private set; }
}

/// <summary>
/// Represents the result of a query to retrieve a category by its ID.
/// </summary>
public class GetCategoryByIdQueryResult: QueryResult<GetCategoryByIdQueryResultDTO>
{
	public GetCategoryByIdQueryResult(GetCategoryByIdQueryResultDTO? value): base(value)
	{
	}
	
	public GetCategoryByIdQueryResult(Error error): base(error)
	{
	}
}

/// <summary>
/// AutoMapper profile for mapping Category entity to GetCategoryByIdQueryResultDTO.
/// </summary>
public class GetCategoryByIdQueryResultMappingProfile: Profile
{
	public GetCategoryByIdQueryResultMappingProfile()
	{
		CreateMap<Domain.Entities.Category, GetCategoryByIdQueryResultDTO>();
	}
}




/// <summary>
/// Represents a query to get a category by its ID.
/// </summary>
public class GetCategoryByIdQuery : IRequest<GetCategoryByIdQueryResult>
{
	public Guid Id { get; private set; }

	public GetCategoryByIdQuery(Guid id)
	{
		Id = id;
	}
}


public class GetCategoryByIdQueryHandler: IRequestHandler<GetCategoryByIdQuery, GetCategoryByIdQueryResult>
{
	private readonly ICategoryRepository _categoryRepository;
	private readonly IMapper _mapper;
	
	public GetCategoryByIdQueryHandler(ICategoryRepository categoryRepository, IMapper mapper)
	{
		_categoryRepository = categoryRepository;
		_mapper             = mapper;
	}
	
	public async Task<GetCategoryByIdQueryResult> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
	{
		try
		{
			var category    = await _categoryRepository.GetByIdAsync(request.Id);
			var categoryDTO = _mapper.Map<GetCategoryByIdQueryResultDTO>(category);
			
			var result = new GetCategoryByIdQueryResult(categoryDTO);

			return result;
		}
		catch (Exception e)
		{
			return new GetCategoryByIdQueryResult(new Error(e.Message));
		}
	}
}