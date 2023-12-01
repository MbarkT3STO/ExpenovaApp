
namespace ExpenseService.Domain.Shared.Common;

/// <summary>
/// Represents an exception that is thrown when a specification is not satisfied.
/// </summary>
public class SpecificationException : DomainException
{
	/// <summary>
	/// Initializes a new instance of the <see cref="SpecificationException"/> class with a specified error message.
	/// </summary>
	/// <param name="message">The error message that explains the reason for the exception.</param>
	public SpecificationException(string message) : base(message)
	{
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="SpecificationException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
	/// </summary>
	/// <param name="message">The error message that explains the reason for the exception.</param>
	/// <param name="innerException">The exception that is the cause of the current exception.</param>
	public SpecificationException(string message, Exception innerException) : base(message, innerException)
	{
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="SpecificationException"/> class with a specified error.
	/// </summary>
	/// <param name="error">The error that caused the exception.</param>
	public SpecificationException(Error error) : base(error.Message)
	{
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="SpecificationException"/> class with a list of errors.
	/// </summary>
	/// <param name="errors">The list of errors that caused the exception.</param>
	public SpecificationException(List<Error> errors) : base(string.Join("\n", errors.Select(error => error.Message)))
	{
	}
}
