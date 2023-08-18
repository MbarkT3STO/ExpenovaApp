using ExpenseService.Domain.Entities;
using MassTransit;
using Messages.AuthServiceMessages;

namespace ExpenseService.Application.MessageConsumers;

public class UserCreatedMessageConsumer : IConsumer<UserCreatedMessage>
{
	readonly IUserRepository _userRepository;
	
	// public UserCreatedMessageConsumer()
	// {
	// }
	
	public UserCreatedMessageConsumer(IUserRepository userRepository)
	{
		_userRepository = userRepository;
	}
	
	public async Task Consume(ConsumeContext<UserCreatedMessage> context)
	{
		var message = context.Message;
		
		var user = User.Create(message.UserId, message.FirstName, message.LastName);
		
		await _userRepository.AddAsync(user);
	}
}
