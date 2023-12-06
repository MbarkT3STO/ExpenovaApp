using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpenseService.Infrastructure.Data.EntityConfigs;

public class OutboxEventEntityConfig : IEntityTypeConfiguration<OutboxMessage>
{
    public void Configure(EntityTypeBuilder<OutboxMessage> builder)
    {
        builder.ToTable("OutboxMessages");
        
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedOnAdd();
        
        builder.Property(e => e.EventName).IsRequired();
        builder.Property(e => e.Data).IsRequired();
        builder.Property(e => e.CreatedAt).IsRequired();
        builder.Property(e => e.IsProcessed).IsRequired();
    }
}