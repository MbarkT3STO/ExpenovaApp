namespace ExpenseService.Infrastructure.Data.Entities;


/// <summary>
/// Represents an outbox message entity.
/// </summary>
public class OutboxMessage
{
	/// <summary>
	/// Gets or sets the ID of the outbox message.
	/// </summary>
	public int Id { get; set; }

	/// <summary>
	/// Gets or sets the name of the event associated with the outbox message.
	/// </summary>
	public string EventName { get; set; }

	/// <summary>
	/// Gets or sets the data of the outbox message.
	/// </summary>
	public string Data { get; set; }
	
	/// <summary>
	/// Gets or sets the name of the queue.
	/// </summary>
	public string QueueName { get; set; }

	/// <summary>
	/// Gets or sets the creation date and time of the outbox message in UTC.
	/// </summary>
	public DateTime CreatedAt { get; set; }

	/// <summary>
	/// Gets or sets a value indicating whether the outbox message has been processed.
	/// </summary>
	public bool IsProcessed { get; set; }


	public OutboxMessage(string eventName, string data, string queueName)
	{
		EventName   = eventName;
		Data        = data;
		QueueName   = queueName;
		
		CreatedAt   = DateTime.UtcNow;
		IsProcessed = false;
	}
}
