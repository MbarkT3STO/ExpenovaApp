using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExpenseService.Domain.Enums;

namespace ExpenseService.Application.Features.SubscriptionExpense.Queries;

public class GetSubscriptionExpensesByCategoryResultDTO: AuditableDTO
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
/// Represents the result of the GetSubscriptionExpensesByCategoryQuery.
/// </summary>
public class GetSubscriptionExpensesByCategoryResult: QueryResult<List<GetSubscriptionExpensesByCategoryResultDTO>, GetSubscriptionExpensesByCategoryResult>
{
	public GetSubscriptionExpensesByCategoryResult(List<GetSubscriptionExpensesByCategoryResultDTO> value): base(value)
	{
	}

	public GetSubscriptionExpensesByCategoryResult(Error error): base(error)
	{
	}
}


public class GetSubscriptionExpensesByCategoryMappingProfile: Profile
{
	public GetSubscriptionExpensesByCategoryMappingProfile()
	{
		CreateMap<Domain.Entities.SubscriptionExpense, GetSubscriptionExpensesByCategoryResultDTO>();
	}
}



/// <summary>
/// Represents a query to retrieve subscription expenses by category.
/// </summary>
public class GetSubscriptionExpensesByCategoryQuery: IRequest<GetSubscriptionExpensesByCategoryResult>
{
	public Guid CategoryId { get; private set; }

	public GetSubscriptionExpensesByCategoryQuery(Guid categoryId)
	{
		CategoryId = categoryId;
	}
}


public class GetSubscriptionExpensesByCategoryQueryHandler: BaseQueryHandler<GetSubscriptionExpensesByCategoryQuery, GetSubscriptionExpensesByCategoryResult>
{
	readonly ISubscriptionExpenseRepository _subscriptionExpenseRepository;
	readonly ICategoryRepository _categoryRepository;

	public GetSubscriptionExpensesByCategoryQueryHandler(IMapper mapper, ISubscriptionExpenseRepository subscriptionExpenseRepository, ICategoryRepository categoryRepository): base(mapper)
	{
		_subscriptionExpenseRepository = subscriptionExpenseRepository;
		_categoryRepository            = categoryRepository;
	}


	public override async Task<GetSubscriptionExpensesByCategoryResult> Handle(GetSubscriptionExpensesByCategoryQuery request, CancellationToken cancellationToken)
	{
		try
		{
			var category             = await _categoryRepository.GetByIdOrThrowAsync(request.CategoryId, cancellationToken);
			var subscriptionExpenses = await _subscriptionExpenseRepository.GetSubscriptionExpensesByCategoryAsync(request.CategoryId, cancellationToken);
			var resultDTOs           = _mapper.Map<List<GetSubscriptionExpensesByCategoryResultDTO>>(subscriptionExpenses);
			var result               = GetSubscriptionExpensesByCategoryResult.Succeeded(resultDTOs);

			return result;
		}
		catch (Exception ex)
		{
			return new GetSubscriptionExpensesByCategoryResult(new Error(ex.Message));
		}
	}
}