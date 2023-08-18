using Messages.AuthServiceMessages;
using Microsoft.Extensions.Options;
using RabbitMqSettings;

namespace AuthService.Events;

/// <summary>
/// Represents an event that is raised when a new user is created.
/// </summary>
public class UserCreatedEvent : INotification
{
	public string UserId { get; set; }
	public string FirstName { get; set; }
	public string LastName { get; set; }
	public string UserName { get; set; }
	public string Email { get; set; }
	public string RoleId { get; set; }
}


public class UserCreatedEventHandler : INotificationHandler<UserCreatedEvent>
{
	readonly IBus _bus;
	readonly RabbitMqOptions _rabbitMqOptions;
	readonly AuthServiceRabbitMqEndpointsOptions _authServiceRabbitMqEndPointsOptions;
	
	public UserCreatedEventHandler(IBus bus, IOptions<RabbitMqOptions> rabbitMqOptions, IOptions<AuthServiceRabbitMqEndpointsOptions> authServiceRabbitMqEndPointsOptions)
	{
		_bus = bus;
		_rabbitMqOptions = rabbitMqOptions.Value;
		_authServiceRabbitMqEndPointsOptions = authServiceRabbitMqEndPointsOptions.Value;
	}
	
	public async Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
	{
		var message = new UserCreatedMessage
		{
			UserId = notification.UserId,
			FirstName = notification.FirstName,
			LastName = notification.LastName,
			Username = notification.UserName,
			Email = notification.Email,
			Role = notification.RoleId
		};

		var queueName = _rabbitMqOptions.HostName + "/" + _authServiceRabbitMqEndPointsOptions.UserCreatedEventQueue;
		var queueToPublishTo = new Uri(queueName);
		var endPoint = await _bus.GetSendEndpoint(queueToPublishTo);

		await endPoint.Send(message, cancellationToken);
	}
}