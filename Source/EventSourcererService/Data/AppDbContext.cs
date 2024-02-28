using Microsoft.EntityFrameworkCore;

namespace EventSourcererService.Data;

public class AppDbContext : DbContext
{
	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
	{
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
	}

	public DbSet<ExpenseServiceUserEvent> ExpenseService_UserEvents { get; set; }
	public DbSet<ExpenseServiceCategoryEvent> ExpenseService_CategoryEvents { get; set; }
	public DbSet<ExpenseServiceExpenseEvent> ExpenseService_ExpenseEvents { get; set; }
	public DbSet<ExpenseServiceSubscriptionExpenseEvent> ExpenseService_SubscriptionExpenseEvents { get; set; }
	public DbSet<ExpenseServiceIncomeEvent> ExpenseService_IncomeEvents { get; set; }

}
