using ExpenseService.Domain.Shared.Common;

namespace ExpenseService.Application.Common;

/// <summary>
/// Represents a failed query result with an error message.
/// </summary>
/// <typeparam name="TValue">The type of the query result value.</typeparam>
public class FailedQuery<TValue> : QueryResult<TValue>
{
	public FailedQuery(Error error): base(error)
	{
	}
	
	public static implicit operator FailedQuery<TValue>(Error error) => new(error);
}