using ExpenseService.Domain.Enums;

namespace ExpenseService.Infrastructure.Data.Entities;

public class SubscriptionExpenseEntity : AuditableEntity<Guid>
{
	public Guid Id { get; set; }
	public decimal Amount { get; private set; }
	public string Description { get; private set; }
	public DateTime StartDate { get; private set; }
	public DateTime EndDate { get; private set; }
	public RecurrenceInterval RecurrenceInterval { get; private set; }
	public decimal BillingAmount { get; private set; }
	
	public Guid CategoryId { get; set; }
	public CategoryEntity Category { get; set; }
	
	public string UserId { get; set; }
	public UserEntity User { get; set; }
}
