using ExpenseService.Application.ApplicationServices;
using ExpenseService.Domain.Shared.Services;

namespace ExpenseService.Application.Features.Expense.Queries;

public class GetExpenseByIdQueryResultDto
{
	public decimal Amount { get; private set; }
	public DateTime Date { get; private set; }
	public string Description { get; set; }
	public Guid CategoryId { get; set; }
	public string UserId { get; set; }
	public DateTime CreatedAt { get; set; }
	public string CreatedBy { get; set; }
	public DateTime? LastUpdatedAt { get; set; }
	public string LastUpdatedBy { get; set; }
	public bool IsDeleted { get; set; }
	public DateTime? DeletedAt { get; set; }
	public string DeletedBy { get; set; }
}


public class GetExpenseByIdQueryResult: QueryResult<GetExpenseByIdQueryResultDto, GetExpenseByIdQueryResult>
{
	public GetExpenseByIdQueryResult(GetExpenseByIdQueryResultDto data): base(data)
	{
	}

	public GetExpenseByIdQueryResult(Error error): base(error)
	{
	}
}


public class MappingProfile: Profile
{
	public MappingProfile()
	{
		CreateMap<Domain.Entities.Expense, GetExpenseByIdQueryResultDto>();
	}
}


/// <summary>
/// Represents a query to retrieve an expense by its ID.
/// </summary>
public class GetExpenseByIdQuery: IRequest<GetExpenseByIdQueryResult>
{
	public Guid Id { get; private set; }

	public GetExpenseByIdQuery(Guid id)
	{
		Id = id;
	}
}


public class GetExpenseByIdQueryHandler: BaseQueryHandler<GetExpenseByIdQuery, GetExpenseByIdQueryResult>
{
	readonly IExpenseRepository _expenseRepository;
	readonly  ApplicationExpenseService _expenseService;

	public GetExpenseByIdQueryHandler(IMapper mapper, IExpenseRepository expenseRepository, ApplicationExpenseService expenseService): base(mapper)
	{
		_expenseRepository = expenseRepository;
		_expenseService    = expenseService;
	}

	public override async Task<GetExpenseByIdQueryResult> Handle(GetExpenseByIdQuery request, CancellationToken cancellationToken)
	{
		try
		{
			var expense = await _expenseService.GetExpenseByIdOrThrowAsync(request.Id);
			var result  = _mapper.Map<GetExpenseByIdQueryResultDto>(expense);

			return GetExpenseByIdQueryResult.Succeeded(result);
		}
		catch (Exception e)
		{
			var error = new Error(e.Message);
			return GetExpenseByIdQueryResult.Failed(error);
		}
	}
}