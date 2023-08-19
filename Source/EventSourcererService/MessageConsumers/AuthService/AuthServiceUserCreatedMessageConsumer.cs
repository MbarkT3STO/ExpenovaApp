using MassTransit;

namespace EventSourcererService.MessageConsumers.AuthService;

public class AuthServiceUserCreatedMessageConsumer: BaseConsumer, IConsumer<UserCreatedMessage>
{
	public AuthServiceUserCreatedMessageConsumer(AppDbContext dbContext): base(dbContext)
	{
	}

	public async Task Consume(ConsumeContext<UserCreatedMessage> context)
	{
		var message = context.Message;
		var eventData = new ExpenseServiceUserEventJsonData(
				message.UserId,
				message.FirstName,
				message.LastName,
				message.Username,
				message.Email,
				message.RoleId,
				message.CreatedAt
			);

		var userEvent = new ExpenseServiceUserEvent("Create", DateTime.UtcNow, message.UserId, eventData);

		
		await _dbContext.ExpenseService_UserEvents.AddAsync(userEvent);
		await _dbContext.SaveChangesAsync();
	}
}
