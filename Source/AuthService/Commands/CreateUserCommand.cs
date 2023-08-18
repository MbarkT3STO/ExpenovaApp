using AuthService.Events;
using MassTransit.Configuration;
using Messages.AuthServiceMessages;
using Microsoft.Extensions.Options;
using RabbitMqSettings;
using DTOs = AuthService.Commands.CreateUserCommandResultDTOs;

namespace AuthService.Commands;

public class CreateUserCommandResultDTOs
{
	public class CreatedUserDTO
	{
		public string Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
	}
}

public class MappingProfile: Profile
{
	public MappingProfile()
	{
		CreateMap<AppUser, DTOs.CreatedUserDTO>();
	}
}

public class CreateUserCommandResultValue
{
	public DTOs.CreatedUserDTO CreatedUser { get; set; }
	
	public CreateUserCommandResultValue( DTOs.CreatedUserDTO createdUser)
	{
		CreatedUser = createdUser;
	}
}


public class CreateUserCommandResult: CommandResult<CreateUserCommandResultValue>
{
	public CreateUserCommandResult(CreateUserCommandResultValue value): base(value)
	{
	}

	public CreateUserCommandResult(Error error): base(error)
	{
	}
}

public class CreateUserCommand: IRequest<CreateUserCommandResult>
{
	public string FirstName { get; set; }
	public string LastName { get; set; }
	
	public string UserName { get; set; }
	public string Email { get; set; }
	public string Password { get; set; }
}


public class CreateUserCommandHandler: CommandHandler, IRequestHandler<CreateUserCommand, CreateUserCommandResult>
{
	readonly RabbitMqOptions _rabbitMqOptions;
	readonly AuthServiceRabbitMqEndpointsOptions _authServiceRabbitMqEndPointsOptions;
	public CreateUserCommandHandler(AppDbContext context, IMapper mapper, UserManager<AppUser> userManager, IMediator mediator): base(context, mapper, mediator, userManager)
	{
	}

	public async Task<CreateUserCommandResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
	{
		try
		{
			var user = new AppUser
			{
				FirstName = request.FirstName,
				LastName  = request.LastName,
				UserName  = request.UserName,
				Email     = request.Email,
			};
			
			// Create user
			var createUserResult = await _userManager.CreateAsync(user, request.Password);

			if (createUserResult.Succeeded)
			{
				// Add user to default role
				var addUserToRoleResult = await _userManager.AddToRoleAsync(user, "User");
				
				if (addUserToRoleResult.Succeeded)
				{
					// Publish UserCreatedEvent
					await PublishUserCreatedEvent(user, cancellationToken);
					
					var createdUserDTO     = _mapper.Map<DTOs.CreatedUserDTO>(user);
					var commandResultValue = new CreateUserCommandResultValue(createdUserDTO);
					
					return new CreateUserCommandResult(commandResultValue);
				}
				else
				{
					var error = GetErrorFromIdentityResult(addUserToRoleResult);
					return new CreateUserCommandResult(error);
				}
			}
			else
			{
				var error = GetErrorFromIdentityResult(createUserResult);
				return new CreateUserCommandResult(error);
			}
		}
		catch (Exception ex)
		{
			var error = new Error(ex.Message);
			return new CreateUserCommandResult(error);
		}
	}
	
	
	/// <summary>
	/// Gets an Error object from an IdentityResult object.
	/// </summary>
	/// <param name="identityResult">The IdentityResult object to get the error from.</param>
	/// <returns>An Error object containing the error message.</returns>
	private static Error GetErrorFromIdentityResult(IdentityResult identityResult)
	{
		var errorsAsString = identityResult.Errors.Aggregate("", (current, error) => current + error.Description + "\n");
		var error          = new Error(errorsAsString);
		return error;
	}
	

	/// <summary>
	/// Publishes a UserCreatedEvent with the given user and user role information.
	/// </summary>
	/// <param name="user">The user that was created.</param>
	/// <param name="userRole">The role of the user that was created.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	private async Task PublishUserCreatedEvent(AppUser user, CancellationToken cancellationToken)
	{
		var userRole = await _context.UserRoles.FirstOrDefaultAsync(ur => ur.UserId == user.Id);

		var userCreatedEvent = new UserCreatedEvent
		{
			UserId    = user.Id,
			FirstName = user.FirstName,
			LastName  = user.LastName,
			UserName  = user.UserName,
			Email     = user.Email,
			RoleId    = userRole.RoleId
		};

		await _mediator.Publish(userCreatedEvent, cancellationToken);
	}
}
