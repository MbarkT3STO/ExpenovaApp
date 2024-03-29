using System.Text.Json;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventSourcererService.Data.Entities.ExpenseService;


/// <summary>
///Represents a subscription expense event in the Expense Service.
/// </summary>
public class ExpenseServiceSubscriptionExpenseEvent : Event<ExpenseServiceSubscriptionExpenseEventJsonData>
{
	public ExpenseServiceSubscriptionExpenseEvent()
	{
	}

	public ExpenseServiceSubscriptionExpenseEvent(Guid eventId, string type, DateTime timeStamp, string userId, ExpenseServiceSubscriptionExpenseEventJsonData jsonData) : base(eventId, type, timeStamp, userId, jsonData)
	{
	}
}


/// <summary>
/// Configures the entity framework mappings for the ExpenseServiceSubscriptionExpenseEvent entity.
/// </summary>
public class ExpenseServiceSubscriptionExpenseEventEntityConfig : IEntityTypeConfiguration<ExpenseServiceSubscriptionExpenseEvent>
{
	public void Configure(EntityTypeBuilder<ExpenseServiceSubscriptionExpenseEvent> builder)
	{
		builder.HasKey(e => e.Id);
		builder.Property(e => e.Id).ValueGeneratedOnAdd();

		// Configure the Converter for JsonData property
		builder.Property(e => e.JsonData).HasColumnType("jsonb")
		.HasConversion(
			value => JsonSerializer.Serialize(value, new JsonSerializerOptions { PropertyNamingPolicy = null }),
			value => JsonSerializer.Deserialize<ExpenseServiceSubscriptionExpenseEventJsonData>(value, new JsonSerializerOptions { PropertyNamingPolicy = null })
		);
	}

}