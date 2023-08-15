namespace AuthService.Common;

/// <summary>
/// Represents the result of a query operation that can either be successful or a failure.
/// </summary>
/// <typeparam name="TValue">The type of the value returned by the query.</typeparam>
public abstract class QueryResult<TValue> 
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public Error? Error { get; }
    public TValue? Value { get; }

    protected QueryResult(TValue value)
    {
        IsSuccess = true;
        Value = value;
    }

    protected QueryResult(Error error)
    {
        IsSuccess = false;
        Error = error;
    }
}
