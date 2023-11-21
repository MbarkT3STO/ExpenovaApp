using ExpenseService.Domain.Shared.Common;

namespace ExpenseService.Application.Common;

/// <summary>
/// Represents a failed command result with an error.
/// </summary>
public class FailedCommandResult<TValue> : CommandResult<TValue, FailedCommandResult<TValue>>
{
	public FailedCommandResult() : base(false)
	{
	}
	
	public FailedCommandResult(Error error) : base(error)
	{
	}
}