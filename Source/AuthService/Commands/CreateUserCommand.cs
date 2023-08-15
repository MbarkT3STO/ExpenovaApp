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

public class MappingProfile : Profile
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


public class CreateUserCommandResult : CommandResult<CreateUserCommandResultValue>
{
	public CreateUserCommandResult(CreateUserCommandResultValue value) : base(value)
	{
	}

	public CreateUserCommandResult(Error error) : base(error)
	{
	}
}

public class CreateUserCommand : IRequest<CreateUserCommandResult>
{
	public string FirstName { get; set; }
	public string LastName { get; set; }
	
	public string UserName { get; set; }
	public string Email { get; set; }
	public string Password { get; set; }
}


public class CreateUserCommandHandler : CommandHandler, IRequestHandler<CreateUserCommand, CreateUserCommandResult>
{
	public CreateUserCommandHandler(AppDbContext context, IMapper mapper, UserManager<AppUser> userManager) : base(context, mapper, userManager)
	{
	}
	
	public async Task<CreateUserCommandResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
	{
		try
		{
			var user = new AppUser
			{
				FirstName = request.FirstName,
				LastName = request.LastName,
				UserName = request.UserName,
				Email = request.Email,
			};
			
			// Create user
			var createUserResult = await _userManager.CreateAsync(user, request.Password);

			if (createUserResult.Succeeded)
			{
				// Add user to default role
				var addUserToRoleResult = await _userManager.AddToRoleAsync(user, "User");
				
				if (addUserToRoleResult.Succeeded)
				{
					var createdUserDTO = _mapper.Map<DTOs.CreatedUserDTO>(user);
					var value = new CreateUserCommandResultValue(createdUserDTO);
					
					return new CreateUserCommandResult(value);
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
		var error = new Error(errorsAsString);
		return error;
	}

}
