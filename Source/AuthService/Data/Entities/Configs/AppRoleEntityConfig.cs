using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthService.Data.Entities.Configs;

public class AppRoleEntityConfig : IEntityTypeConfiguration<AppRole>
{
	public void Configure(EntityTypeBuilder<AppRole> builder)
	{
		builder.HasKey(x => x.Id);
		
		builder.Property(x => x.Name)
			.IsRequired()
			.HasMaxLength(50);
	}
}
