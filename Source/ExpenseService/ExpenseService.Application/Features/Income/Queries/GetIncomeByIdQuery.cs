using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseService.Application.Features.Income.Queries;

public class GetIncomeByIdQueryResultDTO : AuditableDTO
{
	public Guid Id { get; set; }
	public string Description { get; set; }
	public DateTime Date { get; set; }
	public decimal Amount { get; set; }
	public Guid CategoryId { get; set; }
	public string UserId { get; set; }
}

public class GetIncomeByIdQueryResult : QueryResult<GetIncomeByIdQueryResultDTO, GetIncomeByIdQueryResult>
{
	public GetIncomeByIdQueryResult(GetIncomeByIdQueryResultDTO data) : base(data)
	{
	}

	public GetIncomeByIdQueryResult(Error error) : base(error)
	{
	}
}

public class GetIncomeByIdQueryMapperProfile : Profile
{
	public GetIncomeByIdQueryMapperProfile()
	{
		CreateMap<Domain.Entities.Income, GetIncomeByIdQueryResultDTO>();
	}
}




/// <summary>
/// Represents a query to retrieve an income by its ID.
/// </summary>
public class GetIncomeByIdQuery : IRequest<GetIncomeByIdQueryResult>
{
	public Guid Id { get; private set; }

	public GetIncomeByIdQuery(Guid id)
	{
		Id = id;
	}
}


public class GetIncomeByIdQueryHandler : BaseQueryHandler<GetIncomeByIdQuery, GetIncomeByIdQueryResult>
{
	readonly IIncomeRepository _incomeRepository;

	public GetIncomeByIdQueryHandler(IMapper mapper, IIncomeRepository incomeRepository) : base(mapper)
	{
		_incomeRepository = incomeRepository;
	}

	public override async Task<GetIncomeByIdQueryResult> Handle(GetIncomeByIdQuery request, CancellationToken cancellationToken)
	{
		try
		{
			var income = await _incomeRepository.GetByIdOrThrowAsync(request.Id, cancellationToken);

			var resultDTO = _mapper.Map<GetIncomeByIdQueryResultDTO>(income);
			var result = GetIncomeByIdQueryResult.Succeeded(resultDTO);

			return result;
		}
		catch (Exception ex)
		{
			return GetIncomeByIdQueryResult.Failed(ex);
		}
	}
}