
using ExpenseService.Application.ApplicationServices;

namespace ExpenseService.Application.Features.Expense.Commands.Shared;

public class ExpenseCommandHandler<TCommand, TResult, TResultDTO> : BaseCommandHandler<TCommand, TResult, TResultDTO> where TCommand : IRequest<TResult> where TResult : ICommandResult<TResultDTO>
{
	protected readonly IExpenseRepository _expenseRepository;
	protected readonly ApplicationExpenseService _expenseService;
	protected readonly ApplicationCategoryService _categoryService;
	protected readonly UserService _userService;

	public ExpenseCommandHandler( IMapper mapper, IMediator mediator, IExpenseRepository expenseRepository, ApplicationExpenseService expenseService, ApplicationCategoryService categoryService, UserService userService) : base(mediator, mapper)
	{
		_expenseRepository = expenseRepository;
		_expenseService    = expenseService;
		_categoryService   = categoryService;
		_userService       = userService;
	}
}
