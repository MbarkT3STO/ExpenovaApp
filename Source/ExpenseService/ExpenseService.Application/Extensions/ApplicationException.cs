namespace ExpenseService.Application.Extensions;

/// <summary>
/// Represents an application-specific exception.
/// </summary>
public class ApplicationException : Exception
{
	/// <summary>
	/// Gets the error associated with the exception.
	/// </summary>
	public Error Error { get; }

	public ApplicationException(Error error) : base(error.Message)
	{
		Error = error;
	}
	public ApplicationException(string message) : base(message)
	{
		Error = new Error(message);
	}

	public ApplicationException(string message, Exception innerException) : base(message, innerException)
	{
		Error = new Error(message);
	}
}