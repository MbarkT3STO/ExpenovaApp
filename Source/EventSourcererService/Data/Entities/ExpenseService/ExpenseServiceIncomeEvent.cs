using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventSourcererService.Data.Entities.ExpenseService;

/// <summary>
/// Represents an event that captures income related to the expense service.
/// </summary>
public class ExpenseServiceIncomeEvent : Event<ExpenseServiceIncomeEventJsonData>
{
	public ExpenseServiceIncomeEvent()
	{
	}

	public ExpenseServiceIncomeEvent(Guid eventId, string eventType, DateTime eventDate, string userId, ExpenseServiceIncomeEventJsonData eventData) : base(eventId, eventType, eventDate, userId, eventData)
	{
	}
}



/// <summary>
/// Represents the entity configuration for the ExpenseServiceIncomeEvent class.
/// </summary>
public class ExpenseServiceIncomeEventEntityConfig : IEntityTypeConfiguration<ExpenseServiceIncomeEvent>
{
	public void Configure(EntityTypeBuilder<ExpenseServiceIncomeEvent> builder)
	{
		builder.HasKey(e => e.Id);
		builder.Property(e => e.Id).ValueGeneratedOnAdd();

		// Configure the Converter for JsonData property
		builder.Property(e => e.JsonData).HasColumnType("jsonb")
		.HasConversion(
			value => JsonSerializer.Serialize(value, new JsonSerializerOptions { PropertyNamingPolicy = null }),
			value => JsonSerializer.Deserialize<ExpenseServiceIncomeEventJsonData>(value, new JsonSerializerOptions { PropertyNamingPolicy = null })
		);
	}

}