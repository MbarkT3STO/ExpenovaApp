namespace ExpenseService.Infrastructure.Exceptions;

/// <summary>
/// Represents an exception that occurs in the infrastructure layer of the application.
/// </summary>
public class InfrastructureException : Exception
{
	protected InfrastructureException()
	{
	}

	protected InfrastructureException(string message) : base(message)
	{
	}

	protected InfrastructureException(string message, Exception innerException) : base(message, innerException)
	{
	}
}
