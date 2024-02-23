using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExpenseService.Application.Extensions;
using ExpenseService.Domain.Enums;
using ExpenseService.Domain.Specifications.SubscriptionExpenseSpecifications.Composite;

namespace ExpenseService.Application.Features.SubscriptionExpense.Commands;

public class DeleteSubscriptionExpenseCommandResultDTO
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
/// Represents the result of a delete subscription expense command.
/// </summary>
public class DeleteSubscriptionExpenseCommandResult: CommandResult<DeleteSubscriptionExpenseCommandResultDTO, DeleteSubscriptionExpenseCommandResult>
{
	public DeleteSubscriptionExpenseCommandResult(DeleteSubscriptionExpenseCommandResultDTO value): base(value)
	{
	}

	public DeleteSubscriptionExpenseCommandResult(Error error): base(error)
	{
	}
}

public class DeleteSubscriptionExpenseCommandMappingProfile: Profile
{
	public DeleteSubscriptionExpenseCommandMappingProfile()
	{
		CreateMap<Domain.Entities.SubscriptionExpense, DeleteSubscriptionExpenseCommandResultDTO>();
	}
}



/// <summary>
/// Represents a command to delete a subscription expense.
/// </summary>
public class DeleteSubscriptionExpenseCommand: IRequest<DeleteSubscriptionExpenseCommandResult>
{
	public Guid Id { get; private set; }

	public DeleteSubscriptionExpenseCommand(Guid id)
	{
		Id = id;
	}
}


public class DeleteSubscriptionExpenseCommandHandler: BaseCommandHandler<DeleteSubscriptionExpenseCommand, DeleteSubscriptionExpenseCommandResult, DeleteSubscriptionExpenseCommandResultDTO>
{
	readonly ISubscriptionExpenseRepository _subscriptionExpenseRepository;

	public DeleteSubscriptionExpenseCommandHandler(IMapper mapper, IMediator mediator, ISubscriptionExpenseRepository subscriptionExpenseRepository): base(mediator, mapper)
	{
		_subscriptionExpenseRepository = subscriptionExpenseRepository;
	}

	public override async Task<DeleteSubscriptionExpenseCommandResult> Handle(DeleteSubscriptionExpenseCommand request, CancellationToken cancellationToken)
	{
		try
		{
			var expense = await _subscriptionExpenseRepository.GetByIdOrThrowAsync(request.Id, cancellationToken);

			expense.WriteDeletedAudit(expense.User.Id);

			await DeleteAsync(expense, cancellationToken);
			await PublishSubscriptionExpenseDeletedEvent(expense, cancellationToken);

			var resultDTO = _mapper.Map<DeleteSubscriptionExpenseCommandResultDTO>(expense);
			var result    = DeleteSubscriptionExpenseCommandResult.Succeeded(resultDTO);

			return result;
		}
		catch (Exception ex)
		{
			return DeleteSubscriptionExpenseCommandResult.Failed(ex);
		}
	}



	async Task DeleteAsync(Domain.Entities.SubscriptionExpense entity, CancellationToken cancellationToken)
	{
		entity.Validate(new IsValidSubscriptionExpenseForSoftDeleteSpecification());
		await _subscriptionExpenseRepository.DeleteAsync(entity, cancellationToken);
	}


	async Task PublishSubscriptionExpenseDeletedEvent(Domain.Entities.SubscriptionExpense entity, CancellationToken cancellationToken)
	{
		var eventData    = new SubscriptionExpenseDeletedEventData(entity.Id, entity.Description, entity.Amount, entity.User.Id, entity.Category.Id, entity.StartDate, entity.EndDate, entity.RecurrenceInterval, entity.BillingAmount, entity.CreatedAt, entity.CreatedBy, entity.LastUpdatedAt, entity.LastUpdatedBy, entity.IsDeleted, entity.DeletedAt);
		var eventDetails = new DomainEventDetails(nameof(SubscriptionExpenseDeletedEvent), entity.User.Id);
		var @event       = SubscriptionExpenseDeletedEvent.Create(eventDetails, eventData);

		await _mediator.Publish(@event, cancellationToken);
	}
}