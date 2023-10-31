using ExpenseService.Domain.Shared.Interfaces;

namespace ExpenseService.Domain.Events
{
	/// <summary>
	/// Represents a base class for domain events.
	/// </summary>
	/// <typeparam name="T">The type of event data.</typeparam>
	public abstract class DomainEvent<T> : IDomainEvent<T>
	{
		public IDomainEventDetails EventDetails { get; set; }
		public T EventData { get; set; }
		
		
		private DomainEvent()
		{
		}
		
		/// <summary>
		/// Initializes a new instance of the <see cref="DomainEvent{T}"/> class.
		/// </summary>
		/// <param name="eventDetails">The details of the domain event.</param>
		/// <param name="eventData">The data associated with the domain event.</param>
		protected DomainEvent(IDomainEventDetails eventDetails, T eventData)
		{
			EventDetails = eventDetails;
			EventData    = eventData;
		}

	}
}
