using ExpenseService.Domain.Shared.Interfaces;
using System;

namespace ExpenseService.Domain.Events
{
	/// <summary>
	/// Represents the details of a domain event.
	/// </summary>
	public class DomainEventDetails : IDomainEventDetails
	{
		public Guid EventId { get; private set; }
		public string EventName { get; private set; }
		public DateTime OccurredOn { get; private set; }
		public string OccurredBy { get; private set; }
		
		private DomainEventDetails()
		{
		}
		
		public DomainEventDetails(string eventName, string occurredBy)
		{
			EventId    = Guid.NewGuid();
			EventName  = eventName;
			OccurredOn = DateTime.UtcNow;
			OccurredBy = occurredBy;
		}
		public DomainEventDetails(string eventName, DateTime occurredOn, string occurredBy)
		{
			EventId    = Guid.NewGuid();
			EventName  = eventName;
			OccurredOn = occurredOn;
			OccurredBy = occurredBy;
		}
		
		/// <summary>
		/// Creates a new instance of the DomainEventDetails.
		/// </summary>
		/// <param name="eventName">The event name.</param>
		/// <param name="occurredBy">The user who caused the domain event to occur.</param>
		/// <returns>A new instance of the DomainEventDetails.</returns>
		/// <exception cref="ArgumentNullException">Thrown when the eventName or occurredBy parameters are null or empty.</exception>
		/// <exception cref="ArgumentException">Thrown when the eventName or occurredBy parameters are whitespace.</exception>
		public static DomainEventDetails Create(string eventName, string occurredBy)
		{
			if (string.IsNullOrWhiteSpace(eventName))
			{
				throw new ArgumentNullException(nameof(eventName), "The event name cannot be null or empty.");
			}
			
			if (string.IsNullOrWhiteSpace(occurredBy))
			{
				throw new ArgumentNullException(nameof(occurredBy), "The occurred by user cannot be null or empty.");
			}
			
			return new DomainEventDetails(eventName, occurredBy);
		}
		
		/// <summary>
		/// Creates a new instance of the DomainEventDetails.
		/// </summary>
		/// <param name="eventName">The event name.</param>
		/// <param name="occurredOn">The date and time the event occurred.</param>
		/// <param name="occurredBy">The user who caused the domain event to occur.</param>
		/// <returns>A new instance of the DomainEventDetails.</returns>
		/// <exception cref="ArgumentNullException">Thrown when the eventName or occurredBy parameters are null or empty.</exception>
		/// <exception cref="ArgumentException">Thrown when the eventName or occurredBy parameters are whitespace.</exception>
		public static DomainEventDetails Create(string eventName, DateTime occurredOn, string occurredBy)
		{
			if (string.IsNullOrWhiteSpace(eventName))
			{
				throw new ArgumentNullException(nameof(eventName), "The event name cannot be null or empty.");
			}
			
			if (string.IsNullOrWhiteSpace(occurredBy))
			{
				throw new ArgumentNullException(nameof(occurredBy), "The occurred by user cannot be null or empty.");
			}
			
			return new DomainEventDetails(eventName, occurredOn, occurredBy);
		}
	}
}
