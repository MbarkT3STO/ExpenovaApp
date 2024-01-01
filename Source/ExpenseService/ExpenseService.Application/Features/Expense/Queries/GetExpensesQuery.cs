using ExpenseService.Infrastructure.Data.Entities;

namespace ExpenseService.Application.Expense.Queries;

public record GetExpensesQueryResultDTO
{
	public Guid Id { get; set; }
	public decimal Amount { get; set; }
	public string Description { get; set; }
	public DateTime Date { get; set; }
}


public class MappingProfile: Profile
{
	public MappingProfile()
	{
		CreateMap<Domain.Entities.Expense, GetExpensesQueryResultDTO>();
	}
}


public class GetExpensesQueryResult: QueryResult<IEnumerable<GetExpensesQueryResultDTO>, GetExpensesQueryResult>
{
	public GetExpensesQueryResult(IEnumerable<GetExpensesQueryResultDTO> data): base(data)
	{
	}

	public GetExpensesQueryResult(Error error): base(error)
	{
	}
}

public record GetExpensesQuery: IRequest<GetExpensesQueryResult>
{

}

public class GetExpensesQueryHandler: IRequestHandler<GetExpensesQuery, GetExpensesQueryResult>
{
	private readonly IExpenseRepository _expenseRepository;
	private readonly IMapper _mapper;

	public GetExpensesQueryHandler(IExpenseRepository expenseRepository, IMapper mapper)
	{
		_expenseRepository = expenseRepository;
		_mapper            = mapper;
	}

	public async Task<GetExpensesQueryResult> Handle(GetExpensesQuery request, CancellationToken cancellationToken)
	{
		try
		{
			var expenses = await _expenseRepository.GetAsync(cancellationToken);
			var expensesDTO = _mapper.Map<List<GetExpensesQueryResultDTO>>(expenses);

			var result = GetExpensesQueryResult.Succeeded(expensesDTO);

			return result;
		}
		catch(Exception ex)
		{
			var error = new Error(ex.Message);

			return GetExpensesQueryResult.Failed(error);
		}
	}
}