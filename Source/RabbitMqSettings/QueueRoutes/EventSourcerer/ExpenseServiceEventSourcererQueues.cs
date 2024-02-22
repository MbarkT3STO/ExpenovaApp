namespace RabbitMqSettings.QueueRoutes.EventSourcerer;

/// <summary>
/// Represents the queue names used by the Event-Sourcerer service for the Expense service.
/// </summary>
public class ExpenseServiceEventSourcererQueues
{
	public const string UserEventSourcererQueue            = "EventSourcererService-ExpenseService-UserEventsQueue";
	public const string CategoryCreatedEventSourcererQueue = "EventSourcererService-ExpenseService-CategoryCreatedEventsQueue";
	public const string CategoryUpdatedEventSourcererQueue = "EventSourcererService-ExpenseService-CategoryUpdatedEventsQueue";
	public const string CategoryDeletedEventSourcererQueue = "EventSourcererService-ExpenseService-CategoryDeletedEventsQueue";

	public const string ExpenseCreatedEventSourcererQueue = "EventSourcererService-ExpenseService-ExpenseCreatedEventsQueue";
	public const string ExpenseUpdatedEventSourcererQueue = "EventSourcererService-ExpenseService-ExpenseUpdatedEventsQueue";
	public const string ExpenseDeletedEventSourcererQueue = "EventSourcererService-ExpenseService-ExpenseDeletedEventsQueue";


	public const string SubscriptionExpenseCreatedEventSourcererQueue = "EventSourcererService-ExpenseService-SubscriptionExpenseCreatedEventsQueue";
	public const string SubscriptionExpenseUpdatedEventSourcererQueue = "EventSourcererService-ExpenseService-SubscriptionExpenseUpdatedEventsQueue";
	public const string SubscriptionExpenseDeletedEventSourcererQueue = "EventSourcererService-ExpenseService-SubscriptionExpenseDeletedEventsQueue";

}
