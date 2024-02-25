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
	readonly ICategoryRepository _categoryRepository;
	readonly IExpenseRepository _expenseRepository;
	readonly IUserRepository _userRepository;

	public CreateExpenseCommandHandler(IMapper mapper, IMediator mediator, ICategoryRepository categoryRepository, IExpenseRepository expenseRepository, IUserRepository userRepository): base(mediator, mapper)
	{
		_categoryRepository = categoryRepository;
		_expenseRepository  = expenseRepository;
		_userRepository     = userRepository;
	}


	public override async Task<CreateExpenseCommandResult> Handle(CreateExpenseCommand request, CancellationToken cancellationToken)
	{
		try
		{
			var user     = await _userRepository.GetByIdOrThrowAsync(request.UserId, cancellationToken);
			var category = await _categoryRepository.GetByIdAndUserOrThrowAsync(request.CategoryId, request.UserId, cancellationToken);
			var expense  = CreateAndAuditExpense(request, category, user);

			expense.Validate(new IsValidExpenseForCreateSpecification());

			await _expenseRepository.AddAsync(expense);
			await PublishExpenseCreatedEvent(expense);

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

	/// <summary>
	/// Creates a new expense and performs auditing.
	/// </summary>
	/// <param name="request">The create expense command request.</param>
	/// <param name="category">The category of the expense.</param>
	/// <param name="user">The user creating the expense.</param>
	/// <returns>The newly created expense.</returns>
	private Domain.Entities.Expense CreateAndAuditExpense(CreateExpenseCommand request, Domain.Entities.Category category, Domain.Entities.User user)
	{
		var expense = new Domain.Entities.Expense(request.Amount, request.Date, request.Description, category, user);

		expense.WriteCreatedAudit(user.Id);

		return expense;
	}


	/// <summary>
	/// Publishes the ExpenseCreatedEvent asynchronously.
	/// </summary>
	/// <param name="expense">The expense entity.</param>
	/// <returns>A task representing the asynchronous operation.</returns>
	private async Task PublishExpenseCreatedEvent(Domain.Entities.Expense expense)
	{
		var eventDetails = new DomainEventDetails(nameof(ExpenseCreatedEvent), expense.User.Id);
		var eventData    = new ExpenseCreatedEventData(expense.Id, expense.Amount, expense.Description, expense.Date, expense.Category.Id, expense.User.Id);
		var @event       = ExpenseCreatedEvent.Create(eventDetails, eventData);

		await _mediator.Publish(@event);
	}
}