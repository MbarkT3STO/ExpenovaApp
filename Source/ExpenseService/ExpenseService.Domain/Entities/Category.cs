namespace ExpenseService.Domain.Entities;

public class Category : AuditableEntity<Guid>
{
	public string Name { get; private set; }
	public string Description { get; private set; }
	public string UserId { get; private set; }
	
	protected Category() { }
	public Category( string name, string description, string userId)
	{
		Name = name;
		Description = description;
		UserId = userId;
	}
}
