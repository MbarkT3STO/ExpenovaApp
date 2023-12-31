using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpenseService.Infrastructure.Data.EntityConfigs;

public class CategoryEntityConfig : IEntityTypeConfiguration<CategoryEntity>
{
	public void Configure(EntityTypeBuilder<CategoryEntity> builder)
	{
		builder.HasKey(c => c.Id);
		
		builder.Property(c => c.Name).IsRequired().HasMaxLength(50);
			
		builder.Property(c => c.Description).IsRequired(false);


		builder.Property(c => c.CreatedAt).IsRequired(true);
		builder.Property(c => c.CreatedBy).IsRequired(true);
		
		builder.Property(c => c.LastUpdatedAt).IsRequired(false);
		builder.Property(c => c.LastUpdatedBy).IsRequired(false);
		builder.Property(c => c.DeletedAt).IsRequired(false);
		builder.Property(c => c.DeletedBy).IsRequired(false);
		


		builder.HasOne(c => c.User)
			.WithMany(u => u.Categories)
			.HasForeignKey(c => c.UserId)
			.OnDelete(DeleteBehavior.Cascade);
			
		
		builder.HasQueryFilter( OnlyNonDeletedCategoriesQueryFilter() );
	}
	
	

	/// <summary>
	/// Returns an expression that filters out only non-deleted categories.
	/// </summary>
	/// <returns>An expression that represents the filter condition.</returns>
	private static Expression<Func<CategoryEntity, bool>> OnlyNonDeletedCategoriesQueryFilter()
	{
		return categoryEntity => categoryEntity.IsDeleted == false;
	}
}
