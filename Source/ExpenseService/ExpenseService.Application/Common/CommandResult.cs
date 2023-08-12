using ExpenseService.Domain.Shared.Common;

namespace ExpenseService.Application.Common;

/// <summary>
/// Represents the base result of a command execution.
/// </summary>
public abstract class CommandResult : ICommandResult
{
	public bool IsSuccess { get; }
	public bool IsFailure => !IsSuccess;
	public Error? Error { get; }
	
	
	protected CommandResult(Error error)
	{
		IsSuccess = false;
		Error = error;
	}
	protected CommandResult(bool isSuccess)
	{
		IsSuccess = isSuccess;
		Error = isSuccess ? null : new Error("Unknown error");
	}


	public static SucceededCommandResult CreateSucceeded() => new();
	public static FailedCommandResult CreateFailed() => new();
	public static FailedCommandResult CreateFailed(Error error) => new(error);
}


/// <summary>
/// Represents the base result of a command execution with a value.
/// </summary>
/// <typeparam name="TValue">The type of the value.</typeparam>
public abstract class CommandResult<TValue> : ICommandResult<TValue>
{
	public bool IsSuccess { get; }
	public bool IsFailure => !IsSuccess;
	public Error? Error { get; }
	public TValue Value { get; }
	
	
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


	public static SucceededCommandResult<TValue> CreateSucceeded(TValue value) => new(value);
	public static FailedCommandResult CreateFailed(Error error) => new(error);
}
