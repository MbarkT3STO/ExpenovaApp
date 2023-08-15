namespace AuthService.Common;

/// <summary>
/// Represents an error that occurred during the execution of a program.
/// </summary>
public class Error
{
	public string Message { get; set; }
	public Exception? Exception { get; set; }
	
	public Error(string message)
	{
		Message = message;
	}
	
	public Error(string message, Exception exception)
	{
		Message = message;
		Exception = exception;
	}
	
	public static Error FromException(Exception exception)
	{
		return new Error(exception.Message, exception);
	}
}
