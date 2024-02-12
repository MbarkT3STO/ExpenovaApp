using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseService.Application.Features.SubscriptionExpense.Queries;

public class GetSubscriptionExpensesQueryResultDTO
{
	public Guid Id { get; private set; }
	public string Description { get; private set; }
	public decimal Amount { get; private set; }
	public string UserId { get; private set; }
	public Guid CategoryId { get; private set; }
	public DateTime StartDate { get; private set; }
	public DateTime EndDate { get; private set; }

	public DateTime CreatedAt { get; set; }
	public string CreatedBy { get; set; }
	public DateTime? LastUpdatedAt { get; set; }
	public string LastUpdatedBy { get; set; }
	public bool IsDeleted { get; set; }
	public DateTime? DeletedAt { get; set; }
	public string? DeletedBy { get; set; }
}

/// <summary>
/// Represents the result of a <see cref="GetSubscriptionExpensesQuery"/>.
/// </summary>
public class GetSubscriptionExpensesQueryResult: QueryResult<IEnumerable<GetSubscriptionExpensesQueryResultDTO>, GetSubscriptionExpensesQueryResult>
{
	public GetSubscriptionExpensesQueryResult(IEnumerable<GetSubscriptionExpensesQueryResultDTO> value): base(value)
	{
	}

	public GetSubscriptionExpensesQueryResult(Error error): base(error)
	{
	}
}

public class MappingProfile: Profile
{
	public MappingProfile()
	{
		CreateMap<Domain.Entities.SubscriptionExpense, GetSubscriptionExpensesQueryResultDTO>();
	}
}


/// <summary>
/// Represents a query to get all subscription expenses.
/// </summary>
public class GetSubscriptionExpensesQuery: IRequest<GetSubscriptionExpensesQueryResult>
{
}

public class GetSubscriptionExpensesQueryHandler: BaseQueryHandler<GetSubscriptionExpensesQuery, GetSubscriptionExpensesQueryResult>
{
	readonly ISubscriptionExpenseRepository _subscriptionExpenseRepository;

	public GetSubscriptionExpensesQueryHandler(IMapper mapper, ISubscriptionExpenseRepository subscriptionExpenseRepository): base(mapper)
	{
		_subscriptionExpenseRepository = subscriptionExpenseRepository;
	}

	public override async Task<GetSubscriptionExpensesQueryResult> Handle(GetSubscriptionExpensesQuery request, CancellationToken cancellationToken)
	{
		try
		{
			var expenses   = await _subscriptionExpenseRepository.GetAsync(cancellationToken);
			var resultDTOs = _mapper.Map<IEnumerable<GetSubscriptionExpensesQueryResultDTO>>(expenses);
			var result     = GetSubscriptionExpensesQueryResult.Succeeded(resultDTOs);

			return result;
		}
		catch (Exception ex)
		{
			return GetSubscriptionExpensesQueryResult.Failed(new Error(ex.Message));
		}
	}
}
