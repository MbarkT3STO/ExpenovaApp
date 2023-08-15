
namespace AuthService.Common;

/// <summary>
/// Base controller class that provides extended functionality for controllers.
/// </summary>
public abstract class BaseExtendedController : ControllerBase
{
	protected readonly IMediator _mediator;
	protected readonly IMapper _mapper;
	
	protected BaseExtendedController(IMediator mediator, IMapper mapper)
	{
		_mediator = mediator;
		_mapper = mapper;
	}
}
