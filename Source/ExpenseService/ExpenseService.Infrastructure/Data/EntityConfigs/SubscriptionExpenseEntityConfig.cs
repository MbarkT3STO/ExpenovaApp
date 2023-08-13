using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpenseService.Infrastructure.Data.EntityConfigs;

public class SubscriptionExpenseEntityConfig : IEntityTypeConfiguration<SubscriptionExpenseEntity>
{
	public void Configure(EntityTypeBuilder<SubscriptionExpenseEntity> builder)
	{
		builder.HasKey(e => e.Id);
		
		builder.Property(e => e.Amount).IsRequired();
		builder.Property(e => e.Description).IsRequired();
		builder.Property(e => e.StartDate).IsRequired();
		builder.Property(e => e.EndDate).IsRequired();
		builder.Property(e => e.RecurrenceInterval).IsRequired();
		builder.Property(e => e.BillingAmount).IsRequired();
		
		builder.Property(e => e.CreatedBy).IsRequired();
		builder.Property(e => e.CreatedDate).IsRequired();
		builder.Property(e => e.LastUpdatedBy).IsRequired(false);
		builder.Property(e => e.LastUpdatedDate).IsRequired(false);
		
		builder.HasOne(e => e.Category).WithMany(c => c.SubscriptionExpenses).HasForeignKey(e => e.CategoryId);
		builder.HasOne(e => e.User).WithMany(u => u.SubscriptionExpenses).HasForeignKey(e => e.UserId);
		
		builder.ToTable(options => 
		{
			// The Amount column is constrained to be greater than 0
			options.HasCheckConstraint("CK_SubscriptionExpenses_Amount", "\"Amount\" > 0");
			
			// The BillingAmount column is constrained to be greater than 0
			options.HasCheckConstraint("CK_SubscriptionExpenses_BillingAmount", "\"BillingAmount\" > 0");
			
			// The StartDate column is constrained to be less than or equal to the EndDate column
			options.HasCheckConstraint("CK_SubscriptionExpenses_StartDate", "\"StartDate\" <= \"EndDate\"");
		});
	}
}
