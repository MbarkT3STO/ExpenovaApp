using ExpenseService.Domain.Shared.Interfaces;

namespace ExpenseService.Domain.Events;

/// <summary>
/// Represents the data for a category deleted event.
/// </summary>
/// <summary>
/// Represents the event data for when a category is deleted.
/// </summary>
public class CategoryDeletedEventData
{
	/// <summary>
	/// Gets the unique identifier of the category.
	/// </summary>
	public Guid Id { get; }

	/// <summary>
	/// Gets the name of the category.
	/// </summary>
	public string Name { get; }

	/// <summary>
	/// Gets the description of the category.
	/// </summary>
	public string Description { get; }

	/// <summary>
	/// Gets the user ID associated with the category.
	/// </summary>
	public string UserId { get; }

	/// <summary>
	/// Gets or sets the date and time when the category was created.
	/// </summary>
	public DateTime CreatedAt { get; set; }

	/// <summary>
	/// Gets or sets the user who created the category.
	/// </summary>
	public string CreatedBy { get; set; }

	/// <summary>
	/// Gets or sets the date and time when the category was last updated.
	/// </summary>
	public DateTime UpdatedAt { get; set; }

	/// <summary>
	/// Gets or sets the user who last updated the category.
	/// </summary>
	public string UpdatedBy { get; set; }

	/// <summary>
	/// Gets or sets the date and time when the category was deleted.
	/// </summary>
	public DateTime DeletedAt { get; set; }

	/// <summary>
	/// Gets or sets the user who deleted the category.
	/// </summary>
	public string DeletedBy { get; set; }


	private CategoryDeletedEventData()
	{
	}


	/// <summary>
	/// Initializes a new instance of the <see cref="CategoryDeletedEventData"/> class.
	/// </summary>
	/// <param name="id">The unique identifier of the category.</param>
	/// <param name="name">The name of the category.</param>
	/// <param name="description">The description of the category.</param>
	/// <param name="userId">The user ID associated with the category.</param>
	public CategoryDeletedEventData(Guid id, string name, string description, string userId)
	{
		Id = id;
		Name = name;
		Description = description;
		UserId = userId;
	}
}


/// <summary>
/// Represents an event that is raised when a category is deleted.
/// </summary>
public class CategoryDeletedEvent : DomainEvent<CategoryDeletedEventData>
{
	/// <summary>
	/// Initializes a new instance of the <see cref="CategoryDeletedEvent"/> class.
	/// </summary>
	/// <param name="eventDetails">The details of the domain event.</param>
	/// <param name="eventData">The data associated with the category deletion.</param>
	public CategoryDeletedEvent(IDomainEventDetails eventDetails, CategoryDeletedEventData eventData) : base(eventDetails, eventData)
	{

	}

	/// <summary>
	/// Creates a new instance of the <see cref="CategoryDeletedEvent"/> class.
	/// </summary>
	/// <param name="eventDetails">The details of the domain event.</param>
	/// <param name="eventData">The data associated with the category deletion.</param>
	/// <returns>A new instance of the <see cref="CategoryDeletedEvent"/> class.</returns>
	public static CategoryDeletedEvent Create(IDomainEventDetails eventDetails, CategoryDeletedEventData eventData)
	{
		return new CategoryDeletedEvent(eventDetails, eventData);
	}
}
