namespace ExpenseService.Domain.Events;

/// <summary>
/// Represents the data associated with an expense created event.
/// </summary>
public class ExpenseCreatedEventData
{
	public Guid Id { get; }
	public decimal Amount { get; }
	public string Description { get; }
	public DateTime Date { get; }
	public Guid CategoryId { get; }
	public string UserId { get; }


	private ExpenseCreatedEventData()
	{
	}

	public ExpenseCreatedEventData(Guid id, decimal amount, string description, DateTime date, Guid categoryId, string userId)
	{
		Id = id;
		Amount = amount;
		Description = description;
		Date = date;
		CategoryId = categoryId;
		UserId = userId;
	}
}


/// <summary>
/// Represents an event that is raised when an expense is created.
/// </summary>
public class ExpenseCreatedEvent : DomainEvent<ExpenseCreatedEventData>
{
	public ExpenseCreatedEvent(IDomainEventDetails eventDetails, ExpenseCreatedEventData eventData) : base(eventDetails, eventData)
	{

	}

	/// <summary>
	/// Represents an event that is raised when an expense is created.
	/// </summary>
	public static ExpenseCreatedEvent Create(IDomainEventDetails eventDetails, ExpenseCreatedEventData eventData)
	{
		return new ExpenseCreatedEvent(eventDetails, eventData);
	}
}