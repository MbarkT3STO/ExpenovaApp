namespace ExpenseService.Domain.Shared.Common;

/// <summary>
/// Represents an error that occurred during the execution of an operation.
/// </summary>
public class Error
{
	public string Message { get; }
	public DomainException? Exception { get; }

	public static readonly Error None = new (string.Empty);
	public static readonly Error NullValue = new ("Value cannot be null.");


	public Error(string message)
	{
		Message = message;
		Exception = null;
	}

	public Error(string message, DomainException exception)
	{
		Message = message;
		Exception = exception;
	}

	public static Error FromException(DomainException exception)
	{
		return new Error(exception.Message, exception);
	}

	public static Error FromException(Exception exception)
	{
		return new Error(exception.Message, new DomainException(exception.Message, exception));
	}
}
