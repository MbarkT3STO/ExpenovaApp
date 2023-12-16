using ExpenseService.Domain.Shared.Common;

namespace ExpenseService.Application.Common;

/// <summary>
/// Represents the result of a query operation that can either succeed with a value or fail with an error.
/// </summary>
/// <typeparam name="TValue">The type of the value returned upon success.</typeparam>
public abstract class QueryResult<TValue> : IQueryResult<TValue>, IQueryResult
{
	public bool IsSuccess { get; }
	public bool IsFailure => !IsSuccess;
	public Error? Error { get; }
	public TValue? Value { get; }

	object? IQueryResult.Value => Value;

	protected QueryResult(TValue? value)
	{
		IsSuccess = true;
		Value = value;
	}

	protected QueryResult(Error error)
	{
		IsSuccess = false;
		Error = error;
	}


	/// <summary>
	/// Creates a new instance of <see cref="SucceededQuery{TValue}"/> with the specified value.
	/// </summary>
	/// <typeparam name="TValue">The type of the value.</typeparam>
	/// <param name="value">The value.</param>
	/// <returns>A new instance of <see cref="SucceededQuery{TValue}"/> with the specified value.</returns>
	public static SucceededQuery<TValue> Succeeded(TValue value) => new(value);

	/// <summary>
	/// Creates a new instance of <see cref="FailedQuery{TValue}"/> with the specified <paramref name="error"/>.
	/// </summary>
	/// <typeparam name="TValue">The type of the value in the failed query result.</typeparam>
	/// <param name="error">The error that caused the query to fail.</param>
	/// <returns>A new instance of <see cref="FailedQuery{TValue}"/> with the specified <paramref name="error"/>.</returns>
	public static FailedQuery<TValue> Failed(Error error) => new(error);
}
