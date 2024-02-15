using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExpenseService.Domain.Enums;

namespace ExpenseService.Application.Features.SubscriptionExpense.Queries;

public class GetSubscriptionExpensesByUserQueryResultDTO : AuditableDTO
{
	public Guid Id { get; private set; }
	public string Description { get; private set; }
	public decimal Amount { get; private set; }
	public string UserId { get; private set; }
	public Guid CategoryId { get; private set; }
	public DateTime StartDate { get; private set; }
	public DateTime EndDate { get; private set; }
	public RecurrenceInterval RecurrenceInterval { get; private set; }
	public decimal BillingAmount { get; private set; }
}


/// <summary>
/// Represents the result of the GetSubscriptionExpensesByUserQuery.
/// </summary>
public class GetSubscriptionExpensesByUserQueryResult : QueryResult<List<GetSubscriptionExpensesByUserQueryResultDTO>, GetSubscriptionExpensesByUserQueryResult>
{
	public GetSubscriptionExpensesByUserQueryResult(List<GetSubscriptionExpensesByUserQueryResultDTO> value) : base(value)
	{
	}

	public GetSubscriptionExpensesByUserQueryResult(Error error) : base(error)
	{
	}
}


public class GetSubscriptionExpensesByUserQueryMappingProfile : Profile
{
	public GetSubscriptionExpensesByUserQueryMappingProfile()
	{
		CreateMap<Domain.Entities.SubscriptionExpense, GetSubscriptionExpensesByUserQueryResultDTO>();
	}
}




/// <summary>
/// Represents a query to retrieve subscription expenses by user.
/// </summary>
public class GetSubscriptionExpensesByUserQuery : IRequest<GetSubscriptionExpensesByUserQueryResult>
{
	public string UserId { get; private set; }


	public GetSubscriptionExpensesByUserQuery(string userId)
	{
		UserId = userId;
	}
}


public class GetSubscriptionExpensesByUserQueryHandler : BaseQueryHandler<GetSubscriptionExpensesByUserQuery, GetSubscriptionExpensesByUserQueryResult>
{
	readonly ISubscriptionExpenseRepository _subscriptionExpenseRepository;
	readonly IUserRepository _userRepository;

	public GetSubscriptionExpensesByUserQueryHandler(IMapper mapper, ISubscriptionExpenseRepository subscriptionExpenseRepository, IUserRepository userRepository) : base(mapper)
	{
		_subscriptionExpenseRepository = subscriptionExpenseRepository;
		_userRepository = userRepository;
	}


	public override async Task<GetSubscriptionExpensesByUserQueryResult> Handle(GetSubscriptionExpensesByUserQuery request, CancellationToken cancellationToken)
	{
		try
		{
			var category = await _userRepository.GetByIdOrThrowAsync(request.UserId, cancellationToken);
			var subscriptionExpenses = await _subscriptionExpenseRepository.GetSubscriptionExpensesByUserAsync(request.UserId, cancellationToken);
			var resultDTOs = _mapper.Map<List<GetSubscriptionExpensesByUserQueryResultDTO>>(subscriptionExpenses);
			var result = GetSubscriptionExpensesByUserQueryResult.Succeeded(resultDTOs);

			return result;
		}
		catch (Exception ex)
		{
			return new GetSubscriptionExpensesByUserQueryResult(new Error(ex.Message));
		}
	}
}
