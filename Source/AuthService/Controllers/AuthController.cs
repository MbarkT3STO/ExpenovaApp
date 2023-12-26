
using Microsoft.AspNetCore.Authorization;

namespace AuthService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : BaseExtendedController
{
	public AuthController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
	{
	}


	[HttpPost(nameof(Login))]
	public async Task<IActionResult> Login(LoginCommand command)
	{
		var commandResult = await _mediator.Send(command);

		if(commandResult.IsFailure)
		{
			return BadRequest(commandResult.Error.Message);
		}

		return Ok(commandResult.Value);
	}
}
