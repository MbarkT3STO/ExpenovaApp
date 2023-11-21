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
	public static FailedCommandResult<Unit> CreateFailed() => new();
	public static FailedCommandResult<Unit> CreateFailed(Error error) => new(error);
}


/// <summary>
/// Represents the base result of a command execution with a value.
/// </summary>
/// <typeparam name="TValue">The type of the value.</typeparam>
public abstract class CommandResult<TValue, TCommandResult> : ICommandResult<TValue> where TCommandResult: CommandResult<TValue, TCommandResult>
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

	protected CommandResult(bool isSuccess)
	{
		IsSuccess = isSuccess;
		Error = isSuccess ? null : new Error("Unknown error");
	}
	
	public static TCommandResult Succeeded(TValue value) => Activator.CreateInstance(typeof(TCommandResult), value) as TCommandResult;
	public static TCommandResult Failed(Error error) => Activator.CreateInstance(typeof(TCommandResult), error) as TCommandResult;
	
	
}
