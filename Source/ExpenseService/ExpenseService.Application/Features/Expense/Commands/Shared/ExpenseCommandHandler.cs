
using ExpenseService.Application.ApplicationServices;

namespace ExpenseService.Application.Features.Expense.Commands.Shared;

public class ExpenseCommandHandler<TCommand, TResult, TResultDTO> : BaseCommandHandler<TCommand, TResult, TResultDTO> where TCommand : IRequest<TResult> where TResult : ICommandResult<TResultDTO>
{
	readonly ExpenseRepository _expenseRepository;
	readonly ApplicationExpenseService _expenseService;
	readonly UserService _userService;

	protected ExpenseCommandHandler( IMapper mapper, IMediator mediator, ApplicationExpenseService expenseService, UserService userService) : base(mediator, mapper)
	{
		_expenseService = expenseService;
		_userService = userService;
	}
}
