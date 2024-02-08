using ExpenseService.Application.ApplicationServices;

namespace ExpenseService.Application.Features.Expense.Queries;

public class GetExpensesByCategoryQueryResultDto
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


public class GetExpensesByCategoryQueryResult: QueryResult<IEnumerable<GetExpensesByCategoryQueryResultDto>, GetExpensesByCategoryQueryResult>
{
	public GetExpensesByCategoryQueryResult(IEnumerable<GetExpensesByCategoryQueryResultDto> data): base(data)
	{
	}

	public GetExpensesByCategoryQueryResult(Error error): base(error)
	{
	}
}


public class GetExpensesByCategoryQueryMappingProfile: Profile
{
	public GetExpensesByCategoryQueryMappingProfile()
	{
		CreateMap<Domain.Entities.Expense, GetExpensesByCategoryQueryResultDto>();
	}
}



public class GetExpensesByCategoryQuery: IRequest<GetExpensesByCategoryQueryResult>
{
	public Guid CategoryId { get; private set; }

	public GetExpensesByCategoryQuery(Guid categoryId)
	{
		CategoryId = categoryId;
	}
}


public class GetExpensesByCategoryQueryHandler: BaseQueryHandler<GetExpensesByCategoryQuery, GetExpensesByCategoryQueryResult>
{
	readonly ApplicationExpenseService _expenseService;

	public GetExpensesByCategoryQueryHandler(IMapper mapper, ApplicationExpenseService expenseService): base(mapper)
	{
		_expenseService = expenseService;
	}

	public override async Task<GetExpensesByCategoryQueryResult> Handle(GetExpensesByCategoryQuery request, CancellationToken cancellationToken)
	{
		try
		{
			var expenses   = await _expenseService.GetExpensesByCategory(request.CategoryId);
			var resultDTOs = _mapper.Map<IEnumerable<GetExpensesByCategoryQueryResultDto>>(expenses);
			var result     = GetExpensesByCategoryQueryResult.Succeeded(resultDTOs);

			return result;
		}
		catch (Exception ex)
		{
			var error = new Error(ex.Message);

			return GetExpensesByCategoryQueryResult.Failed(error);
		}
	}
}
