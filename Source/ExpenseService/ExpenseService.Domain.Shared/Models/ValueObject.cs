namespace ExpenseService.Domain.Shared.Models;

/// <summary>
/// Represents the base value object.
/// </summary>
public abstract class ValueObject
{
	protected abstract bool EqualsCore(ValueObject otherObject);
	protected abstract int GetHashCodeCore();


	public override bool Equals(object? obj)
	{
		return obj is ValueObject valueObject && EqualsCore(valueObject);
	}
	
	public override int GetHashCode()
	{
		return GetHashCodeCore();
	}

	public static bool operator ==(ValueObject a, ValueObject b)
	{
		if (a is null && b is null) return true;

		if (a is null || b is null) return false;

		return a.Equals(b);
	}
	
	public static bool operator !=(ValueObject a, ValueObject b)
	{
		return !(a == b);
	}

}
