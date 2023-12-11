namespace ExpenseService.Domain.Shared.Common;

/// <summary>
/// Represents the result of an operation that can either succeed or fail.
/// </summary>
public class Result : IResult
{
	public bool IsSuccess { get; }
	public bool IsFailure => !IsSuccess;
	public Error Error { get; }

	public static Result Success() => new Result(true, Error.None);
	public static Result Failure(Error error) => new Result(false, error);

	public Result(bool isSuccess, Error error)
	{
		IsSuccess = isSuccess;
		Error = error;
	}

	public static implicit operator bool(Result result) => result.IsSuccess;

	public static implicit operator Result(Error error) => Failure(error);

}



/// <summary>
/// Represents the result of an operation that can either succeed or fail.
/// </summary>
public class Result<TValue> : Result
{
	public TValue Value { get; }

	public Result(TValue value, bool isSuccess, Error error) : base(isSuccess, error)
	{
		Value = value;
	}

	public static implicit operator TValue(Result<TValue> result) => result.Value;

	public static implicit operator Result<TValue>(TValue value) => Success(value);

	public static implicit operator Result<TValue>(Error error) => Failure(error);

	public static Result<TValue> Success(TValue value) => new (value, true, Error.None);
	public new static Result<TValue> Failure(Error error) => new (default!, false, error);

	public Result<TValue> OnSuccess(Action<TValue> action)
	{
		if (IsSuccess)
			action(Value);

		return this;
	}

	public Result<TValue> OnFailure(Action<Error> action)
	{
		if (IsFailure)
			action(Error);

		return this;
	}

	public Result<TValue> OnSuccess(Func<TValue, Result<TValue>> func)
	{
		if (IsSuccess)
			return func(Value);

		return this;
	}

	public Result<TValue> OnFailure(Func<Error, Result<TValue>> func)
	{
		if (IsFailure)
			return func(Error);

		return this;
	}
}
