namespace ExpenseService.Infrastructure.Exceptions.RepositoryExceptions;

/// <summary>
/// Represents an exception that is thrown when a requested record is not found.
/// </summary>
public class NotFoundException : InfrastructureException
{
	public NotFoundException()
	{
	}

	public NotFoundException(string message) : base(message)
	{
	}

	public NotFoundException(string message, Exception innerException) : base(message, innerException)
	{
	}
}
