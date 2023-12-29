using ExpenseService.Application.ApplicationServices;
using ExpenseService.Application.Features.Expense.Commands.Shared;
using ExpenseService.Infrastructure.Data.Entities;

namespace ExpenseService.Application.Features.Expense.Commands;

public class CreateExpenseCommandResultDto
{
	public Guid Id { get; set; }
	public decimal Amount { get; set; }
	public string Description { get; set; }
	public DateTime Date { get; set; }
	public Guid CategoryId { get; set; }
	public string UserId { get; set; }
}

public class MappingProfile : Profile
{
	public MappingProfile()
	{
		CreateMap<ExpenseEntity, CreateExpenseCommandResultDto>();
	}
}


public class CreateExpenseCommandResult : CommandResult<CreateExpenseCommandResultDto, CreateExpenseCommandResult>
{
	public CreateExpenseCommandResult(CreateExpenseCommandResultDto data) : base(data)
	{
	}

	public CreateExpenseCommandResult(Error error) : base(error)
	{
	}
}



public record CreateExpenseCommand(decimal Amount, string Description, DateTime Date, Guid CategoryId, string UserId) : IRequest<CreateExpenseCommandResult>;

public class CreateExpenseCommandHandler : ExpenseCommandHandler<CreateExpenseCommand, CreateExpenseCommandResult, CreateExpenseCommandResultDto>
{
	private readonly IExpenseRepository _expenseRepository;

	public CreateExpenseCommandHandler(IMapper mapper, IMediator mediator, IExpenseRepository expenseRepository, ApplicationExpenseService expenseService, UserService userService) : base(mapper, mediator, expenseService, userService)
	{
		_expenseRepository = expenseRepository;
	}

	public override async Task<CreateExpenseCommandResult> Handle(CreateExpenseCommand request, CancellationToken cancellationToken)
	{


	}
}