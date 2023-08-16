
namespace AuthService.Common;

/// <summary>
/// Base class for command handlers.
/// </summary>
public abstract class CommandHandler
{
	protected readonly AppDbContext _context;
	protected readonly IMapper _mapper;
	protected readonly UserManager<AppUser> _userManager;
	protected readonly IBus _bus;
	
	protected CommandHandler(AppDbContext context)
	{
		_context = context;
	}

	protected CommandHandler(UserManager<AppUser> userManager)
	{
		_userManager = userManager;
	}

	protected CommandHandler(AppDbContext context, IMapper mapper)
	{
		_context = context;
		_mapper = mapper;
	}
	
	protected CommandHandler(AppDbContext context, IBus bus)
	{
		_context = context;
		_bus = bus;
	}

	protected CommandHandler(AppDbContext context, IMapper mapper, IBus bus)
	{
		_context = context;
		_mapper = mapper;
		_bus = bus;
	}


	protected CommandHandler(AppDbContext context, IMapper mapper, UserManager<AppUser> userManager)
	{
		_context = context;
		_mapper = mapper;
		_userManager = userManager;
	}
	
	protected CommandHandler(AppDbContext context, IMapper mapper, UserManager<AppUser> userManager, IBus bus)
	{
		_context = context;
		_mapper = mapper;
		_userManager = userManager;
		_bus = bus;
	}

}
