
using Microsoft.Extensions.Logging;

namespace ExpenseService.Application.Behaviors;

/// <summary>
/// Represents a behavior that logs information before and after handling a request.
/// </summary>
/// <typeparam name="TRequest">The type of the request.</typeparam>
/// <typeparam name="TResponse">The type of the response.</typeparam>
public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
	private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

	public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
	{
		_logger = logger;
	}

	public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
	{
		_logger.LogInformation("Handling {RequestType}", typeof(TRequest).Name);
		var response = await next();
		_logger.LogInformation("Handled {RequestType}", typeof(TRequest).Name);

		return response;
	}
}
