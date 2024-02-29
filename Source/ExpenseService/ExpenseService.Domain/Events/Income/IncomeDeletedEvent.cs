using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseService.Domain.Events.Income;


public class IncomeDeletedEventData
{
	public Guid Id { get; set; }
	public string Description { get; set; }
	public DateTime Date { get; set; }
	public decimal Amount { get; set; }
	public Guid CategoryId { get; set; }
	public string UserId { get; set; }

	public IncomeDeletedEventData(Guid id, string description, DateTime date, decimal amount, Guid categoryId, string userId)
	{
		Id          = id;
		Description = description;
		Date        = date;
		Amount      = amount;
		CategoryId  = categoryId;
		UserId      = userId;
	}

	private IncomeDeletedEventData()
	{
	}
}


/// <summary>
/// Represents an event that is raised when an income is deleted.
/// </summary>
public class IncomeDeletedEvent: DomainEvent<IncomeDeletedEventData>
{
	public IncomeDeletedEvent(IDomainEventDetails eventDetails, IncomeDeletedEventData eventData): base(eventDetails, eventData)
	{
	}

	public static IncomeDeletedEvent Create(IDomainEventDetails eventDetails, IncomeDeletedEventData eventData)
	{
		return new IncomeDeletedEvent(eventDetails, eventData);
	}
}
