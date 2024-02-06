namespace ExpenseService.Application.Features.Expense.Queries;

public class GetExpensesByUserQueryResultDto
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
/// Represents the result of a GetExpensesByUserQuery.
/// </summary>
public class GetExpensesByUserQueryResult: QueryResult<IEnumerable<GetExpensesByUserQueryResultDto>, GetExpensesByUserQueryResult>
{
	public GetExpensesByUserQueryResult(IEnumerable<GetExpensesByUserQueryResultDto> data): base(data)
	{
	}

	public GetExpensesByUserQueryResult(Error error): base(error)
	{
	}
}


public class GetExpensesByUserQueryMappingProfile: Profile
{
	public GetExpensesByUserQueryMappingProfile()
	{
		CreateMap<Domain.Entities.Expense, GetExpensesByUserQueryResultDto>();
	}
}


/// <summary>
/// Represents a query to retrieve expenses by user.
/// </summary>
public class GetExpensesByUserQuery: IRequest<GetExpensesByUserQueryResult>
{
	public string UserId { get; private set; }

	public GetExpensesByUserQuery(string userId)
	{
		UserId = userId;
	}
}


public class GetExpensesByUserQueryHandler: IRequestHandler<GetExpensesByUserQuery, GetExpensesByUserQueryResult>
{
	private readonly IExpenseRepository _expenseRepository;
	private readonly IMapper _mapper;

	public GetExpensesByUserQueryHandler(IExpenseRepository expenseRepository, IMapper mapper)
	{
		_expenseRepository = expenseRepository;
		_mapper            = mapper;
	}

	public async Task<GetExpensesByUserQueryResult> Handle(GetExpensesByUserQuery request, CancellationToken cancellationToken)
	{
		try
		{
			var expenses    = await _expenseRepository.GetExpensesByUserIdAsync(request.UserId, cancellationToken);
			var expensesDTO = _mapper.Map<List<GetExpensesByUserQueryResultDto>>(expenses);

			var result = GetExpensesByUserQueryResult.Succeeded(expensesDTO);

			return result;
		}
		catch(Exception ex)
		{
			var error = new Error(ex.Message);

			return GetExpensesByUserQueryResult.Failed(error);
		}
	}
}