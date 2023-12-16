using Microsoft.Extensions.Logging;

namespace ExpenseService.Application.Behaviors;

public class ExceptionHandlingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
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

			if (typeof(TResponse).IsAssignableFrom(typeof(CommandResult)))
			{
				// Create a failed result from the TResponse type, because it is a CommandResult and every CommandResult has a static Failed method
				var failedResult = Activator.CreateInstance(typeof(TResponse), new Error(exception.Message));

				return (TResponse)failedResult;
			}
			else
			{
				_logger.LogError(exception, "An error occurred while handling the request: {RequestType}", typeof(TRequest).Name);

				return default;
			}

		}
	}
}