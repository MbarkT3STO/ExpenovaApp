namespace ExpenseService.Domain.Events;

/// <summary>
/// Represents the data for a subscription expense created event.
/// </summary>
public class SubscriptionExpenseCreatedEventData
{

	public Guid Id { get; private set; }
	public string Description { get; private set; }
	public decimal Amount { get; private set; }
	public string UserId { get; private set; }
	public Guid CategoryId { get; private set; }
	public DateTime StartDate { get; private set; }
	public DateTime EndDate { get; private set; }
	public RecurrenceInterval RecurrenceInterval { get; private set; }
	public decimal BillingAmount { get; private set; }


	public DateTime CreatedAt { get; set; }
	public string CreatedBy { get; set; }
	public DateTime? LastUpdatedAt { get; set; }
	public string LastUpdatedBy { get; set; }
	public bool IsDeleted { get; set; }
	public DateTime? DeletedAt { get; set; }
	public string? DeletedBy { get; set; }


	private SubscriptionExpenseCreatedEventData()
	{
	}
	public SubscriptionExpenseCreatedEventData(Guid id, string description, decimal amount, string userId, Guid categoryId, DateTime startDate, DateTime endDate, RecurrenceInterval recurrenceInterval, decimal billingAmount, DateTime createdAt, string createdBy, DateTime? lastUpdatedAt, string lastUpdatedBy, bool isDeleted, DateTime? deletedAt, string? deletedBy)
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
/// Represents an event that is raised when a subscription expense is created.
/// </summary>
public class SubscriptionExpenseCreatedEvent: DomainEvent<SubscriptionExpenseCreatedEventData>
{
	public SubscriptionExpenseCreatedEvent(IDomainEventDetails eventDetails, SubscriptionExpenseCreatedEventData eventData): base(eventDetails, eventData)
	{
	}
	public static SubscriptionExpenseCreatedEvent Create(IDomainEventDetails eventDetails, SubscriptionExpenseCreatedEventData eventData)
	{
		return new SubscriptionExpenseCreatedEvent(eventDetails, eventData);
	}
}
