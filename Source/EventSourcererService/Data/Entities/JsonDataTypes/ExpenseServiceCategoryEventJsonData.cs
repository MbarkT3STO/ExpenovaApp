namespace EventSourcererService.Data.Entities.JsonDataTypes;

public class ExpenseServiceCategoryEventJsonData : AuditableJsonEntity<Guid>
{
    public string Name { get; set; }
    public string UserId { get; set; }
}
