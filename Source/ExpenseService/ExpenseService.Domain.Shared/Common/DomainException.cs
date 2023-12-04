namespace ExpenseService.Domain.Shared.Common;

/// <summary>
/// Represents a domain exception.
/// </summary>
public class DomainException : Exception
{
	public DomainException(string message) : base(message)
	{
	}
	
	public DomainException(string message, Exception innerException) : base(message, innerException)
	{
	}
	
	public DomainException(Error error) : base(error.Message)
	{
	}
	
	public DomainException(IEnumerable<Error> errors) : base(string.Join("\n", errors.Select(error => error.Message)))
	{
	}
}
