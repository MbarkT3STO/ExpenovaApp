using ExpenseService.Domain.Shared.Interfaces;

namespace ExpenseService.Domain.Events;

/// <summary>
/// Represents the data associated with a category updated event.
/// </summary>
public class CategoryUpdatedEventData
{
	public Guid Id { get; }
	public string NewName { get; }
	public string NewDescription { get; }
	public string UserId { get; }
	
	private CategoryUpdatedEventData()
	{
	}
	
	public CategoryUpdatedEventData(Guid id, string newName, string newDescription, string userId)
	{
		Id             = id;
		NewName        = newName;
		NewDescription = newDescription;
		UserId         = userId;
	}
}


/// <summary>
/// Represents an event that is raised when a category is updated.
/// </summary>
public class CategoryUpdatedEvent : DomainEvent<CategoryUpdatedEventData>
{
	public CategoryUpdatedEvent(IDomainEventDetails eventDetails, CategoryUpdatedEventData eventData) : base(eventDetails, eventData)
	{

	}

	/// <summary>
	/// Creates a new instance of the CategoryUpdatedEvent.
	/// </summary>
	/// <param name="eventDetails">The domain event details.</param>
	/// <param name="eventData">The event data.</param>
	/// <returns>A new instance of the CategoryUpdatedEvent.</returns>
	public static CategoryUpdatedEvent Create(IDomainEventDetails eventDetails, CategoryUpdatedEventData eventData)
	{
		return new CategoryUpdatedEvent(eventDetails, eventData);
	}
}