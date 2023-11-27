

namespace Messages.Abstractions;

/// <summary>
/// Represents a base event message.
/// </summary>
public abstract class BaseEventMessage : IEventMessage
{
	/// <summary>
	/// Gets the unique identifier of the event.
	/// </summary>
	public required Guid EventId { get; set; }
}
