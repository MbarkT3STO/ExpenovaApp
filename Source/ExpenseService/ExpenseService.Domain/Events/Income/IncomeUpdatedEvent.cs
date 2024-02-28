namespace ExpenseService.Domain.Events.Income;

public class IncomeUpdatedEventData
{
	public Guid Id { get; set; }
	public string Description { get; set; }
	public DateTime Date { get; set; }
	public decimal Amount { get; set; }
	public Guid CategoryId { get; set; }
	public string UserId { get; set; }

	public IncomeUpdatedEventData(Guid id, string description, DateTime date, decimal amount, Guid categoryId, string userId)
	{
		Id          = id;
		Description = description;
		Date        = date;
		Amount      = amount;
		CategoryId  = categoryId;
		UserId      = userId;
	}

	private IncomeUpdatedEventData()
	{
	}
}


/// <summary>
/// Represents an event that is raised when an income is updated.
/// </summary>
public class IncomeUpdatedEvent: DomainEvent<IncomeUpdatedEventData>
{
	public IncomeUpdatedEvent(IDomainEventDetails eventDetails, IncomeUpdatedEventData eventData): base(eventDetails, eventData)
	{
	}

	/// <summary>
	/// Creates a new instance of the IncomeUpdatedEvent class.
	/// </summary>
	/// <param name="eventDetails">The details of the domain event.</param>
	/// <param name="eventData">The data associated with the income updated event.</param>
	/// <returns>A new instance of the IncomeUpdatedEvent class.</returns>
	public static IncomeUpdatedEvent Create(IDomainEventDetails eventDetails, IncomeUpdatedEventData eventData)
	{
		return new IncomeUpdatedEvent(eventDetails, eventData);
	}
}