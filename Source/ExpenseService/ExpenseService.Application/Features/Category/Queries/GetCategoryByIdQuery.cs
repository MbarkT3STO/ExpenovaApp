namespace ExpenseService.Application.Features.Category.Queries;

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
public class GetCategoryByIdQueryResult: QueryResult<GetCategoryByIdQueryResultDTO, GetCategoryByIdQueryResult>
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
/// Validator for the GetCategoryByIdQuery class.
/// </summary>
public class GetCategoryByIdQueryValidator : AbstractValidator<GetCategoryByIdQuery>
{
	public GetCategoryByIdQueryValidator()
	{
		RuleFor(x => x.Id).Must(id => id != Guid.Empty).WithMessage("Category ID cannot be empty.");
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


public class GetCategoryByIdQueryHandler: BaseQueryHandler<GetCategoryByIdQuery, GetCategoryByIdQueryResult>
{
	private readonly ICategoryRepository _categoryRepository;

	public GetCategoryByIdQueryHandler(ICategoryRepository categoryRepository, IMapper mapper) : base(mapper)
	{
		_categoryRepository = categoryRepository;
	}

	public override async Task<GetCategoryByIdQueryResult> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
	{
		try
		{
			var category    = await _categoryRepository.GetByIdAsync(request.Id);
			var categoryDTO = _mapper.Map<GetCategoryByIdQueryResultDTO>(category);

			var result = GetCategoryByIdQueryResult.Succeeded(categoryDTO);

			return result;
		}
		catch (Exception e)
		{
			var error = new Error(e.Message);

			return GetCategoryByIdQueryResult.Failed(error);
		}
	}
}