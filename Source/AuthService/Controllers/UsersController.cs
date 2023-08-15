using static AuthService.Queries.GetUsersQueryResultDTOs;

namespace AuthService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : BaseExtendedController
{
	public UsersController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
	{
	}
	
	[HttpGet]
	public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers()
	{
		var queryResult = await _mediator.Send(new GetUsersQuery());
		
		if(queryResult.IsSuccess)
			return Ok(queryResult.Value.Users);
			
		return BadRequest(queryResult.Error.Message);
	}
	
	[HttpPost]
	public async Task<ActionResult<UserDTO>> CreateUser(CreateUserCommand command)
	{
		var queryResult = await _mediator.Send(command);
		
		if(queryResult.IsSuccess)
			return Ok(queryResult.Value);
			
		return BadRequest(queryResult.Error.Message);
	}
}
