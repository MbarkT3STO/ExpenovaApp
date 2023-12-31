namespace RabbitMqSettings.QueueRoutes.EventSourcerer;

public class ExpenseServiceEventSourcererQueues
{
	public const string UserEventSourcererQueue            = "EventSourcererService-ExpenseService-UserEventsQueue";
	public const string CategoryCreatedEventSourcererQueue = "EventSourcererService-ExpenseService-CategoryCreatedEventsQueue";
	public const string CategoryUpdatedEventSourcererQueue = "EventSourcererService-ExpenseService-CategoryUpdatedEventsQueue";
	public const string CategoryDeletedEventSourcererQueue = "EventSourcererService-ExpenseService-CategoryDeletedEventsQueue";

	public const string ExpenseCreatedEventSourcererQueue  = "EventSourcererService-ExpenseService-ExpenseCreatedEventsQueue";

}
