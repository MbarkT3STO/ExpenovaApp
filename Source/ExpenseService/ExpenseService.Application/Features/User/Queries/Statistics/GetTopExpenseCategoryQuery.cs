namespace ExpenseService.Application.Features.User.Queries.Statistics;

public class GetTopExpenseCategoryQueryResultDTO
{
	public string? Category { get; set; }
	public decimal? Sum { get; set; }
}


/// <summary>
/// Represents the result of the GetTopExpenseCategoryQuery.
/// </summary>
public class GetTopExpenseCategoryQueryResult: QueryResult<GetTopExpenseCategoryQueryResultDTO, GetTopExpenseCategoryQueryResult>
{
	public GetTopExpenseCategoryQueryResult(GetTopExpenseCategoryQueryResultDTO value): base(value)
	{
	}

	public GetTopExpenseCategoryQueryResult(Error error): base(error)
	{
	}
}

public class GetTopExpenseCategoryQueryMapper: Profile
{
	public GetTopExpenseCategoryQueryMapper()
	{
		CreateMap<(string? Category, decimal? Sum), GetTopExpenseCategoryQueryResultDTO>()
																	   .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
																	   .ForMember(dest => dest.Sum, opt => opt.MapFrom(src => src.Sum));
	}
}



/// <summary>
/// Represents a query to get the top expense category for a user.
/// </summary>
public class GetTopExpenseCategoryQuery: IRequest<GetTopExpenseCategoryQueryResult>
{
	public string UserId { get; private set; }

	public GetTopExpenseCategoryQuery(string userId)
	{
		UserId = userId;
	}
}


public class GetTopExpenseCategoryQueryHandler: BaseQueryHandler<GetTopExpenseCategoryQuery, GetTopExpenseCategoryQueryResult>
{
	readonly IUserRepository _userRepository;
	readonly IExpenseRepository _expenseRepository;

	public GetTopExpenseCategoryQueryHandler(IMapper mapper, IUserRepository userRepository, IExpenseRepository expenseRepository): base(mapper)
	{
		_userRepository    = userRepository;
		_expenseRepository = expenseRepository;
	}

	public override async Task<GetTopExpenseCategoryQueryResult> Handle(GetTopExpenseCategoryQuery request, CancellationToken cancellationToken)
	{
		try
		{
			var user        = await _userRepository.GetByIdOrThrowAsync(request.UserId, cancellationToken);
			var topCategory = await _expenseRepository.GetTopCategoryAsync(request.UserId, cancellationToken);

			var resultDTO   = _mapper.Map<GetTopExpenseCategoryQueryResultDTO>(topCategory);
			var result      = GetTopExpenseCategoryQueryResult.Succeeded(resultDTO);

			return result;
		}
		catch (Exception ex)
		{
			return GetTopExpenseCategoryQueryResult.Failed(ex);
		}
	}
}