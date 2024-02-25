using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseService.Application.Features.User.Queries.Statistics;

public class GetSubscriptionExpensesSumGroupedByYearQueryResultDTO
{
   public int Year { get; set; }
   public decimal Sum { get; set; }
}

/// <summary>
/// Represents the result of a query to get the sum of subscription expenses grouped by year.
/// </summary>
public class GetSubscriptionExpensesSumGroupedByYearQueryResult: QueryResult<IEnumerable<GetSubscriptionExpensesSumGroupedByYearQueryResultDTO>, GetSubscriptionExpensesSumGroupedByYearQueryResult>
{
   public GetSubscriptionExpensesSumGroupedByYearQueryResult(IEnumerable<GetSubscriptionExpensesSumGroupedByYearQueryResultDTO> value): base(value)
   {
   }

   public GetSubscriptionExpensesSumGroupedByYearQueryResult(Error error): base(error)
   {
   }
}

public class GetSubscriptionExpensesSumGroupedByYearQueryMapper: Profile
{
   public GetSubscriptionExpensesSumGroupedByYearQueryMapper()
   {
	  CreateMap<(int Year, decimal Sum), GetSubscriptionExpensesSumGroupedByYearQueryResultDTO>()
	  																	 .ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.Year))
																		 .ForMember(dest => dest.Sum, opt => opt.MapFrom(src => src.Sum));
   }
}



/// <summary>
/// Represents a query to get the sum of subscription expenses grouped by year.
/// </summary>
public class GetSubscriptionExpensesSumGroupedByYearQuery: IRequest<GetSubscriptionExpensesSumGroupedByYearQueryResult>
{
   public string UserId { get; private set; }

   public GetSubscriptionExpensesSumGroupedByYearQuery(string userId)
   {
	  UserId = userId;
   }
}


public class GetSubscriptionExpensesSumGroupedByYearQueryHandler: BaseQueryHandler<GetSubscriptionExpensesSumGroupedByYearQuery, GetSubscriptionExpensesSumGroupedByYearQueryResult>
{
	readonly IUserRepository _userRepository;
   readonly ISubscriptionExpenseRepository _subscriptionExpenseRepository;

   public GetSubscriptionExpensesSumGroupedByYearQueryHandler(IMapper mapper, IUserRepository userRepository, ISubscriptionExpenseRepository subscriptionExpenseRepository): base(mapper)
   {
	  _userRepository                = userRepository;
	  _subscriptionExpenseRepository = subscriptionExpenseRepository;
   }


   public override async Task<GetSubscriptionExpensesSumGroupedByYearQueryResult> Handle(GetSubscriptionExpensesSumGroupedByYearQuery request, CancellationToken cancellationToken)
   {
	  try
	  {
		 var user            = await _userRepository.GetByIdOrThrowAsync(request.UserId, cancellationToken);
		 var groupedExpenses = await _subscriptionExpenseRepository.GetSumGroupedByYearAsync(request.UserId, cancellationToken);
		 var resultDTOs      = _mapper.Map<IEnumerable<GetSubscriptionExpensesSumGroupedByYearQueryResultDTO>>(groupedExpenses);

		 var result = GetSubscriptionExpensesSumGroupedByYearQueryResult.Succeeded(resultDTOs);

		 return result;
	  }
	  catch (Exception ex)
	  {
		return GetSubscriptionExpensesSumGroupedByYearQueryResult.Failed(ex);
	  }
   }
}