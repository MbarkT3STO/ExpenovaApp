namespace ExpenseService.Domain.Events;

public class SubscriptionExpenseDeletedEventData
{
	public Guid Id { get; set; }
	public string Description { get; set; }
	public decimal Amount { get; set; }
	public string UserId { get; set; }
	public Guid CategoryId { get; set; }
	public DateTime StartDate { get; set; }
	public DateTime EndDate { get; set; }
	public RecurrenceInterval RecurrenceInterval { get; set; }
	public decimal BillingAmount { get; set; }

	public DateTime CreatedAt { get; }
	public string CreatedBy { get; }
	public DateTime? LastUpdatedAt { get; }
	public string LastUpdatedBy { get; }
	public bool IsDeleted { get; }
	public DateTime? DeletedAt { get; }
	public string? DeletedBy { get; set; }


	private SubscriptionExpenseDeletedEventData()
	{
	}


	public SubscriptionExpenseDeletedEventData(Guid id, string description, decimal amount, string userId, Guid categoryId, DateTime startDate, DateTime endDate, RecurrenceInterval recurrenceInterval, decimal billingAmount, DateTime createdAt, string createdBy, DateTime? lastUpdatedAt, string lastUpdatedBy, bool isDeleted, DateTime? deletedAt, string? deletedBy)
	{
		Id                 = id;
		Description        = description;
		Amount             = amount;
		UserId             = userId;
		CategoryId         = categoryId;
		StartDate          = startDate;
		EndDate            = endDate;
		RecurrenceInterval = recurrenceInterval;
		BillingAmount      = billingAmount;
		CreatedAt          = createdAt;
		CreatedBy          = createdBy;
		LastUpdatedAt      = lastUpdatedAt;
		LastUpdatedBy      = lastUpdatedBy;
		IsDeleted          = isDeleted;
		DeletedAt          = deletedAt;
		DeletedBy          = deletedBy;
	}
}


/// <summary>
/// Represents an event that is raised when a subscription expense is deleted.
/// </summary>
public class SubscriptionExpenseDeletedEvent : DomainEvent<SubscriptionExpenseDeletedEventData>
{
	public SubscriptionExpenseDeletedEvent(IDomainEventDetails eventDetails, SubscriptionExpenseDeletedEventData eventData) : base(eventDetails, eventData)
	{

	}


	/// <summary>
	/// Represents an event that is raised when a subscription expense is deleted.
	/// </summary>
	public static SubscriptionExpenseDeletedEvent Create(IDomainEventDetails eventDetails, SubscriptionExpenseDeletedEventData eventData)
	{
		return new SubscriptionExpenseDeletedEvent(eventDetails, eventData);
	}
}
