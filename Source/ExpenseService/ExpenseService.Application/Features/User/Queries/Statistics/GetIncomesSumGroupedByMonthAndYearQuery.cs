using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseService.Application.Features.User.Queries.Statistics;

public class GetIncomesSumGroupedByMonthAndYearQueryResultDTO
{
	public int Year { get; set; }
	public int Month { get; set; }
	public decimal Sum { get; set; }
}

public class GetIncomesSumGroupedByMonthAndYearQueryResult: QueryResult<IEnumerable<GetIncomesSumGroupedByMonthAndYearQueryResultDTO>, GetIncomesSumGroupedByMonthAndYearQueryResult>
{
	public GetIncomesSumGroupedByMonthAndYearQueryResult(IEnumerable<GetIncomesSumGroupedByMonthAndYearQueryResultDTO> value): base(value)
	{
	}

	public GetIncomesSumGroupedByMonthAndYearQueryResult(Error error): base(error)
	{
	}
}

public class GetIncomesSumGroupedByMonthAndYearQueryMapper: Profile
{
	public GetIncomesSumGroupedByMonthAndYearQueryMapper()
	{
		CreateMap<(int Month, int Year, decimal Sum), GetIncomesSumGroupedByMonthAndYearQueryResultDTO>()
																		 .ForMember(dest => dest.Month, opt => opt.MapFrom(src => src.Month))
																		 .ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.Year))
																		 .ForMember(dest => dest.Sum, opt => opt.MapFrom(src => src.Sum));
	}
}



/// <summary>
/// Represents a query to get the sum of incomes grouped by month and year for a specific user.
/// </summary>
public class GetIncomesSumGroupedByMonthAndYearQuery: IRequest<GetIncomesSumGroupedByMonthAndYearQueryResult>
{
	public string UserId { get; private set; }

	public GetIncomesSumGroupedByMonthAndYearQuery(string userId)
	{
		UserId = userId;
	}
}


public class GetIncomesSumGroupedByMonthAndYearQueryHandler: BaseQueryHandler<GetIncomesSumGroupedByMonthAndYearQuery, GetIncomesSumGroupedByMonthAndYearQueryResult>
{
	readonly IIncomeRepository _incomeRepository;
	readonly IUserRepository _userRepository;

	public GetIncomesSumGroupedByMonthAndYearQueryHandler(IMapper mapper, IIncomeRepository incomeRepository, IUserRepository userRepository): base(mapper)
	{
		_incomeRepository = incomeRepository;
		_userRepository   = userRepository;
	}

	public override async Task<GetIncomesSumGroupedByMonthAndYearQueryResult> Handle(GetIncomesSumGroupedByMonthAndYearQuery request, CancellationToken cancellationToken)
	{
		try
		{
			var user    = await _userRepository.GetByIdOrThrowAsync(request.UserId, cancellationToken);
			var incomes = await _incomeRepository.GetIncomesByUserAsync(request.UserId, cancellationToken);

			var groupedIncomes = await _incomeRepository.GetSumGroupedByMonthAndYearAsync(request.UserId, cancellationToken);

			var resultDTOs = _mapper.Map<IEnumerable<GetIncomesSumGroupedByMonthAndYearQueryResultDTO>>(groupedIncomes);

			return GetIncomesSumGroupedByMonthAndYearQueryResult.Succeeded(resultDTOs);
		}
		catch (Exception ex)
		{
			return GetIncomesSumGroupedByMonthAndYearQueryResult.Failed(ex);
		}
	}
}