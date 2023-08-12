namespace ExpenseService.Application.Common;

/// <summary>
/// Represents a successful query result with a value of type <typeparamref name="TValue"/>.
/// </summary>
/// <typeparam name="TValue">The type of the value returned by the query.</typeparam>
public class SucceededQuery<TValue> : QueryResult<TValue>
{
	public SucceededQuery(TValue? value) : base(value)
	{
	}


	public static implicit operator SucceededQuery<TValue>(TValue value) => new(value);
}
