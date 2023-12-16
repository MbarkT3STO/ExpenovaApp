using ExpenseService.Application.Category.Commands;
using ExpenseService.Application.Category.Queries;
using ExpenseService.Application.Expense.Queries;
using Microsoft.Extensions.Logging;

namespace ExpenseService.Application.Behaviors;

public class ExceptionHandlingBehavior<TRequest, TResponse>: IPipelineBehavior<TRequest, TResponse>
{
	private readonly ILogger<ExceptionHandlingBehavior<TRequest, TResponse>> _logger;

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
	private static TResponse CreateFailedResult(Exception exception)
	{
		if (IsTResponseOfType(typeof(CreateCategoryCommandResult)))
		{
			var failedResult = CreateCategoryCommandResult.Failed(new Error(exception.Message));
			return (TResponse)(object)failedResult;
		}
		else if (IsTResponseOfType(typeof(UpdateCategoryCommandResult)))
		{
			var failedResult = UpdateCategoryCommandResult.Failed(new Error(exception.Message));
			return (TResponse)(object)failedResult;
		}
		else if (IsTResponseOfType(typeof(DeleteCategoryCommandResult)))
		{
			var failedResult = DeleteCategoryCommandResult.Failed(new Error(exception.Message));
			return (TResponse)(object)failedResult;
		}
		else if (IsTResponseOfType(typeof(GetCategoryByIdQueryResult)))
		{
			var failedResult = GetCategoryByIdQueryResult.Failed(new Error(exception.Message));
			return (TResponse)(object)failedResult;
		}
		else if (IsTResponseOfType(typeof(GetCategoriesQueryResult)))
		{
			var failedResult = GetCategoriesQueryResult.Failed(new Error(exception.Message));
			return (TResponse)(object)failedResult;
		}
		else if (IsTResponseOfType(typeof(GetExpensesQueryResult)))
		{
			var failedResult = GetExpensesQueryResult.Failed(new Error(exception.Message));
			return (TResponse)(object)failedResult;
		}

		return default;
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