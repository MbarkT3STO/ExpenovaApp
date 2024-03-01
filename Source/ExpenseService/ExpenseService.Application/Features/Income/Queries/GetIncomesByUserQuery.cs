namespace ExpenseService.Application.Features.Income.Queries;

public class GetIncomesByUserQueryResultDTO: AuditableDTO
{
	public Guid Id { get; set; }
	public string Description { get; set; }
	public DateTime Date { get; set; }
	public decimal Amount { get; set; }
	public Guid CategoryId { get; set; }
	public string UserId { get; set; }
}

public class GetIncomesByUserQueryResult: QueryResult<IEnumerable<GetIncomesByUserQueryResultDTO>, GetIncomesByUserQueryResult>
{
	public GetIncomesByUserQueryResult(IEnumerable<GetIncomesByUserQueryResultDTO> data): base(data)
	{
	}

	public GetIncomesByUserQueryResult(Error error): base(error)
	{
	}
}

public class GetIncomesByUserQueryMapperProfile: Profile
{
	public GetIncomesByUserQueryMapperProfile()
	{
		CreateMap<Domain.Entities.Income, GetIncomesByUserQueryResultDTO>();
	}
}



/// <summary>
/// Represents a query to retrieve incomes by user.
/// </summary>
public class GetIncomesByUserQuery: IRequest<GetIncomesByUserQueryResult>
{
	public string UserId { get; set; }

	public GetIncomesByUserQuery(string userId)
	{
		UserId = userId;
	}
}


public class GetIncomesByUserQueryHandler: BaseQueryHandler<GetIncomesByUserQuery, GetIncomesByUserQueryResult>
{
	readonly IIncomeRepository _incomeRepository;
	readonly IUserRepository _userRepository;

	public GetIncomesByUserQueryHandler(IMediator mediator, IMapper mapper, IIncomeRepository incomeRepository, IUserRepository userRepository): base(mapper)
	{
		_incomeRepository = incomeRepository;
		_userRepository   = userRepository;
	}


	public override async Task<GetIncomesByUserQueryResult> Handle(GetIncomesByUserQuery request, CancellationToken cancellationToken)
	{
		try
		{
			var user      = await _userRepository.GetByIdOrThrowAsync(request.UserId, cancellationToken);
			var incomes   = await _incomeRepository.GetIncomesByUserAsync(request.UserId, cancellationToken);
			var resultDTO = _mapper.Map<IEnumerable<GetIncomesByUserQueryResultDTO>>(incomes);

			return GetIncomesByUserQueryResult.Succeeded(resultDTO);
		}
		catch (Exception ex)
		{
			return GetIncomesByUserQueryResult.Failed(ex);
		}
	}
}