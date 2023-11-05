using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpenseService.Infrastructure.Data.EntityConfigs;

public class ExpenseEntityConfig : IEntityTypeConfiguration<ExpenseEntity>
{
	public void Configure(EntityTypeBuilder<ExpenseEntity> builder)
	{
		builder.HasKey(e => e.Id);
		
		builder.Property(e => e.Amount).IsRequired();
		builder.Property(e => e.Description).IsRequired();
		builder.Property(e => e.Date).IsRequired();
		
		builder.Property(e => e.CreatedBy).IsRequired(true);
		builder.Property(e => e.CreatedAt).IsRequired(true);
		
		builder.Property(e => e.LastUpdatedBy).IsRequired(false);
		builder.Property(e => e.LastUpdatedAt).IsRequired(false);
		
		
		builder.HasOne(e => e.Category).WithMany(c => c.Expenses).HasForeignKey(e => e.CategoryId);
		builder.HasOne(e => e.User).WithMany(u => u.Expenses).HasForeignKey(e => e.UserId);
		
		builder.ToTable(options => 
		{
			// The Amount column is constrained to be greater than 0
			options.HasCheckConstraint("CK_Expenses_Amount", "\"Amount\" > 0");			
		});
	}
}
