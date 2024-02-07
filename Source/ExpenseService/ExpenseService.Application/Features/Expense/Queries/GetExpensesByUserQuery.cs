using ExpenseService.Application.ApplicationServices;

namespace ExpenseService.Application.Features.Expense.Queries;

/// <summary>
/// Represents the data transfer object (DTO) for the result of the <see cref="GetExpensesByUserQuery"/>.
/// </summary>
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
/// Represents the result of a <see cref="GetExpensesByUserQuery"/>.
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


public class GetExpensesByUserQueryHandler: BaseQueryHandler<GetExpensesByUserQuery, GetExpensesByUserQueryResult>
{
	readonly ApplicationExpenseService _expenseService;
	readonly ApplicationUserService _userService;
	readonly IExpenseRepository _expenseRepository;


	public GetExpensesByUserQueryHandler(IMapper mapper, IExpenseRepository expenseRepository, ApplicationExpenseService expenseService, ApplicationUserService userService): base(mapper)
	{
		_expenseService    = expenseService;
		_userService       = userService;
		_expenseRepository = expenseRepository;
	}


	public override async Task<GetExpensesByUserQueryResult> Handle(GetExpensesByUserQuery request, CancellationToken cancellationToken)
	{
		try
		{
			var user        = await _userService.GetUserByIdOrThrowAsync(request.UserId);
			var expenses    = await _expenseRepository.GetExpensesByUserIdAsync(request.UserId, cancellationToken);
			var expensesDTO = _mapper.Map<List<GetExpensesByUserQueryResultDto>>(expenses);

			var result = GetExpensesByUserQueryResult.Succeeded(expensesDTO);

			return result;
		}
		catch (Exception ex)
		{
			var error = new Error(ex.Message);

			return GetExpensesByUserQueryResult.Failed(error);
		}
	}
}