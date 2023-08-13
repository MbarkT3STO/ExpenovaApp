using ExpenseService.Domain.Shared.Entities;

namespace ExpenseService.Infrastructure.Data.Entities;

public class ExpenseEntity : AuditableEntity<Guid>
{
	public Guid Id { get; set; }
	public decimal Amount { get; set; }
	public string Description { get; set; }
	public DateTime Date { get; set; }
	
	public Guid CategoryId { get; set; }
	public CategoryEntity Category { get; set; }

	public string UserId { get; set; }
	public UserEntity User { get; set; }
}