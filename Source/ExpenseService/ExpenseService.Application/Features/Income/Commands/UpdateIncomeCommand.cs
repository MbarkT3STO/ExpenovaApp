using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExpenseService.Application.Extensions;
using ExpenseService.Domain.Specifications.IncomeSpecifications.Composite;

namespace ExpenseService.Application.Features.Income.Commands;

public class UpdateIncomeCommandResultDTO: AuditableDTO
{
	public Guid Id { get; set; }
	public string Description { get; set; }
	public DateTime Date { get; set; }
	public decimal Amount { get; set; }
	public Guid CategoryId { get; set; }
	public string UserId { get; set; }
}

public class UpdateIncomeCommandResult: CommandResult<UpdateIncomeCommandResultDTO, UpdateIncomeCommandResult>
{
	public UpdateIncomeCommandResult(UpdateIncomeCommandResultDTO data): base(data)
	{
	}

	public UpdateIncomeCommandResult(Error error): base(error)
	{
	}
}

public class UpdateIncomeCommandMapperProfile: Profile
{
	public UpdateIncomeCommandMapperProfile()
	{
		CreateMap<Domain.Entities.Income, UpdateIncomeCommandResultDTO>();
	}
}



/// <summary>
/// Represents a command to update an income.
/// </summary>
public class UpdateIncomeCommand: IRequest<UpdateIncomeCommandResult>
{
	public Guid Id { get; set; }
	public string Description { get; set; }
	public DateTime Date { get; set; }
	public decimal Amount { get; set; }
	public Guid CategoryId { get; set; }
	public string UserId { get; set; }
}

public class UpdateIncomeCommandHandler: BaseCommandHandler<UpdateIncomeCommand, UpdateIncomeCommandResult, UpdateIncomeCommandResultDTO>
{
	readonly IUserRepository _userRepository;
	readonly ICategoryRepository _categoryRepository;
	readonly IIncomeRepository _incomeRepository;

	public UpdateIncomeCommandHandler(IMediator mediator, IMapper mapper, IUserRepository userRepository, ICategoryRepository categoryRepository, IIncomeRepository incomeRepository): base(mediator, mapper)
	{
		_userRepository     = userRepository;
		_categoryRepository = categoryRepository;
		_incomeRepository   = incomeRepository;
	}


	public override async Task<UpdateIncomeCommandResult> Handle(UpdateIncomeCommand request, CancellationToken cancellationToken)
	{
		try
		{
			var user     = await _userRepository.GetByIdOrThrowAsync(request.UserId, cancellationToken);
			var category = await _categoryRepository.GetByIdOrThrowAsync(request.CategoryId, cancellationToken);
			var income   = await _incomeRepository.GetByIdOrThrowAsync(request.Id, cancellationToken);

			income.Update(request.Description, request.Date, request.Amount, category, user);
			income.WriteUpdatedAudit(request.UserId);

			await ApplyUpdateAsync(income, cancellationToken);

			// TODO: Publish the Update event

			var resultDTO = _mapper.Map<UpdateIncomeCommandResultDTO>(income);
			var result = UpdateIncomeCommandResult.Succeeded(resultDTO);

			return result;
		}
		catch (Exception ex)
		{
			return UpdateIncomeCommandResult.Failed(ex);
		}
	}


	async Task ApplyUpdateAsync(Domain.Entities.Income income, CancellationToken cancellationToken)
	{
		income.Validate(new IsValidIncomeForUpdateSpecification());

		await _incomeRepository.UpdateAsync(income, cancellationToken);
	}
}