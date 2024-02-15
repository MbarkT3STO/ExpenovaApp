using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExpenseService.Domain.Enums;

namespace ExpenseService.Application.Features.SubscriptionExpense.Queries;

public class GetSubscriptionExpensesByCategoryAndUserQueryResultDTO: AuditableDTO
{
	public Guid Id { get; set; }
	public string Description { get; set; }
	public decimal Amount { get; set; }
	public string UserId { get; set; }
	public Guid CategoryId { get; set; }
	public DateTime StartDate { get; set; }
	public DateTime EndDate { get; set; }
	public RecurrenceInterval RecurrenceInterval { get; set; }
	public decimal BillingAmount { get; set; }
}


/// <summary>
/// Represents the result of the GetSubscriptionExpensesByCategoryAndUserQuery.
/// </summary>
public class GetSubscriptionExpensesByCategoryAndUserQueryResult: QueryResult<IEnumerable<GetSubscriptionExpensesByCategoryAndUserQueryResultDTO>, GetSubscriptionExpensesByCategoryAndUserQueryResult>
{
	public GetSubscriptionExpensesByCategoryAndUserQueryResult(IEnumerable<GetSubscriptionExpensesByCategoryAndUserQueryResultDTO> value): base(value)
	{
	}

	public GetSubscriptionExpensesByCategoryAndUserQueryResult(Error error): base(error)
	{
	}
}

public class GetSubscriptionExpensesByCategoryAndUserQueryMappingProfile: Profile
{
	public GetSubscriptionExpensesByCategoryAndUserQueryMappingProfile()
	{
		CreateMap<Domain.Entities.SubscriptionExpense, GetSubscriptionExpensesByCategoryAndUserQueryResultDTO>();
	}
}


/// <summary>
/// Represents a query to retrieve subscription expenses by category and user.
/// </summary>
public class GetSubscriptionExpensesByCategoryAndUserQuery: IRequest<GetSubscriptionExpensesByCategoryAndUserQueryResult>
{
	public Guid CategoryId { get; private set; }
	public string UserId { get; private set; }

	public GetSubscriptionExpensesByCategoryAndUserQuery(Guid categoryId, string userId)
	{
		CategoryId = categoryId;
		UserId     = userId;
	}
}


public class GetSubscriptionExpensesByCategoryAndUserQueryHandler: BaseQueryHandler<GetSubscriptionExpensesByCategoryAndUserQuery, GetSubscriptionExpensesByCategoryAndUserQueryResult>
{
	readonly IUserRepository _userRepository;
	readonly ICategoryRepository _categoryRepository;
	readonly ISubscriptionExpenseRepository _subscriptionExpenseRepository;


	public GetSubscriptionExpensesByCategoryAndUserQueryHandler(IMapper mapper, IUserRepository userRepository, ICategoryRepository categoryRepository, ISubscriptionExpenseRepository subscriptionExpenseRepository): base(mapper)
	{
		_userRepository                = userRepository;
		_categoryRepository            = categoryRepository;
		_subscriptionExpenseRepository = subscriptionExpenseRepository;
	}


	public override async Task<GetSubscriptionExpensesByCategoryAndUserQueryResult> Handle(GetSubscriptionExpensesByCategoryAndUserQuery request, CancellationToken cancellationToken)
	{
		try
		{
			var user       = await _userRepository.GetByIdOrThrowAsync(request.UserId, cancellationToken);
			var category   = await _categoryRepository.GetByIdOrThrowAsync(request.CategoryId, cancellationToken);

			var entities   = await _subscriptionExpenseRepository.GetSubscriptionExpensesByUserAndCategoryAsync(request.UserId, request.CategoryId, cancellationToken);

			var resultDTOs = _mapper.Map<List<GetSubscriptionExpensesByCategoryAndUserQueryResultDTO>>(entities);
			var result     = GetSubscriptionExpensesByCategoryAndUserQueryResult.Succeeded(resultDTOs);

			return result;
		}
		catch (Exception ex)
		{
			return GetSubscriptionExpensesByCategoryAndUserQueryResult.Failed(new Error(ex.Message));
		}
	}
}