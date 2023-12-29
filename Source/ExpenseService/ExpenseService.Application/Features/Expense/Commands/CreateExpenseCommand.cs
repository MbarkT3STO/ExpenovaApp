using ExpenseService.Application.ApplicationServices;
using ExpenseService.Application.Extensions;
using ExpenseService.Domain.Entities;
using ExpenseService.Domain.Specifications.ExpenseSpecifications.Composite;
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

public class MappingProfile: Profile
{
	public MappingProfile()
	{
		CreateMap<Domain.Entities.Expense, CreateExpenseCommandResultDto>();
	}
}


public class CreateExpenseCommandResult: CommandResult<CreateExpenseCommandResultDto, CreateExpenseCommandResult>
{
	public CreateExpenseCommandResult(CreateExpenseCommandResultDto data): base(data)
	{
	}

	public CreateExpenseCommandResult(Error error): base(error)
	{
	}
}



public record CreateExpenseCommand(decimal Amount, string Description, DateTime Date, Guid CategoryId, string UserId): IRequest<CreateExpenseCommandResult>;

public class CreateExpenseCommandHandler: BaseCommandHandler<CreateExpenseCommand, CreateExpenseCommandResult, CreateExpenseCommandResultDto>
{
	readonly IExpenseRepository _expenseRepository;
	readonly ApplicationExpenseService _expenseService;
	readonly UserService _userService;
	readonly ApplicationCategoryService _categoryService;

	public CreateExpenseCommandHandler(IMapper mapper, IMediator mediator, IExpenseRepository expenseRepository, ApplicationExpenseService expenseService, UserService userService, ApplicationCategoryService categoryService): base(mediator, mapper )
	{
		_expenseRepository = expenseRepository;
		_expenseService    = expenseService;
		_userService       = userService;
		_categoryService   = categoryService;
	}

	public override async Task<CreateExpenseCommandResult> Handle(CreateExpenseCommand request, CancellationToken cancellationToken)
	{
		try
		{
			var user     = await _userService.GetUserOrThrowExceptionIfNotExistsAsync(request.UserId);
			var category = await _categoryService.GetCategoryOrThrowExceptionIfNotExistsAsync(request.CategoryId, request.UserId);
			var expense  = CreateAndAuditExpense(request, category, user);

			expense.Validate(new IsValidExpenseForCreateSpecification());

			await _expenseRepository.AddAsync(expense);

			var expenseDto = _mapper.Map<CreateExpenseCommandResultDto>(expense);
			var result     = CreateExpenseCommandResult.Succeeded(expenseDto);

			return result;
		}
		catch (Exception ex)
		{
			var error = new Error(ex.Message);

			return CreateExpenseCommandResult.Failed(error);
		}
	}

	private Domain.Entities.Expense CreateAndAuditExpense(CreateExpenseCommand request, Domain.Entities.Category category, User user)
	{
		var expense = new Domain.Entities.Expense(request.Amount, request.Date, request.Description, category, user);

		expense.WriteCreatedAudit(user.Id);

		return expense;
	}
}