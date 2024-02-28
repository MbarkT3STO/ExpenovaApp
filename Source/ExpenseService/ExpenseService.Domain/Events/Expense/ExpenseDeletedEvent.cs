namespace ExpenseService.Domain.Events;

public class ExpenseDeletedEventData
{
	public Guid Id { get; }
	public decimal Amount { get; }
	public string Description { get; }
	public DateTime Date { get; }
	public Guid CategoryId { get; }
	public string UserId { get; }

	public DateTime CreatedAt { get; }
	public string CreatedBy { get; }
	public DateTime? LastUpdatedAt { get; }
	public string LastUpdatedBy { get; }
	public bool IsDeleted { get; }
	public DateTime? DeletedAt { get; }


	private ExpenseDeletedEventData()
	{
	}

	public ExpenseDeletedEventData(Guid id, decimal amount, string description, DateTime date, Guid categoryId, string userId, DateTime createdAt, string createdBy, DateTime? lastUpdatedAt, string lastUpdatedBy, bool isDeleted, DateTime? deletedAt)
	{
		Id            = id;
		Amount        = amount;
		Description   = description;
		Date          = date;
		CategoryId    = categoryId;
		UserId        = userId;
		CreatedAt     = createdAt;
		CreatedBy     = createdBy;
		LastUpdatedAt = lastUpdatedAt;
		LastUpdatedBy = lastUpdatedBy;
		IsDeleted     = isDeleted;
		DeletedAt     = deletedAt;
	}
}


public class ExpenseDeletedEvent : DomainEvent<ExpenseDeletedEventData>
{
	public ExpenseDeletedEvent(IDomainEventDetails eventDetails, ExpenseDeletedEventData eventData) : base(eventDetails, eventData)
	{

	}

	public static ExpenseDeletedEvent Create(IDomainEventDetails eventDetails, ExpenseDeletedEventData eventData)
	{
		return new ExpenseDeletedEvent(eventDetails, eventData);
	}
}