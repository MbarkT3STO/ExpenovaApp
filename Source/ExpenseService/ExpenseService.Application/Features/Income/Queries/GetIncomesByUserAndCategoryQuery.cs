using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseService.Application.Features.Income.Queries;

public class GetIncomesByUserAndCategoryQueryResultDTO: AuditableDTO
{
	public Guid Id { get; set; }
	public string Description { get; set; }
	public DateTime Date { get; set; }
	public decimal Amount { get; set; }
	public Guid CategoryId { get; set; }
	public string UserId { get; set; }
}

public class GetIncomesByUserAndCategoryQueryResult: QueryResult<IEnumerable<GetIncomesByUserAndCategoryQueryResultDTO>, GetIncomesByUserAndCategoryQueryResult>
{
	public GetIncomesByUserAndCategoryQueryResult(IEnumerable<GetIncomesByUserAndCategoryQueryResultDTO> data): base(data)
	{
	}

	public GetIncomesByUserAndCategoryQueryResult(Error error): base(error)
	{
	}
}

public class GetIncomesByUserAndCategoryQueryMapperProfile: Profile
{
	public GetIncomesByUserAndCategoryQueryMapperProfile()
	{
		CreateMap<Domain.Entities.Income, GetIncomesByUserAndCategoryQueryResultDTO>();
	}
}



/// <summary>
/// Represents a query to retrieve incomes by user and category.
/// </summary>
public class GetIncomesByUserAndCategoryQuery: IRequest<GetIncomesByUserAndCategoryQueryResult>
{public string UserId { get; set; }public Guid CategoryId { get; set; }

public GetIncomesByUserAndCategoryQuery(string userId, Guid categoryId)
	{
		UserId         = userId;
		CategoryId = categoryId;
	}
}


public class GetIncomesByUserAndCategoryQueryHandler: BaseQueryHandler<GetIncomesByUserAndCategoryQuery, GetIncomesByUserAndCategoryQueryResult>
{
	readonly IIncomeRepository _incomeRepository;
	readonly IUserRepository _userRepository;
	readonly ICategoryRepository _categoryRepository;

	public GetIncomesByUserAndCategoryQueryHandler(IMapper mapper, IIncomeRepository incomeRepository, IUserRepository userRepository, ICategoryRepository categoryRepository): base(mapper)
	{
		_incomeRepository   = incomeRepository;
		_userRepository     = userRepository;
		_categoryRepository = categoryRepository;
	}

	public override async Task<GetIncomesByUserAndCategoryQueryResult> Handle(GetIncomesByUserAndCategoryQuery request, CancellationToken cancellationToken)
	{
		try
		{
			var user      = await _userRepository.GetByIdOrThrowAsync(request.UserId, cancellationToken);
			var category  = await _categoryRepository.GetByIdOrThrowAsync(request.CategoryId, cancellationToken);
			var incomes   = await _incomeRepository.GetIncomesByUserAndCategoryAsync(request.UserId, request.CategoryId, cancellationToken);
			var resultDTO = _mapper.Map<IEnumerable<GetIncomesByUserAndCategoryQueryResultDTO>>(incomes);

			return GetIncomesByUserAndCategoryQueryResult.Succeeded(resultDTO);
		}
		catch (Exception ex)
		{
			return GetIncomesByUserAndCategoryQueryResult.Failed(ex);
		}
	}
}