namespace ExpenseService.Infrastructure.Data;

public class AppDbContext: DbContext
{
	public AppDbContext(DbContextOptions<AppDbContext> options): base(options) { }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
	}

	public DbSet<UserEntity> Users { get; set; }
	public DbSet<CategoryEntity> Categories { get; set; }
	public DbSet<ExpenseEntity> Expenses { get; set; }
	public DbSet<SubscriptionExpenseEntity> SubscriptionExpenses { get; set; }
	public DbSet<IncomeEntity> Incomes { get; set; }

	public DbSet<OutboxMessage> OutboxMessages { get; set; }
}
