using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseService.Application.Features.Income.Queries;

public class GetIncomesQueryResultDTO : AuditableDTO
{
	public Guid Id { get; set; }
	public string Description { get; set; }
	public DateTime Date { get; set; }
	public decimal Amount { get; set; }
	public string CategoryId { get; set; }
	public string UserId { get; set; }
}


public class GetIncomesQueryResult : QueryResult<IEnumerable<GetIncomesQueryResultDTO>, GetIncomesQueryResult>
{
	public GetIncomesQueryResult(IEnumerable<GetIncomesQueryResultDTO> data) : base(data)
	{
	}

	public GetIncomesQueryResult(Error error) : base(error)
	{
	}
}

public class GetIncomesQueryMapperProfile : Profile
{
	public GetIncomesQueryMapperProfile()
	{
		CreateMap<Domain.Entities.Income, GetIncomesQueryResultDTO>();
	}
}



/// <summary>
/// Represents a query to retrieve incomes.
/// </summary>
public class GetIncomesQuery : IRequest<GetIncomesQueryResult>
{

}


public class GetIncomesQueryHandler : BaseQueryHandler<GetIncomesQuery, GetIncomesQueryResult>
{
	readonly IIncomeRepository _incomeRepository;

	public GetIncomesQueryHandler(IMapper mapper, IIncomeRepository incomeRepository) : base(mapper)
	{
		_incomeRepository = incomeRepository;
	}

	public override async Task<GetIncomesQueryResult> Handle(GetIncomesQuery request, CancellationToken cancellationToken)
	{
		try
		{
			var incomes = await _incomeRepository.GetAsync(cancellationToken);
			var resultDTOs = _mapper.Map<IEnumerable<GetIncomesQueryResultDTO>>(incomes);
			var result = GetIncomesQueryResult.Succeeded(resultDTOs);

			return result;
		}
		catch (Exception e)
		{
			return GetIncomesQueryResult.Failed(e);
		}
	}
}