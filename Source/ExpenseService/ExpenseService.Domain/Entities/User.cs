namespace ExpenseService.Domain.Entities;

public class User: Entity<string>
{
	public string FirstName { get; private set; }
	public string LastName { get; private set; }
	public string FullName => $"{FirstName} {LastName}";
	public string Email { get; private set; }
	public string RoleId { get; private set; }
	public DateTime CreatedAt { get; private set; }
	public DateTime? UpdatedAt { get; private set; }

	// Constructor for DDD
	private User()
	{
	}

	private User(string Id, string firstName, string lastName, string email, string roleId)
	{
		this.Id   = Id;
		FirstName = firstName;
		LastName  = lastName;
		Email     = email;
		RoleId    = roleId;
		CreatedAt = DateTime.UtcNow;
	}

	/// <summary>
	/// Creates a new instance of the User class.
	/// </summary>
	/// <param name="Id">The user's ID.</param>
	/// <param name="firstName">The user's first name.</param>
	/// <param name="lastName">The user's last name.</param>
	/// <param name="email">The user's email address.</param>
	/// <param name="roleId">The user's role ID.</param>
	/// <returns>A new instance of the User class.</returns>
	public static User Create(string Id, string firstName, string lastName, string email, string roleId)
	{
		return new User(Id, firstName, lastName, email, roleId);
	}


}
