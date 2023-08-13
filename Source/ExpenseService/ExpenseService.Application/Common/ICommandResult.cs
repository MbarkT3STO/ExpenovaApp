using ExpenseService.Domain.Shared.Common;

namespace ExpenseService.Application.Common;

/// <summary>
/// Represents the result of a command operation.
/// </summary>
public interface ICommandResult
{
	bool IsSuccess { get; }
	bool IsFailure { get; }
	Error? Error { get; }
}

/// <summary>
/// Represents the result of a command operation.
/// </summary>
/// <typeparam name="T">The type of the value returned by the command.</typeparam>
public interface ICommandResult<out T>: ICommandResult
{
	bool IsSuccess { get; }
	bool IsFailure { get; }
	Error? Error { get; }
	T Value { get; }
}