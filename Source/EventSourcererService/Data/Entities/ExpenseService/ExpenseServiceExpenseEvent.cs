using System.Text.Json;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventSourcererService.Data.Entities.ExpenseService;

/// <summary>
/// Represents an expense event in the Expense Service.
/// </summary>
public class ExpenseServiceExpenseEvent : Event<ExpenseServiceExpenseEventJsonData>
{
	public ExpenseServiceExpenseEvent(string type, DateTime timeStamp, string userId, ExpenseServiceExpenseEventJsonData jsonData) : base(type, timeStamp, userId, jsonData)
	{
	}
}


/// <summary>
/// Configures the entity framework mappings for the ExpenseServiceExpenseEvent entity.
/// </summary>
public class ExpenseServiceExpenseEventEntityConfig : IEntityTypeConfiguration<ExpenseServiceExpenseEvent>
{
	public void Configure(EntityTypeBuilder<ExpenseServiceExpenseEvent> builder)
	{
		builder.HasKey(e => e.Id);
		builder.Property(e => e.Id).ValueGeneratedOnAdd();

		// Configure the Converter for JsonData property
		builder.Property(e => e.JsonData).HasColumnType("jsonb")
		.HasConversion(
			value => JsonSerializer.Serialize(value, new JsonSerializerOptions { PropertyNamingPolicy = null }),
			value => JsonSerializer.Deserialize<ExpenseServiceExpenseEventJsonData>(value, new JsonSerializerOptions { PropertyNamingPolicy = null })
		);
	}

}