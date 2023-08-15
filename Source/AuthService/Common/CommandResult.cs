namespace AuthService.Common;

/// <summary>
/// Represents the result of a command operation that can either succeed with a value or fail with an error.
/// </summary>
/// <typeparam name="TValue">The type of the value returned upon success.</typeparam>
public abstract class CommandResult<TValue>
{
	public bool IsSuccess { get; set; }
	public bool IsFailure => !IsSuccess;
	public Error? Error { get; set; }
	public TValue? Value { get; set; }
	
	
	protected CommandResult(TValue value)
	{
		IsSuccess = true;
		Value = value;
	}
	
	protected CommandResult(Error error)
	{
		IsSuccess = false;
		Error = error;
	}
}
