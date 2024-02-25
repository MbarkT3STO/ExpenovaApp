using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseService.Application.Features.User.Queries.Statistics;

public class GetTopSubscriptionExpenseCategoryQueryResultDTO
{
	public string? Category { get; set; }
	public decimal? Sum { get; set; }
}

public class GetTopSubscriptionExpenseCategoryQueryResult: QueryResult<GetTopSubscriptionExpenseCategoryQueryResultDTO, GetTopSubscriptionExpenseCategoryQueryResult>
{
	public GetTopSubscriptionExpenseCategoryQueryResult(GetTopSubscriptionExpenseCategoryQueryResultDTO value): base(value)
	{
	}

	public GetTopSubscriptionExpenseCategoryQueryResult(Error error): base(error)
	{
	}
}

public class GetTopSubscriptionExpenseCategoryQueryMapper: Profile
{
	public GetTopSubscriptionExpenseCategoryQueryMapper()
	{
		CreateMap<(string? Category, decimal? Sum), GetTopSubscriptionExpenseCategoryQueryResultDTO>()
																	   .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
																	   .ForMember(dest => dest.Sum, opt => opt.MapFrom(src => src.Sum));
	}
}



/// <summary>
/// Represents a query to get the top subscription expense category for a user.
/// </summary>
public class GetTopSubscriptionExpenseCategoryQuery: IRequest<GetTopSubscriptionExpenseCategoryQueryResult>
{
	public string UserId { get; private set; }

	public GetTopSubscriptionExpenseCategoryQuery(string userId)
	{
		UserId = userId;
	}
}


public class GetTopSubscriptionExpenseCategoryQueryHandler: BaseQueryHandler<GetTopSubscriptionExpenseCategoryQuery, GetTopSubscriptionExpenseCategoryQueryResult>
{
	readonly IUserRepository _userRepository;
	readonly ISubscriptionExpenseRepository _subscriptionExpenseRepository;

	public GetTopSubscriptionExpenseCategoryQueryHandler(IMapper mapper, IUserRepository userRepository, ISubscriptionExpenseRepository subscriptionExpenseRepository): base(mapper)
	{
		_userRepository                = userRepository;
		_subscriptionExpenseRepository = subscriptionExpenseRepository;
	}

	public override async Task<GetTopSubscriptionExpenseCategoryQueryResult> Handle(GetTopSubscriptionExpenseCategoryQuery request, CancellationToken cancellationToken)
	{
		try
		{
			var user        = await _userRepository.GetByIdOrThrowAsync(request.UserId, cancellationToken);
			var topCategory = await _subscriptionExpenseRepository.GetTopCategoryAsync(request.UserId, cancellationToken);

			var resultDTO = _mapper.Map<GetTopSubscriptionExpenseCategoryQueryResultDTO>(topCategory);
			var result    = GetTopSubscriptionExpenseCategoryQueryResult.Succeeded(resultDTO);

			return result;
		}
		catch (Exception ex)
		{
			return GetTopSubscriptionExpenseCategoryQueryResult.Failed(ex);
		}
	}
}