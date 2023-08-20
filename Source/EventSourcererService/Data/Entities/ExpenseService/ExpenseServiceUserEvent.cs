using System.Text.Json;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventSourcererService.Data.Entities.ExpenseService;

/// <summary>
///Represents a User event in the Expense Service.
/// </summary>
public class ExpenseServiceUserEvent : Event<ExpenseServiceUserEventJsonData>
{
	public ExpenseServiceUserEvent(string type, DateTime timeStamp, string userId, ExpenseServiceUserEventJsonData jsonData) : base(type, timeStamp, userId, jsonData)
	{
	}

	public ExpenseServiceUserEventJsonData JsonData { get; set; }
}


/// <summary>
/// Configures the entity framework mappings for the ExpenseServiceUserEvent entity.
/// </summary>
public class ExpenseServiceUserEventEntityConfig : IEntityTypeConfiguration<ExpenseServiceUserEvent>
{
	public void Configure(EntityTypeBuilder<ExpenseServiceUserEvent> builder)
	{
		builder.HasKey(e => e.Id);
		builder.Property(e => e.Id).ValueGeneratedOnAdd();

		// Configure the Converter for JsonData property
		builder.Property(e => e.JsonData).HasColumnType("jsonb")
		.HasConversion(
			value => JsonSerializer.Serialize(value, new JsonSerializerOptions { PropertyNamingPolicy = null }),
			value => JsonSerializer.Deserialize<ExpenseServiceUserEventJsonData>(value, new JsonSerializerOptions { PropertyNamingPolicy = null })
		);
	}

}