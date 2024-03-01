using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseService.Application.Features.User.Queries.Statistics;

public class GetIncomesSumGroupedByYearQueryResultDTO
{
	public int Year { get; set; }
	public decimal Sum { get; set; }
}

public class GetIncomesSumGroupedByYearQueryResult: QueryResult<IEnumerable<GetIncomesSumGroupedByYearQueryResultDTO>, GetIncomesSumGroupedByYearQueryResult>
{
	public GetIncomesSumGroupedByYearQueryResult(IEnumerable<GetIncomesSumGroupedByYearQueryResultDTO> value): base(value)
	{
	}

	public GetIncomesSumGroupedByYearQueryResult(Error error): base(error)
	{
	}
}

public class GetIncomesSumGroupedByYearQueryMapper: Profile
{
	public GetIncomesSumGroupedByYearQueryMapper()
	{
		CreateMap<(int Year, decimal Sum), GetIncomesSumGroupedByYearQueryResultDTO>()
																		 .ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.Year))
																		 .ForMember(dest => dest.Sum, opt => opt.MapFrom(src => src.Sum));
	}
}



/// <summary>
/// Represents a query to get the sum of incomes grouped by year for a specific user.
/// </summary>
public class GetIncomesSumGroupedByYearQuery: IRequest<GetIncomesSumGroupedByYearQueryResult>
{
	public string UserId { get; private set; }

	public GetIncomesSumGroupedByYearQuery(string userId)
	{
		UserId = userId;
	}
}


public class GetIncomesSumGroupedByYearQueryHandler: BaseQueryHandler<GetIncomesSumGroupedByYearQuery, GetIncomesSumGroupedByYearQueryResult>
{
	readonly IIncomeRepository _incomeRepository;
	readonly IUserRepository _userRepository;

	public GetIncomesSumGroupedByYearQueryHandler(IMapper mapper, IIncomeRepository incomeRepository, IUserRepository userRepository): base(mapper)
	{
		_incomeRepository = incomeRepository;
		_userRepository   = userRepository;
	}

	public override async Task<GetIncomesSumGroupedByYearQueryResult> Handle(GetIncomesSumGroupedByYearQuery request, CancellationToken cancellationToken)
	{
		try
		{
			var user = await _userRepository.GetByIdOrThrowAsync(request.UserId, cancellationToken);
			var groupedData = await _incomeRepository.GetSumGroupedByYearAsync(request.UserId, cancellationToken);
			var resultDTOs = _mapper.Map<IEnumerable<GetIncomesSumGroupedByYearQueryResultDTO>>(groupedData);


			return GetIncomesSumGroupedByYearQueryResult.Succeeded(resultDTOs);
		}
		catch (Exception ex)
		{
			return GetIncomesSumGroupedByYearQueryResult.Failed(ex);
		}
	}
}