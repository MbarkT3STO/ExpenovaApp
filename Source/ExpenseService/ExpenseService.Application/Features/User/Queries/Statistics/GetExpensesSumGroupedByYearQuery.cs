using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseService.Application.Features.User.Queries.Statistics;

public class GetExpensesSumGroupedByYearQueryResultDTO
{
	public int Year { get; set; }
	public decimal Sum { get; set; }
}

/// <summary>
/// Represents the result of the GetExpensesSumGroupedByYearQuery.
/// </summary>
public class GetExpensesSumGroupedByYearQueryResult: QueryResult<IEnumerable<GetExpensesSumGroupedByYearQueryResultDTO>, GetExpensesSumGroupedByYearQueryResult>
{
	public GetExpensesSumGroupedByYearQueryResult(IEnumerable<GetExpensesSumGroupedByYearQueryResultDTO> value): base(value)
	{
	}

	public GetExpensesSumGroupedByYearQueryResult(Error error): base(error)
	{
	}
}

public class GetExpensesSumGroupedByYearQueryMapper: Profile
{
	public GetExpensesSumGroupedByYearQueryMapper()
	{
		CreateMap<(int Year, decimal Sum), GetExpensesSumGroupedByYearQueryResultDTO>()
																	   .ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.Year))
																	   .ForMember(dest => dest.Sum, opt => opt.MapFrom(src => src.Sum));
	}
}



/// <summary>
/// Represents a query to get the sum of expenses grouped by year for a specific user.
/// </summary>
public class GetExpensesSumGroupedByYearQuery: IRequest<GetExpensesSumGroupedByYearQueryResult>
{
	public string UserId { get; private set; }

	public GetExpensesSumGroupedByYearQuery(string userId)
	{
		UserId = userId;
	}
}


public class GetExpensesSumGroupedByYearQueryHandler: BaseQueryHandler<GetExpensesSumGroupedByYearQuery, GetExpensesSumGroupedByYearQueryResult>
{
	readonly IUserRepository _userRepository;
	readonly IExpenseRepository _expenseRepository;

	public GetExpensesSumGroupedByYearQueryHandler(IMapper mapper, IUserRepository userRepository, IExpenseRepository expenseRepository): base(mapper)
	{
		_userRepository    = userRepository;
		_expenseRepository = expenseRepository;
	}


	public override async Task<GetExpensesSumGroupedByYearQueryResult> Handle(GetExpensesSumGroupedByYearQuery request, CancellationToken cancellationToken)
	{
		try
		{
			var user     = await _userRepository.GetByIdOrThrowAsync(request.UserId, cancellationToken);
			var expenses = await _expenseRepository.GetSumGroupedByYearAsync(request.UserId, cancellationToken);

			var resultDTOs = _mapper.Map<IEnumerable<GetExpensesSumGroupedByYearQueryResultDTO>>(expenses);
			var result     = GetExpensesSumGroupedByYearQueryResult.Succeeded(resultDTOs);

			return result;
		}
		catch (Exception ex)
		{
			return GetExpensesSumGroupedByYearQueryResult.Failed(ex);
		}
	}
}