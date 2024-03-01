using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseService.Application.Features.User.Queries.Statistics;

public class GetTopIncomeCategoryQueryResultDTO
{
	public string Category { get; set; }
	public decimal Sum { get; set; }
}

public class GetTopIncomeCategoryQueryResult: QueryResult<GetTopIncomeCategoryQueryResultDTO, GetTopIncomeCategoryQueryResult>
{
	public GetTopIncomeCategoryQueryResult(GetTopIncomeCategoryQueryResultDTO value): base(value)
	{
	}

	public GetTopIncomeCategoryQueryResult(Error error): base(error)
	{
	}
}

public class GetTopIncomeCategoryQueryMapper: Profile
{
	public GetTopIncomeCategoryQueryMapper()
	{
		CreateMap<(string Category, decimal Sum), GetTopIncomeCategoryQueryResultDTO>()
																		 .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
																		 .ForMember(dest => dest.Sum, opt => opt.MapFrom(src => src.Sum));
	}
}




/// <summary>
/// Represents a query to get the top income category for a user.
/// </summary>
public class GetTopIncomeCategoryQuery: IRequest<GetTopIncomeCategoryQueryResult>
{
	public string UserId { get; private set; }

	public GetTopIncomeCategoryQuery(string userId)
	{
		UserId = userId;
	}
}


public class GetTopIncomeCategoryQueryHandler: BaseQueryHandler<GetTopIncomeCategoryQuery, GetTopIncomeCategoryQueryResult>
{
	readonly IIncomeRepository _incomeRepository;
	readonly IUserRepository _userRepository;

	public GetTopIncomeCategoryQueryHandler(IMapper mapper, IIncomeRepository incomeRepository, IUserRepository userRepository): base(mapper)
	{
		_incomeRepository = incomeRepository;
		_userRepository   = userRepository;
	}

	public override async Task<GetTopIncomeCategoryQueryResult> Handle(GetTopIncomeCategoryQuery request, CancellationToken cancellationToken)
	{
		try
		{
			var user        = await _userRepository.GetByIdOrThrowAsync(request.UserId, cancellationToken);
			var groupedData = await _incomeRepository.GetTopCategoryAsync(request.UserId, cancellationToken);

			var resultDTOs = _mapper.Map<GetTopIncomeCategoryQueryResultDTO>(groupedData);

			return GetTopIncomeCategoryQueryResult.Succeeded(resultDTOs);
		}
		catch (Exception ex)
		{
			return GetTopIncomeCategoryQueryResult.Failed(ex);
		}
	}
}