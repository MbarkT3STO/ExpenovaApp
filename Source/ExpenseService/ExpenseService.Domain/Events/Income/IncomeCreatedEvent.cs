namespace ExpenseService.Domain.Events.Income;

public class IncomeCreatedEventData
{
	public Guid Id { get; }
	public decimal Amount { get; }
	public string Description { get; }
	public DateTime Date { get; }
	public Guid CategoryId { get; }
	public string UserId { get; }

	private IncomeCreatedEventData()
	{
	}

	public IncomeCreatedEventData(Guid id, string description, decimal amount, DateTime date, Guid categoryId, string userId)
	{
		Id          = id;
		Amount      = amount;
		Description = description;
		Date        = date;
		CategoryId  = categoryId;
		UserId      = userId;
	}
}


/// <summary>
/// Represents an event that is raised when an income is created.
/// </summary>
public class IncomeCreatedEvent: DomainEvent<IncomeCreatedEventData>
{
	public IncomeCreatedEvent(IDomainEventDetails eventDetails, IncomeCreatedEventData eventData): base(eventDetails, eventData)
	{

	}

	public static IncomeCreatedEvent Create(IDomainEventDetails eventDetails, IncomeCreatedEventData eventData)
	{
		return new IncomeCreatedEvent(eventDetails, eventData);
	}
}
