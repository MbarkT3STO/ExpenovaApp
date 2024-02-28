using System;

namespace ExpenseService.Domain.Events;

public class ExpenseUpdatedEventData
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


	private ExpenseUpdatedEventData()
	{
	}

	public ExpenseUpdatedEventData(Guid id, decimal amount, string description, DateTime date, Guid categoryId, string userId, DateTime createdAt, string createdBy, DateTime? lastUpdatedAt, string lastUpdatedBy, bool isDeleted, DateTime? deletedAt)
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


/// <summary>
/// Represents an event that is raised when an expense is updated.
/// </summary>
public class ExpenseUpdatedEvent : DomainEvent<ExpenseUpdatedEventData>
{
	public ExpenseUpdatedEvent(IDomainEventDetails eventDetails, ExpenseUpdatedEventData eventData) : base(eventDetails, eventData)
	{

	}

	/// <summary>
	/// Creates a new instance of the ExpenseUpdatedEvent class.
	/// </summary>
	/// <param name="eventDetails">The details of the domain event.</param>
	/// <param name="eventData">The data associated with the expense updated event.</param>
	/// <returns>A new instance of the ExpenseUpdatedEvent class.</returns>
	public static ExpenseUpdatedEvent Create(IDomainEventDetails eventDetails, ExpenseUpdatedEventData eventData)
	{
		return new ExpenseUpdatedEvent(eventDetails, eventData);
	}
}
