using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseService.Application.Features.User.Queries.Statistics;

public class GetIncomesSumGroupedByCategoryQueryResultDTO
{
	public string Category { get; set; }
	public decimal Sum { get; set; }
}

public class GetIncomesSumGroupedByCategoryQueryResult: QueryResult<IEnumerable<GetIncomesSumGroupedByCategoryQueryResultDTO>, GetIncomesSumGroupedByCategoryQueryResult>
{
	public GetIncomesSumGroupedByCategoryQueryResult(IEnumerable<GetIncomesSumGroupedByCategoryQueryResultDTO> value): base(value)
	{
	}

	public GetIncomesSumGroupedByCategoryQueryResult(Error error): base(error)
	{
	}
}

public class GetIncomesSumGroupedByCategoryQueryMapper: Profile
{
	public GetIncomesSumGroupedByCategoryQueryMapper()
	{
		CreateMap<(string Category, decimal Sum), GetIncomesSumGroupedByCategoryQueryResultDTO>()
																		 .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
																		 .ForMember(dest => dest.Sum, opt => opt.MapFrom(src => src.Sum));
	}
}




/// <summary>
/// Represents a query to get the sum of incomes grouped by category for a specific user.
/// </summary>
public class GetIncomesSumGroupedByCategoryQuery: IRequest<GetIncomesSumGroupedByCategoryQueryResult>
{
	public string UserId { get; private set; }

	public GetIncomesSumGroupedByCategoryQuery(string userId)
	{
		UserId = userId;
	}
}


public class GetIncomesSumGroupedByCategoryQueryHandler: BaseQueryHandler<GetIncomesSumGroupedByCategoryQuery, GetIncomesSumGroupedByCategoryQueryResult>
{
	readonly IIncomeRepository _incomeRepository;
	readonly IUserRepository _userRepository;

	public GetIncomesSumGroupedByCategoryQueryHandler(IMapper mapper, IIncomeRepository incomeRepository, IUserRepository userRepository): base(mapper)
	{
		_incomeRepository = incomeRepository;
		_userRepository   = userRepository;
	}


	public override async Task<GetIncomesSumGroupedByCategoryQueryResult> Handle(GetIncomesSumGroupedByCategoryQuery request, CancellationToken cancellationToken)
	{
		try
		{
			var user        = await _userRepository.GetByIdOrThrowAsync(request.UserId, cancellationToken);
			var groupedData = await _incomeRepository.GetSumGroupedByCategoryAsync(request.UserId, cancellationToken);

			var resultDTOs  = _mapper.Map<IEnumerable<GetIncomesSumGroupedByCategoryQueryResultDTO>>(groupedData);

			return GetIncomesSumGroupedByCategoryQueryResult.Succeeded(resultDTOs);
		}
		catch (Exception ex)
		{
			return GetIncomesSumGroupedByCategoryQueryResult.Failed(ex);
		}
	}
}