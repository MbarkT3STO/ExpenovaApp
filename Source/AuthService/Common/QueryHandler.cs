namespace AuthService.Common;

/// <summary>
/// Base class for query handlers. Provides access to the application database context, object mapper, and user manager.
/// </summary>
public abstract class QueryHandler
{
	protected readonly AppDbContext _context;
	protected readonly IMapper _mapper;
	protected readonly UserManager<AppUser> _userManager;

	protected QueryHandler(AppDbContext context)
	{
		_context = context;
	}

	protected QueryHandler(AppDbContext context, IMapper mapper)
	{
		_context = context;
		_mapper = mapper;
	}

	protected QueryHandler(AppDbContext context, UserManager<AppUser> userManager)
	{
		_context = context;
		_userManager = userManager;
	}

	protected QueryHandler(AppDbContext context, IMapper mapper, UserManager<AppUser> userManager)
	{
		_context = context;
		_mapper = mapper;
		_userManager = userManager;
	}

}
