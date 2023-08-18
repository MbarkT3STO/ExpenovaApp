namespace ExpenseService.Domain.Entities;

public class User : Entity<string>
{
	public string FirstName { get; private set; }
	public string LastName { get; private set; }
	public string FullName => $"{FirstName} {LastName}";
	public DateTime CreatedAt { get; private set; }
	public DateTime? UpdatedAt { get; private set; }
	
	// Constructor for DDD
	private User()
	{
	}
	
	private User(string Id, string firstName, string lastName)
	{
		this.Id = Id;
		FirstName = firstName;
		LastName = lastName;
		CreatedAt = DateTime.UtcNow;
	}
	
	/// <summary>
	/// Creates a new instance of the User.
	/// </summary>
	/// <param name="Id">The user's ID.</param>
	/// <param name="firstName">The user's first name.</param>
	/// <param name="lastName">The user's last name.</param>
	/// <returns>A new instance of the User class.</returns>
	public static User Create(string Id, string firstName, string lastName)
	{
		return new User(Id, firstName, lastName);
	}
	
}
