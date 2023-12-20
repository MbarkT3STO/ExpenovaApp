using ExpenseService.Application.Category.Commands;
using ExpenseService.Application.Category.Queries;
using ExpenseService.Application.Expense.Queries;
using Microsoft.Extensions.Logging;

namespace ExpenseService.Application.Behaviors;

public class ExceptionHandlingBehavior<TRequest, TResponse>: IPipelineBehavior<TRequest, TResponse>
{
	private readonly ILogger<ExceptionHandlingBehavior<TRequest, TResponse>> _logger;
	private readonly Dictionary<Type, Func<Exception, TResponse>> _responseTypeToFailedResultMap = new()
	{
		{ typeof(CreateCategoryCommandResult), exception => (TResponse)(object) CreateCategoryCommandResult.Failed(new Error(exception.Message)) },
		{ typeof(UpdateCategoryCommandResult), exception => (TResponse)(object) UpdateCategoryCommandResult.Failed(new Error(exception.Message)) },
		{ typeof(DeleteCategoryCommandResult), exception => (TResponse)(object) DeleteCategoryCommandResult.Failed(new Error(exception.Message)) },
		{ typeof(GetCategoryByIdQueryResult), exception => (TResponse)(object) GetCategoryByIdQueryResult.Failed(new Error(exception.Message)) },
		{ typeof(GetCategoriesQueryResult), exception => (TResponse)(object) GetCategoriesQueryResult.Failed(new Error(exception.Message)) },
		{ typeof(GetExpensesQueryResult), exception => (TResponse)(object) GetExpensesQueryResult.Failed(new Error(exception.Message)) }
	};

	public ExceptionHandlingBehavior(ILogger<ExceptionHandlingBehavior<TRequest, TResponse>> logger)
	{
		_logger = logger;
	}

	public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
	{
		try
		{
			return await next();
		}
		catch (Exception exception)
		{
			_logger.LogError(exception, "An error occurred while handling the request: {RequestType}", typeof(TRequest).Name);

			var result = CreateFailedResult(exception);

			return result;
		}
	}


	/// <summary>
	/// Creates a failed result with the specified exception.
	/// </summary>
	/// <typeparam name="TResponse">The type of the response.</typeparam>
	private TResponse CreateFailedResult(Exception exception)
	{
		var isExistingResponseType = _responseTypeToFailedResultMap.TryGetValue(typeof(TResponse), out var createFailedResult);

		if (isExistingResponseType)
		{
			return createFailedResult(exception);
		}

		throw new InvalidOperationException($"The type {typeof(TResponse).Name} is not supported by the Exception Handling Behavior.");
	}


	/// <summary>
	/// Checks if the specified type is the same as the generic type parameter TResponse.
	/// </summary>
	/// <param name="type">The type to compare with the generic type parameter TResponse.</param>
	/// <returns>True if the specified type is the same as TResponse; otherwise, false.</returns>
	private static bool IsTResponseOfType(Type type)
	{
		return typeof(TResponse) == type;
	}
}