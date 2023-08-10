namespace ExpenseService.Domain.Entities;

public class User : AuditableEntity<string>
{
	public string FirstName { get; private set; }
	public string LastName { get; private set; }
	public string FullName => $"{FirstName} {LastName}";
	public DateTime CreatedAt { get; private set; }
	public DateTime? UpdatedAt { get; private set; }
}
