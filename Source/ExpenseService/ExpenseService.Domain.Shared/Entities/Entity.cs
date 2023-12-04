using ExpenseService.Domain.Shared.Common;

namespace ExpenseService.Domain.Shared.Entities;

/// <summary>
/// Represents the base entity.
/// </summary>
public abstract class Entity<T> : IEntity<T>
{
	public T Id { get; protected set; }
	
	public override bool Equals(object? obj)
	{
		var compareTo = obj as Entity<T>;
		
		if (ReferenceEquals(this, compareTo)) return true;
		if(compareTo is null) return false;
		
		return Id.Equals(compareTo.Id);
	}
	
	public override int GetHashCode()
	{
		// Return the hash code for the Id field by calculating the hash code for the type of the Id field.
		return GetType().GetHashCode() * 907 + Id.GetHashCode();
	} 
	
	public static bool operator == (Entity<T> a, Entity<T> b)
	{
		// If both are null, or both are same instance, return true.
		if (ReferenceEquals(a, b)) return true;
		
		// If one is null, but not both, return false.
		if (a is null || b is null) return false;
		
		// Return true if the fields match:
		return a.Equals(b);
	}
	
	public static bool operator != (Entity<T> a, Entity<T> b)
	{
		return !(a == b);
	}
}
