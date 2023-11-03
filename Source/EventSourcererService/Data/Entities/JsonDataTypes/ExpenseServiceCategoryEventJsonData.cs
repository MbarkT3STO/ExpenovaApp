namespace EventSourcererService.Data.Entities.JsonDataTypes;

public class ExpenseServiceCategoryEventJsonData : AuditableJsonEntity<Guid>
{
	public string Name { get; set; }
	public string UserId { get; set; }

	public ExpenseServiceCategoryEventJsonData(Guid id, string name, string userId, DateTime createdAt, string createdBy) : base(id, createdAt, createdBy)
	{
		Name = name;
		UserId = userId;
	}

	public ExpenseServiceCategoryEventJsonData(Guid id, string name, string userId, DateTime createdAt, string createdBy, DateTime? lastUpdatedAt, string lastUpdatedBy) : base(id, createdAt, createdBy, lastUpdatedAt, lastUpdatedBy)
	{
		Name = name;
		UserId = userId;
	}

	public ExpenseServiceCategoryEventJsonData(Guid id, string name, string userId, DateTime createdAt, string createdBy, DateTime? lastUpdatedAt, string lastUpdatedBy, DateTime? deletedAt, string deletedBy) : base(id, createdAt, createdBy, lastUpdatedAt, lastUpdatedBy, deletedAt, deletedBy)
	{
		Name = name;
		UserId = userId;
	}

}
