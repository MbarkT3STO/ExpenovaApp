namespace ExpenseService.Application.Expense.Queries;

public record GetExpensesQueryResultDTO
{
	public Guid Id { get; set; }
	public decimal Amount { get; set; }
	public string Description { get; set; }
	public DateTime Date { get; set; }

	public int CategoryId { get; set; }
	public string UserId { get; set; }
}

public class GetExpensesQueryResult : QueryResult<IEnumerable<GetExpensesQueryResultDTO>>
{
	public GetExpensesQueryResult(IEnumerable<GetExpensesQueryResultDTO> data) : base(data)
	{
	}
	
	public GetExpensesQueryResult(Error error) : base(error)
	{
	}
	
	// public static implicit operator GetExpensesQueryResult(SucceededQuery<IEnumerable<GetExpensesQueryResultDTO>> queryResult)
	// {
	// 	return new(queryResult.Value);
	// }
}

public record GetExpensesQuery : IRequest<GetExpensesQueryResult>
{
	
}

public class GetExpensesQueryHandler : IRequestHandler<GetExpensesQuery, IQueryResult>
{
	private readonly IExpenseRepository _expenseRepository;
	private readonly IMapper _mapper;

	public GetExpensesQueryHandler(IExpenseRepository expenseRepository, IMapper mapper)
	{
		_expenseRepository = expenseRepository;
		_mapper = mapper;
	}

	public async Task<IQueryResult> Handle(GetExpensesQuery request, CancellationToken cancellationToken)
	{
		var expenses = await _expenseRepository.GetAsync();
		var expensesDTO = _mapper.Map<IReadOnlyCollection<GetExpensesQueryResultDTO>>(expenses);
		
		var result = GetExpensesQueryResult.CreateSucceeded(expensesDTO);
		
		return result;
	}
}