using DTOs = AuthService.Queries.GetUsersQueryResultDTOs;

namespace AuthService.Queries;

public class GetUsersQueryResultDTOs
{
	public class UserDTO
	{
		public string Id { get; set; }
		public string UserName { get; set; }
		public string Email { get; set; }
	}
}

public class MappingProfile: Profile
{
	public MappingProfile()
	{
		CreateMap<AppUser, DTOs.UserDTO>();
	}
}

public class GetUsersQueryValue
{
	public IReadOnlyCollection<DTOs.UserDTO> Users { get; set; }
	
	public GetUsersQueryValue(IReadOnlyCollection<DTOs.UserDTO> users)
	{
		Users = users;
	}
}


public class GetUsersQueryResult: QueryResult<GetUsersQueryValue>
{
	public GetUsersQueryResult(GetUsersQueryValue value): base(value)
	{
	}

	public GetUsersQueryResult(Error error): base(error)
	{
	}
}


public class GetUsersQuery: IRequest<GetUsersQueryResult>
{
	
}


public class GetUsersQueryHandler: QueryHandler, IRequestHandler<GetUsersQuery, GetUsersQueryResult>
{
	public GetUsersQueryHandler(AppDbContext context, IMapper mapper): base(context, mapper)
	{
	}
	
	public async Task<GetUsersQueryResult> Handle(GetUsersQuery request, CancellationToken cancellationToken)
	{
		try
		{
			var users            = await _context.Users.ToListAsync(cancellationToken);
			var usersDTO         = _mapper.Map<IReadOnlyCollection<DTOs.UserDTO>>(users);
			var queryResultValue = new GetUsersQueryValue(usersDTO);
			
			return new GetUsersQueryResult(queryResultValue);
		}
		catch (Exception e)
		{
			return new GetUsersQueryResult(Error.FromException(e));
		}
	}
}
	