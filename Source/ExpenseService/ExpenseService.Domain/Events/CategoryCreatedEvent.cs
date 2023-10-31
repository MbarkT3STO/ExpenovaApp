using ExpenseService.Domain.Shared.Interfaces;
using System;

namespace ExpenseService.Domain.Events;

/// <summary>
/// Represents the data associated with a category created event.
/// </summary>
public record CategoryCreatedEventData
{
	public Guid Id { get; }
	public string Name { get; }
	public string Description { get; }
	public string UserId { get; }
	
	private CategoryCreatedEventData()
	{
	}
	
	public CategoryCreatedEventData(Guid id, string name, string description, string userId)
	{
		Id          = id;
		Name        = name;
		Description = description;
		UserId      = userId;
	}
}

public class CategoryCreatedEvent: IDomainEvent<CategoryCreatedEventData>
{
	public IDomainEventDetails EventDetails { get; set; }

	public CategoryCreatedEventData EventData { get; set; }
	
	private CategoryCreatedEvent()
	{
	}

	public CategoryCreatedEvent(IDomainEventDetails eventDetails, CategoryCreatedEventData eventData)
	{
		EventDetails = eventDetails;
		EventData    = eventData;
	}
	
	/// <summary>
	/// Creates a new instance of the CategoryCreatedEvent.
	/// </summary>
	/// <param name="eventDetails">The domain event details.</param>
	/// <param name="eventData">The event data.</param>
	/// <returns>A new instance of the CategoryCreatedEvent.</returns>
	public static CategoryCreatedEvent Create(IDomainEventDetails eventDetails, CategoryCreatedEventData eventData)
	{
		return new CategoryCreatedEvent(eventDetails, eventData);
	}
}
