using ExpenseService.Application.ApplicationServices;
using ExpenseService.Application.Extensions;
using ExpenseService.Domain.Shared.Services;

namespace ExpenseService.Application.Features.Expense.Commands;

public class DeleteExpenseCommandResultDto
{
	public Guid Id { get; private set; }
	public bool IsDeleted { get; private set; }
	public DateTime DeletedAt { get; private set; }
	public string DeletedBy { get; private set; }
}

public class DeleteExpenseCommandResult: CommandResult<DeleteExpenseCommandResultDto, DeleteExpenseCommandResult>
{
	public DeleteExpenseCommandResult(DeleteExpenseCommandResultDto data): base(data)
	{
	}

	public DeleteExpenseCommandResult(Error error): base(error)
	{
	}
}


public class DeleteExpenseCommandMappingProfile: Profile
{
	public DeleteExpenseCommandMappingProfile()
	{
		CreateMap<Domain.Entities.Expense, DeleteExpenseCommandResultDto>();
	}
}



/// <summary>
/// Represents a command to delete an expense.
/// </summary>
public class DeleteExpenseCommand: IRequest<DeleteExpenseCommandResult>
{
	public Guid Id { get; private set; }
	public string DeletedBy { get; private set; }

	public DeleteExpenseCommand(Guid id, string deletedBy)
	{
		Id        = id;
		DeletedBy = deletedBy;
	}
}


public class DeleteExpenseCommandHandler: IRequestHandler<DeleteExpenseCommand, DeleteExpenseCommandResult>
{
	private readonly IExpenseRepository _expenseRepository;
	private readonly ApplicationExpenseService expenseService;
	private readonly IMapper _mapper;
	private readonly IMediator _mediator;

	public DeleteExpenseCommandHandler(IExpenseRepository expenseRepository, ApplicationExpenseService expenseService, IMapper mapper, IMediator mediator)
	{
		_expenseRepository  = expenseRepository;
		this.expenseService = expenseService;
		_mapper             = mapper;
		_mediator           = mediator;
	}

	public async Task<DeleteExpenseCommandResult> Handle(DeleteExpenseCommand request, CancellationToken cancellationToken)
	{
		try
		{
			var expense = await expenseService.GetExpenseByIdOrThrowAsync(request.Id);

			// TODO: This is a workaround to set the deleted by property of the expense
			// For now, the deleted by property is set as the User's ID related to the expense
			// In the future, this should be set as the ID of the user who is deleting the expense and it should be retrieved from the request ( Web API )
			// As the following: expense.WriteDeletedAudit(request.DeletedBy);

			expense.WriteDeletedAudit(expense.User.Id);

			await expenseService.ApplySoftDeleteAsync(expense);
			await PublishExpenseDeletedEvent(expense);

			var resultDTO = _mapper.Map<DeleteExpenseCommandResultDto>(expense);

			return DeleteExpenseCommandResult.Succeeded(resultDTO);
		}
		catch (Exception ex)
		{
			var error = new Error(ex.Message);

			return DeleteExpenseCommandResult.Failed(error);
		}
	}


	async Task PublishExpenseDeletedEvent(Domain.Entities.Expense expense)
	{
		var eventDetails = new DomainEventDetails(nameof(ExpenseDeletedEvent), expense.User.Id);
		var eventData    = new ExpenseDeletedEventData(expense.Id, expense.Amount, expense.Description, expense.Date, expense.Category.Id, expense.User.Id, expense.CreatedAt, expense.CreatedBy, expense.LastUpdatedAt, expense.LastUpdatedBy, expense.IsDeleted, expense.DeletedAt);
		var @event       = ExpenseDeletedEvent.Create(eventDetails, eventData);

		await _mediator.Publish(@event);
	}
}