namespace RabbitMqSettings.QueueRoutes.EventSourcerer;

/// <summary>
/// Contains the queue names for events related to the AuthService.
/// </summary>
public static class AuthServiceEventSourcererQueues
{
    public const string UserEventSourcererQueue = "EventSourcererService-AuthService-UserEventsQueue";
}
