namespace ExpenseService.Application.Common;

/// <summary>
/// Represents a successful command result with no message.
/// </summary>
public class SucceededCommandResult : CommandResult
{
	public SucceededCommandResult() : base(true) { }
}

/// <summary>
/// Represents a successful command result with value.
/// </summary>
/// <typeparam name="TValue">The type of the value.</typeparam>
public class SucceededCommandResult<TValue> : CommandResult<TValue>
{
	public SucceededCommandResult(TValue value) : base(value) { }
}
