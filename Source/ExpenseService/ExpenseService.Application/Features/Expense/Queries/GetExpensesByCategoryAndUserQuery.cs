namespace ExpenseService.Application.Features.Expense.Queries;

public class GetExpensesByCategoryAndUserQueryResultDto
{
	public Guid Id { get; private set; }
	public decimal Amount { get; private set; }
	public string Description { get; private set; }
	public DateTime Date { get; private set; }

	public Guid CategoryId { get; private set; }
	public string UserId { get; private set; }

	public DateTime CreatedAt { get; private set; }
	public string CreatedBy { get; private set; }
	public DateTime LastUpdatedAt { get; private set; }
	public string LastUpdatedBy { get; private set; }
	public bool IsDeleted { get; private set; }
	public DateTime DeletedAt { get; private set; }
	public string DeletedBy { get; private set; }
}


/// <summary>
/// Represents the result of the GetExpensesByCategoryAndUserQuery.
/// </summary>
public class GetExpensesByCategoryAndUserQueryResult: QueryResult<IEnumerable<GetExpensesByCategoryAndUserQueryResultDto>, GetExpensesByCategoryAndUserQueryResult>
{

	public GetExpensesByCategoryAndUserQueryResult(IEnumerable<GetExpensesByCategoryAndUserQueryResultDto> data): base(data)
	{
	}

	public GetExpensesByCategoryAndUserQueryResult(Error error): base(error)
	{
	}
}

public class GetExpensesByCategoryAndUserQueryMappingProfile: Profile
{
	public GetExpensesByCategoryAndUserQueryMappingProfile()
	{
		CreateMap<Domain.Entities.Expense, GetExpensesByCategoryAndUserQueryResultDto>();
	}
}




/// <summary>
/// Represents a query to retrieve expenses by category and user.
/// </summary>
public class GetExpensesByCategoryAndUserQuery: IRequest<GetExpensesByCategoryAndUserQueryResult>
{
	public Guid CategoryId { get; private set; }

	public string UserId { get; private set; }

	public GetExpensesByCategoryAndUserQuery(Guid categoryId, string userId)
	{
		CategoryId = categoryId;
		UserId     = userId;
	}
}


public class GetExpensesByCategoryAndUserQueryHandler: BaseQueryHandler<GetExpensesByCategoryAndUserQuery, GetExpensesByCategoryAndUserQueryResult>
{
	readonly IUserRepository _userRepository;
	readonly ICategoryRepository _categoryRepository;
	readonly IExpenseRepository _expenseRepository;

	public GetExpensesByCategoryAndUserQueryHandler(IMapper mapper, IUserRepository userRepository, ICategoryRepository categoryRepository, IExpenseRepository expenseRepository): base(mapper)
	{
		_userRepository     = userRepository;
		_categoryRepository = categoryRepository;
		_expenseRepository  = expenseRepository;
	}

	public override async Task<GetExpensesByCategoryAndUserQueryResult> Handle(GetExpensesByCategoryAndUserQuery request, CancellationToken cancellationToken)
	{
		try
		{
			var user        = await _userRepository.GetByIdOrThrowAsync(request.UserId, cancellationToken);
			var category    = await _categoryRepository.GetByIdOrThrowAsync(request.CategoryId, cancellationToken);

			var expenses    = await _expenseRepository.GetExpensesByUserAndCategoryAsync(request.UserId, request.CategoryId, cancellationToken);
			var expensesDto = _mapper.Map<IEnumerable<GetExpensesByCategoryAndUserQueryResultDto>>(expenses);

			var result = GetExpensesByCategoryAndUserQueryResult.Succeeded(expensesDto);

			return result;
		}
		catch (Exception ex)
		{
			var error = new Error(ex.Message);

			return GetExpensesByCategoryAndUserQueryResult.Failed(error);
		}

	}


}
