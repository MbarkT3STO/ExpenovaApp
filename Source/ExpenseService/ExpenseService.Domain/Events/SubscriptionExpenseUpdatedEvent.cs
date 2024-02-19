using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseService.Domain.Events;

public class SubscriptionExpenseUpdatedEventData
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


	private SubscriptionExpenseUpdatedEventData()
	{
	}


	public SubscriptionExpenseUpdatedEventData(Guid id, string description, decimal amount, string userId, Guid categoryId, DateTime startDate, DateTime endDate, RecurrenceInterval recurrenceInterval, decimal billingAmount, DateTime createdAt, string createdBy, DateTime? lastUpdatedAt, string lastUpdatedBy, bool isDeleted, DateTime? deletedAt)
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
	}
}



/// <summary>
/// Represents an event that is raised when a subscription expense is updated.
/// </summary>
public class SubscriptionExpenseUpdatedEvent : DomainEvent<SubscriptionExpenseUpdatedEventData>
{
	public SubscriptionExpenseUpdatedEvent(IDomainEventDetails eventDetails, SubscriptionExpenseUpdatedEventData eventData) : base(eventDetails, eventData)
	{

	}

	/// <summary>
	/// Creates a new instance of the SubscriptionExpenseUpdatedEvent class.
	/// </summary>
	/// <param name="eventDetails">The details of the domain event.</param>
	/// <param name="eventData">The data associated with the subscription expense updated event.</param>
	/// <returns>A new instance of the SubscriptionExpenseUpdatedEvent class.</returns>
	public static SubscriptionExpenseUpdatedEvent Create(IDomainEventDetails eventDetails, SubscriptionExpenseUpdatedEventData eventData)
	{
		return new SubscriptionExpenseUpdatedEvent(eventDetails, eventData);
	}
}
