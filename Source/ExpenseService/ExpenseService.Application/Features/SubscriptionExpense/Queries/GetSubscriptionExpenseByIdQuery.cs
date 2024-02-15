using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExpenseService.Domain.Enums;

namespace ExpenseService.Application.Features.SubscriptionExpense.Queries;

public class GetSubscriptionExpenseByIdQueryResultDTO: AuditableDTO
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
/// Represents the result of the GetSubscriptionExpenseByIdQuery.
/// </summary>
public class GetSubscriptionExpenseByIdQueryResult: QueryResult<GetSubscriptionExpenseByIdQueryResultDTO, GetSubscriptionExpenseByIdQueryResult>
{
	public GetSubscriptionExpenseByIdQueryResult(GetSubscriptionExpenseByIdQueryResultDTO value): base(value)
	{
	}

	public GetSubscriptionExpenseByIdQueryResult(Error error): base(error)
	{
	}
}

public class GetSubscriptionExpenseByIdQueryMappingProfile: Profile
{
	public GetSubscriptionExpenseByIdQueryMappingProfile()
	{
		CreateMap<Domain.Entities.SubscriptionExpense, GetSubscriptionExpenseByIdQueryResultDTO>();
	}
}


/// <summary>
/// Represents a query to retrieve a subscription expense by its ID.
/// </summary>
public class GetSubscriptionExpenseByIdQuery: IRequest<GetSubscriptionExpenseByIdQueryResult>
{
	public Guid Id { get; private set; }

	public GetSubscriptionExpenseByIdQuery(Guid id)
	{
		Id = id;
	}
}


public class GetSubscriptionExpenseByIdQueryHandler: BaseQueryHandler<GetSubscriptionExpenseByIdQuery, GetSubscriptionExpenseByIdQueryResult>
{
	readonly ISubscriptionExpenseRepository _subscriptionExpenseRepository;

	public GetSubscriptionExpenseByIdQueryHandler(IMapper mapper, ISubscriptionExpenseRepository subscriptionExpenseRepository): base(mapper)
	{
		_subscriptionExpenseRepository = subscriptionExpenseRepository;
	}

	public override async Task<GetSubscriptionExpenseByIdQueryResult> Handle(GetSubscriptionExpenseByIdQuery request, CancellationToken cancellationToken)
	{
		try
		{
			var expense   = await _subscriptionExpenseRepository.GetByIdOrThrowAsync(request.Id, cancellationToken);
			var resultDTO = _mapper.Map<GetSubscriptionExpenseByIdQueryResultDTO>(expense);
			var result    = GetSubscriptionExpenseByIdQueryResult.Succeeded(resultDTO);

			return result;
		}
		catch (Exception ex)
		{
			return GetSubscriptionExpenseByIdQueryResult.Failed(new Error(ex.Message));
		}
	}
}