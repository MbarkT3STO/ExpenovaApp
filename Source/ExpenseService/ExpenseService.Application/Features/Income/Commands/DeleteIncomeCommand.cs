using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExpenseService.Application.Extensions;
using ExpenseService.Domain.Specifications.IncomeSpecifications.Composite;

namespace ExpenseService.Application.Features.Income.Commands;

public class DeleteIncomeCommandResultDTO: AuditableDTO
{
	public Guid Id { get; set; }
	public string Description { get; set; }
	public DateTime Date { get; set; }
	public decimal Amount { get; set; }
	public Guid CategoryId { get; set; }
	public string UserId { get; set; }
}

public class DeleteIncomeCommandResult: CommandResult<DeleteIncomeCommandResultDTO, DeleteIncomeCommandResult>
{
	public DeleteIncomeCommandResult(DeleteIncomeCommandResultDTO data): base(data)
	{
	}

	public DeleteIncomeCommandResult(Error error): base(error)
	{
	}
}

public class DeleteIncomeCommandMapperProfile: Profile
{
	public DeleteIncomeCommandMapperProfile()
	{
		CreateMap<Domain.Entities.Income, DeleteIncomeCommandResultDTO>();
	}
}



/// <summary>
/// Represents a command to delete an income.
/// </summary>
public class DeleteIncomeCommand: IRequest<DeleteIncomeCommandResult>
{
	public Guid Id { get; set; }

	public DeleteIncomeCommand(Guid id)
	{
		Id = id;
	}
}


public class DeleteIncomeCommandHandler: BaseCommandHandler<DeleteIncomeCommand, DeleteIncomeCommandResult, DeleteIncomeCommandResultDTO>
{
	readonly IIncomeRepository _incomeRepository;

	public DeleteIncomeCommandHandler(IMediator mediator, IMapper mapper, IIncomeRepository incomeRepository): base(mediator, mapper)
	{
		_incomeRepository = incomeRepository;
	}

	public override async Task<DeleteIncomeCommandResult> Handle(DeleteIncomeCommand request, CancellationToken cancellationToken)
	{
		try
		{
			var income = await _incomeRepository.GetByIdOrThrowAsync(request.Id, cancellationToken);

			income.WriteDeletedAudit(income.User.Id);

			await ApplySoftDeleteAsync(income, cancellationToken);
			await PublishIncomeDeletedEvent(income, cancellationToken);


			var resultDTO = _mapper.Map<DeleteIncomeCommandResultDTO>(income);
			var result    = DeleteIncomeCommandResult.Succeeded(resultDTO);

			return result;
		}
		catch (Exception ex)
		{
			return DeleteIncomeCommandResult.Failed(ex);
		}
	}

	async Task ApplySoftDeleteAsync(Domain.Entities.Income income, CancellationToken cancellationToken)
	{
		income.Validate(new IsValidIncomeForSoftDeleteSpecification());

		await _incomeRepository.DeleteAsync(income, cancellationToken);
	}

	async Task PublishIncomeDeletedEvent(Domain.Entities.Income income, CancellationToken cancellationToken)
	{
		var eventDetails = new DomainEventDetails(nameof(IncomeDeletedEvent), income.User.Id);
		var eventData    = new IncomeDeletedEventData(income.Id, income.Description, income.Date, income.Amount, income.Category.Id, income.User.Id);
		var @event       = IncomeDeletedEvent.Create(eventDetails, eventData);

		await _mediator.Publish(@event, cancellationToken);
	}
}
