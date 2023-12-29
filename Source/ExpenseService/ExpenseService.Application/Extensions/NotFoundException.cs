namespace ExpenseService.Application.Extensions;

/// <summary>
/// Represents an exception that is thrown when a resource is not found.
/// </summary>
public class NotFoundException : ApplicationException
{
    public NotFoundException(Error error) : base(error)
    {
    }

    public NotFoundException(string message) : base(message)
    {
    }

    public NotFoundException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
