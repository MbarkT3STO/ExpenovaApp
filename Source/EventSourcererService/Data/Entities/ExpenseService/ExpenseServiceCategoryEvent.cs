using System.Text.Json;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventSourcererService.Data.Entities.ExpenseService;

/// <summary>
/// Represents a category event in the Expense Service.
/// </summary>
public class ExpenseServiceCategoryEvent : Event<ExpenseServiceCategoryEventJsonData>
{
    public ExpenseServiceCategoryEvent(string type, DateTime timeStamp, string userId, ExpenseServiceCategoryEventJsonData jsonData) : base(type, timeStamp, userId, jsonData)
    {
    }
}


/// <summary>
/// Configures the entity framework mappings for the ExpenseServiceCategoryEvent entity.
/// </summary>
public class ExpenseServiceCategoryEventEntityConfig : IEntityTypeConfiguration<ExpenseServiceCategoryEvent>
{
	public void Configure(EntityTypeBuilder<ExpenseServiceCategoryEvent> builder)
	{
		builder.HasKey(e => e.Id);
		builder.Property(e => e.Id).ValueGeneratedOnAdd();
		
		// Configure the Converter for JsonData property
		builder.Property(e => e.JsonData).HasConversion(
			value => JsonSerializer.Serialize(value, new JsonSerializerOptions { PropertyNamingPolicy = null }),
			value => JsonSerializer.Deserialize<ExpenseServiceCategoryEventJsonData>(value, new JsonSerializerOptions { PropertyNamingPolicy = null })
		);

	}
	
}