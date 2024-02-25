using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseService.Application.Features.User.Queries.Statistics;

public class GetExpensesSumGroupedByMonthAndYearQueryResultDTO
{
   public int Year { get; set; }
   public int Month { get; set; }
   public decimal Sum { get; set; }
}


/// <summary>
/// Represents the result of the GetExpensesSumGroupedByMonthAndYearQuery.
/// </summary>
/// <typeparam name="TValue">The type of the value contained in the result.</typeparam>
/// <typeparam name="TError">The type of the error contained in the result.</typeparam>
public class GetExpensesSumGroupedByMonthAndYearQueryResult: QueryResult<IEnumerable<GetExpensesSumGroupedByMonthAndYearQueryResultDTO>, GetExpensesSumGroupedByMonthAndYearQueryResult>
{
   public GetExpensesSumGroupedByMonthAndYearQueryResult(IEnumerable<GetExpensesSumGroupedByMonthAndYearQueryResultDTO> value): base(value)
   {
   }

   public GetExpensesSumGroupedByMonthAndYearQueryResult(Error error): base(error)
   {
   }
}


public class GetExpensesSumGroupedByMonthAndYearQueryMapper: Profile
{
   public GetExpensesSumGroupedByMonthAndYearQueryMapper()
   {
	  CreateMap<(int Month, int Year, decimal Sum), GetExpensesSumGroupedByMonthAndYearQueryResultDTO>()
	  																	 .ForMember(dest => dest.Month, opt => opt.MapFrom(src => src.Month))
																		 .ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.Year))
																		 .ForMember(dest => dest.Sum, opt => opt.MapFrom(src => src.Sum));
   }
}



/// <summary>
/// Represents a query to get the sum of expenses grouped by month and year for a specific user.
/// </summary>
public class GetExpensesSumGroupedByMonthAndYearQuery: IRequest<GetExpensesSumGroupedByMonthAndYearQueryResult>
{
   public string UserId { get; private set; }

   public GetExpensesSumGroupedByMonthAndYearQuery(string userId)
   {
	  UserId = userId;
   }
}


public class GetExpensesSumGroupedByMonthAndYearQueryHandler: BaseQueryHandler<GetExpensesSumGroupedByMonthAndYearQuery, GetExpensesSumGroupedByMonthAndYearQueryResult>
{
	readonly IUserRepository _userRepository;
   readonly IExpenseRepository _expenseRepository;

   public GetExpensesSumGroupedByMonthAndYearQueryHandler(IMapper mapper, IUserRepository userRepository, IExpenseRepository expenseRepository): base(mapper)
   {
	  _userRepository    = userRepository;
	  _expenseRepository = expenseRepository;
   }


   public override async Task<GetExpensesSumGroupedByMonthAndYearQueryResult> Handle(GetExpensesSumGroupedByMonthAndYearQuery request, CancellationToken cancellationToken)
   {
	  try
	  {
		 var user            = await _userRepository.GetByIdOrThrowAsync(request.UserId, cancellationToken);
		 var groupedExpenses = await _expenseRepository.GetSumGroupedByMonthAndYearAsync(request.UserId, cancellationToken);
		 var resultDTOs      = _mapper.Map<IEnumerable<GetExpensesSumGroupedByMonthAndYearQueryResultDTO>>(groupedExpenses);


		 var result = GetExpensesSumGroupedByMonthAndYearQueryResult.Succeeded(resultDTOs);

		 return result;
	  }
	  catch (Exception ex)
	  {
		 return GetExpensesSumGroupedByMonthAndYearQueryResult.Failed(ex);
	  }
   }
}