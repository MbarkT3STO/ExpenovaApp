namespace EventSourcererService.Data.Entities.JsonDataTypes;

public class ExpenseServiceUserEventJsonData : AuditableJsonEntity<string>
{
	public string FirstName { get; set; }
	public string LastName { get; set; }
	public string UserName { get; set; }
	public string Email { get; set; }
	public string RoleId { get; set; }
	public DateTime CreatedAt { get; private set; }
	public DateTime? UpdatedAt { get; private set; }


	public ExpenseServiceUserEventJsonData(string id, string firstName, string lastName, string userName, string email, string roleId, DateTime createdAt)
	{
		Id = id;
		FirstName = firstName;
		LastName = lastName;
		UserName = userName;
		Email = email;
		RoleId = roleId;
		CreatedAt = createdAt;
	}

}