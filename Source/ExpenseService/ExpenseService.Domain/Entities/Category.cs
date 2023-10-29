namespace ExpenseService.Domain.Entities;

public class Category: AuditableEntity<Guid>
{
	public string Name { get; private set; }
	public string Description { get; private set; }
	public string UserId { get; private set; }
	
	protected Category() { }
	public Category( string name, string description, string userId)
	{
		Name        = name;
		Description = description;
		UserId      = userId;
	}
	
	
	/// <summary>
	/// Updates the name of the category.
	/// </summary>
	/// <param name="newName">The new name of the category.</param>
	public void UpdateName(string newName)
	{
		Name = newName;
	}
	
	/// <summary>
	/// Updates the description of the category.
	/// </summary>
	/// <param name="newDescription">The new description to set for the category.</param>
	public void UpdateDescription(string newDescription)
	{
		Description = newDescription;
	}
}
