
namespace ExpenseService.Application.Features.User.Queries.Statistics;

public class GetUserBasicStatisticsQueryResultDTO
{
	public int CategoriesCount { get; set; }
	public int ExpensesCount { get; set; }
	public decimal ExpensesSum { get; set; }
	public decimal ExpensesAverage { get; set; }
	public int SubscriptionExpensesCount { get; set; }
	public decimal SubscriptionExpensesSum { get; set; }
	public decimal SubscriptionExpensesAverage { get; set; }
}

/// <summary>
/// Represents the result of the <see cref="GetUserBasicStatisticsQuery"/>.
/// </summary>
public class GetUserBasicStatisticsQueryResult: QueryResult<GetUserBasicStatisticsQueryResultDTO, GetUserBasicStatisticsQueryResult>
{
	public GetUserBasicStatisticsQueryResult(GetUserBasicStatisticsQueryResultDTO value): base(value)
	{
	}

	public GetUserBasicStatisticsQueryResult(Error error): base(error)
	{
	}
}



/// <summary>
/// Represents a query to retrieve basic statistics for a user.
/// </summary>
public class GetUserBasicStatisticsQuery: IRequest<GetUserBasicStatisticsQueryResult>
{
	public string UserId { get; private set; }

	public GetUserBasicStatisticsQuery(string userId)
	{
		UserId = userId;
	}
}

public class GetUserBasicStatisticsQueryHandler: BaseQueryHandler<GetUserBasicStatisticsQuery, GetUserBasicStatisticsQueryResult>
{
	readonly ICategoryRepository _categoryRepository;
	readonly IExpenseRepository _expenseRepository;
	readonly ISubscriptionExpenseRepository _subscriptionExpenseRepository;

	public GetUserBasicStatisticsQueryHandler(IMapper mapper, ICategoryRepository categoryRepository, IExpenseRepository expenseRepository, ISubscriptionExpenseRepository subscriptionExpenseRepository): base(mapper)
	{
		_categoryRepository            = categoryRepository;
		_expenseRepository             = expenseRepository;
		_subscriptionExpenseRepository = subscriptionExpenseRepository;
	}


	public override async Task<GetUserBasicStatisticsQueryResult> Handle(GetUserBasicStatisticsQuery request, CancellationToken cancellationToken)
	{
		try
		{
			var categoriesCount             = await _categoryRepository.GetCountAsync(request.UserId, cancellationToken);
			var expensesCount               = await _expenseRepository.GetCountAsync(request.UserId, cancellationToken);
			var expensesSum                 = await _expenseRepository.GetSumAsync(request.UserId, cancellationToken);
			var expensesAverage             = await _expenseRepository.GetAverageAsync(request.UserId, cancellationToken);
			var subscriptionExpensesCount   = await _subscriptionExpenseRepository.GetCountAsync(request.UserId, cancellationToken);
			var subscriptionExpensesSum     = await _subscriptionExpenseRepository.GetSumAsync(request.UserId, cancellationToken);
			var subscriptionExpensesAverage = await _subscriptionExpenseRepository.GetAverageAsync(request.UserId, cancellationToken);


			var resultDTO = new GetUserBasicStatisticsQueryResultDTO
			{
				CategoriesCount             = categoriesCount,
				ExpensesCount               = expensesCount,
				ExpensesSum                 = expensesSum,
				ExpensesAverage             = expensesAverage,
				SubscriptionExpensesCount   = subscriptionExpensesCount,
				SubscriptionExpensesSum     = subscriptionExpensesSum,
				SubscriptionExpensesAverage = subscriptionExpensesAverage
			};

			var result = GetUserBasicStatisticsQueryResult.Succeeded(resultDTO);

			return result;
		}
		catch (Exception ex)
		{
			return GetUserBasicStatisticsQueryResult.Failed(ex);
		}
	}
}



