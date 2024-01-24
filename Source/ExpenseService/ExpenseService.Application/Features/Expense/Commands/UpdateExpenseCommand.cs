using ExpenseService.Application.ApplicationServices;
using ExpenseService.Application.Extensions;
using ExpenseService.Application.Features.Expense.Commands.Shared;

namespace ExpenseService.Application.Features.Expense.Commands;

public class UpdateExpenseCommandResultDto
{
	public decimal Amount { get; private set; }
	public DateTime Date { get; private set; }
	public string Description { get; set; }
	public Guid CategoryId { get; set; }
	public string UserId { get; set; }
	public DateTime CreatedAt { get; set; }
	public string CreatedBy { get; set; }
	public DateTime? LastUpdatedAt { get; set; }
	public string LastUpdatedBy { get; set; }
	public bool IsDeleted { get; set; }
	public DateTime? DeletedAt { get; set; }
	public string DeletedBy { get; set; }
}


public class UpdateExpenseCommandResult: CommandResult<UpdateExpenseCommandResultDto, UpdateExpenseCommandResult>
{
	public UpdateExpenseCommandResult(UpdateExpenseCommandResultDto data): base(data)
	{
	}

	public UpdateExpenseCommandResult(Error error): base(error)
	{
	}
}


public class UpdateExpenseCommandMappingProfile: Profile
{
	public UpdateExpenseCommandMappingProfile()
	{
		CreateMap<Domain.Entities.Expense, UpdateExpenseCommandResultDto>();
	}
}


public class UpdateExpenseCommand: IRequest<UpdateExpenseCommandResult>
{
	public Guid Id { get; private set; }
	public decimal Amount { get; private set; }
	public DateTime Date { get; private set; }
	public string Description { get; set; }
	public Guid CategoryId { get; set; }
	public string UserId { get; set; }
	public string UpdatedBy { get; set; }

	public UpdateExpenseCommand(Guid id, decimal amount, DateTime date, string description, Guid categoryId, string userId, string updatedBy)
	{
		Id          = id;
		Amount      = amount;
		Date        = date;
		Description = description;
		CategoryId  = categoryId;
		UserId      = userId;
		UpdatedBy   = updatedBy;
	}
}


public class UpdateExpenseCommandHandler: ExpenseCommandHandler<UpdateExpenseCommand, UpdateExpenseCommandResult, UpdateExpenseCommandResultDto>
{
	protected UpdateExpenseCommandHandler(IMapper mapper, IMediator mediator, IExpenseRepository expenseRepository, ApplicationExpenseService expenseService, ApplicationCategoryService categoryService, UserService userService) : base(mapper, mediator, expenseRepository, expenseService, categoryService, userService)
	{
	}

	public override async Task<UpdateExpenseCommandResult> Handle(UpdateExpenseCommand request, CancellationToken cancellationToken)
	{
		try
		{
			var expense  = await _expenseService.GetExpenseByIdOrThrowAsync(request.Id);
			var user     = await _userService.GetUserByIdOrThrowAsync(request.UserId);
			var category = await _categoryService.GetCategoryOrThrowAsync(request.CategoryId, request.UserId);

			expense.WriteUpdatedAudit(request.UpdatedBy);

			await _expenseService.UpdateAsync(expense);

			var expenseDto = _mapper.Map<UpdateExpenseCommandResultDto>(expense);

			return UpdateExpenseCommandResult.Succeeded(expenseDto);
		}
		catch (Exception e)
		{
			var error = new Error(e.Message);

			return UpdateExpenseCommandResult.Failed(error);
		}
	}
}