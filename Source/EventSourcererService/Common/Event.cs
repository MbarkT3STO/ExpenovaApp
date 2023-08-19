namespace EventSourcererService.Common;

/// <summary>
/// Represents an event in the system.
/// </summary>
/// <typeparam name="T">The type of data associated with the event.</typeparam>
public abstract class Event<T> where T : class
{
	public int Id { get; set; }
	public string Type { get; set; }
	public DateTime TimeStamp { get; set; }
	public string UserId { get; set; }
	public T JsonData { get; set; }
	
	protected Event( string type, DateTime timeStamp, string userId, T jsonData)
	{
		Type      = type;
		TimeStamp = timeStamp;
		UserId    = userId;
		JsonData  = jsonData;
	}
}
