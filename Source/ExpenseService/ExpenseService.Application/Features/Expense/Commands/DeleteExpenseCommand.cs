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

	public DeleteExpenseCommandHandler(IExpenseRepository expenseRepository, ApplicationExpenseService expenseService, IMapper mapper)
	{
		_expenseRepository  = expenseRepository;
		this.expenseService = expenseService;
		_mapper             = mapper;
	}

	public async Task<DeleteExpenseCommandResult> Handle(DeleteExpenseCommand request, CancellationToken cancellationToken)
	{
		try
		{
			var expense = await expenseService.GetExpenseByIdOrThrowAsync(request.Id);

			expense.WriteDeletedAudit(request.DeletedBy);
			await expenseService.ApplySoftDeleteAsync(expense);

			var resultDTO = _mapper.Map<DeleteExpenseCommandResultDto>(expense);

			return DeleteExpenseCommandResult.Succeeded(resultDTO);
		}
		catch (Exception ex)
		{
			var error = new Error(ex.Message);

			return DeleteExpenseCommandResult.Failed(error);
		}
	}
}