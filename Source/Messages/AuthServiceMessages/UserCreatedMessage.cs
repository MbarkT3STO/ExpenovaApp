namespace Messages.AuthServiceMessages;

/// <summary>
/// Represents a message that is sent when a new user is created.
/// </summary>
public class UserCreatedMessage : BaseEventMessage
{
	public string UserId { get; set; }
	public string Email { get; set; }
	public string Username { get; set; }
	public string? FirstName { get; set; }
	public string? LastName { get; set; }
	public string RoleId { get; set; }
	public DateTime CreatedAt { get; set; }
}
