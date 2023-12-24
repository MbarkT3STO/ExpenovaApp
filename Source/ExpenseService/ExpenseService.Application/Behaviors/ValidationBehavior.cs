using FluentValidation;

namespace ExpenseService.Application.Behaviors;

/// <summary>
/// Represents a behavior that performs validation on a request before it is handled by the pipeline.
/// </summary>
/// <typeparam name="TRequest">The type of the request.</typeparam>
/// <typeparam name="TResponse">The type of the response.</typeparam>
public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
	private readonly IEnumerable<IValidator<TRequest>> _validators;

	public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
	{
		_validators = validators;
	}


	public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
	{
		var context = new ValidationContext<TRequest>(request);

		var failures = _validators
			.Select(v => v.Validate(context))
			.SelectMany(result => result.Errors)
			.Where(f => f != null)
			.ToList();

		if (failures.Count != 0)
		{
			var errorMessages = failures.Select(f => f.ErrorMessage).ToList();
			var combinedErrorMessage = string.Join("/n", errorMessages);

			throw new ValidationException(combinedErrorMessage);
		}

		return await next();
	}
}