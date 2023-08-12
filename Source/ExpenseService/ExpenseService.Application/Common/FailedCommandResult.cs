using ExpenseService.Domain.Shared.Common;

namespace ExpenseService.Application.Common;

/// <summary>
/// Represents a failed command result with an error.
/// </summary>
public class FailedCommandResult : CommandResult
{
	public FailedCommandResult() : base(false)
	{
	}
	
	public FailedCommandResult(Error error) : base(error)
	{
	}
}