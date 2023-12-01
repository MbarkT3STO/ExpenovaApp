namespace ExpenseService.Domain.Shared.Common;

/// <summary>
/// Represents the result of a satisfaction check.
/// </summary>
public class SatisfactionResult
{
	/// <summary>
	/// Gets or sets a value indicating whether the satisfaction check is satisfied.
	/// </summary>
	public bool IsSatisfied { get; set; }

	/// <summary>
	/// Gets or sets the list of errors if the satisfaction check is not satisfied.
	/// </summary>
	public List<Error> Errors { get; set; } = new();


	public SatisfactionResult(bool isSatisfied)
	{
		IsSatisfied = isSatisfied;
	}
	
	public SatisfactionResult(Error error)
	{
		Errors.Add(error);
		IsSatisfied = false;
	}

	public SatisfactionResult(List<Error> errors)
	{
		Errors      = errors;
		IsSatisfied = false;
	}
	
	

	/// <summary>
	/// Creates a new instance of the <see cref="SatisfactionResult"/> class indicating that the satisfaction is met.
	/// </summary>
	public static SatisfactionResult Satisfied()
	{
		var satisfactionResult = new SatisfactionResult(true);

		return satisfactionResult;
	}
	
	
	/// <summary>
	/// Creates a new instance of the <see cref="SatisfactionResult"/> class indicating that the satisfaction is not met.
	/// </summary>
	/// <param name="error">The error message associated with the unsatisfied condition.</param>
	public static SatisfactionResult NotSatisfied(Error error)
	{
		var satisfactionResult = new SatisfactionResult(error);

		return satisfactionResult;
	}
	
	
	/// <summary>
	/// Creates a new instance of the <see cref="SatisfactionResult"/> class indicating that the satisfaction is not met.
	/// </summary>
	/// <param name="errors">The list of errors associated with the unsatisfied condition.</param>
	public static SatisfactionResult NotSatisfied(List<Error> errors)
	{
		var satisfactionResult = new SatisfactionResult(errors);

		return satisfactionResult;
	}
	
}
