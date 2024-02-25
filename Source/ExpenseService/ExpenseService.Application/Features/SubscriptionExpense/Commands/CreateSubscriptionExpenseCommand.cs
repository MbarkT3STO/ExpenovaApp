using ExpenseService.Application.Extensions;
using ExpenseService.Domain.Entities;
using ExpenseService.Domain.Enums;

namespace ExpenseService.Application.Features.SubscriptionExpense.Commands;

public class CreateSubscriptionExpenseCommandResultDTO
{
	public Guid Id { get; private set; }
	public string Description { get; private set; }
	public decimal Amount { get; private set; }
	public string UserId { get; private set; }
	public Guid CategoryId { get; private set; }
	public DateTime StartDate { get; private set; }
	public DateTime EndDate { get; private set; }
	public RecurrenceInterval RecurrenceInterval { get; private set; }
	public decimal BillingAmount { get; private set; }


	public DateTime CreatedAt { get; set; }
	public string CreatedBy { get; set; }
	public DateTime? LastUpdatedAt { get; set; }
	public string LastUpdatedBy { get; set; }
	public bool IsDeleted { get; set; }
	public DateTime? DeletedAt { get; set; }
	public string? DeletedBy { get; set; }
}

/// <summary>
/// Represents the result of the <see cref="CreateSubscriptionExpenseCommand"/>.
/// </summary>
public class CreateSubscriptionExpenseCommandResult: CommandResult<CreateSubscriptionExpenseCommandResultDTO, CreateSubscriptionExpenseCommandResult>
{
	public CreateSubscriptionExpenseCommandResult(CreateSubscriptionExpenseCommandResultDTO value): base(value)
	{
	}

	public CreateSubscriptionExpenseCommandResult(Error error): base(error)
	{
	}
}

public class MappingProfile: Profile
{
	public MappingProfile()
	{
		CreateMap<Domain.Entities.SubscriptionExpense, CreateSubscriptionExpenseCommandResultDTO>();
	}
}


/// <summary>
/// Represents a command to create a subscription expense.
/// </summary>
public class CreateSubscriptionExpenseCommand: IRequest<CreateSubscriptionExpenseCommandResult>
{
	public decimal Amount { get; set; }
	public string Description { get; set; }
	public DateTime StartDate { get; set; }
	public DateTime EndDate { get; set; }
	public RecurrenceInterval RecurrenceInterval { get; set; }
	public decimal BillingAmount { get; set; }

	public Guid CategoryId { get; set; }
	public string UserId { get; set; }
}

public class CreateSubscriptionExpenseCommandHandler: BaseCommandHandler<CreateSubscriptionExpenseCommand, CreateSubscriptionExpenseCommandResult, CreateSubscriptionExpenseCommandResultDTO>
{
	readonly IUserRepository _userRepository;
	readonly ICategoryRepository _categoryRepository;
	readonly IExpenseRepository _expenseRepository;
	readonly ISubscriptionExpenseRepository _subscriptionExpenseRepository;


	public CreateSubscriptionExpenseCommandHandler(IMediator mediator, IMapper mapper, IUserRepository userRepository, ICategoryRepository categoryRepository, IExpenseRepository expenseRepository, ISubscriptionExpenseRepository subscriptionExpenseRepository): base(mediator, mapper)
	{
		_userRepository                = userRepository;
		_categoryRepository            = categoryRepository;
		_expenseRepository             = expenseRepository;
		_subscriptionExpenseRepository = subscriptionExpenseRepository;
	}

	public override async Task<CreateSubscriptionExpenseCommandResult> Handle(CreateSubscriptionExpenseCommand request, CancellationToken cancellationToken)
	{
		try
		{
			var user     = await _userRepository.GetByIdOrThrowAsync(request.UserId, cancellationToken);
			var category = await _categoryRepository.GetByIdOrThrowAsync(request.CategoryId, cancellationToken);

			var subscriptionExpense = CreateAndAuditSubscriptionExpense(request, user, category);

			var createdSubscriptionExpense = await _subscriptionExpenseRepository.AddAsync(subscriptionExpense, cancellationToken);
			var resultDTO                  = _mapper.Map<CreateSubscriptionExpenseCommandResultDTO>(createdSubscriptionExpense);

			await PublishSubscriptionExpenseCreatedEvent(createdSubscriptionExpense, cancellationToken);

			return CreateSubscriptionExpenseCommandResult.Succeeded(resultDTO);
		}
		catch (Exception ex)
		{
			var error = new Error(ex.Message);

			return CreateSubscriptionExpenseCommandResult.Failed(error);
		}
	}


	/// <summary>
	/// Creates a new subscription expense and audits the creation.
	/// </summary>
	/// <param name="request">The command request containing the expense details.</param>
	/// <param name="user">The user creating the expense.</param>
	/// <param name="category">The category of the expense.</param>
	/// <returns>The created subscription expense.</returns>
	Domain.Entities.SubscriptionExpense CreateAndAuditSubscriptionExpense(CreateSubscriptionExpenseCommand request, Domain.Entities.User user, Domain.Entities.Category category)
	{
		var subscriptionExpense = new Domain.Entities.SubscriptionExpense(request.Amount, request.Description, request.StartDate, request.EndDate, request.RecurrenceInterval, request.BillingAmount, category, user);

		subscriptionExpense.WriteCreatedAudit(user.Id);

		return subscriptionExpense;
	}


	/// <summary>
	/// Publishes a subscription expense created event.
	/// </summary>
	/// <param name="subscriptionExpense">The subscription expense entity.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>A task representing the asynchronous operation.</returns>
	private async Task PublishSubscriptionExpenseCreatedEvent(Domain.Entities.SubscriptionExpense subscriptionExpense, CancellationToken cancellationToken)
	{
		var eventDetails = new DomainEventDetails(nameof(SubscriptionExpenseCreatedEvent), subscriptionExpense.User.Id);
		var eventData    = new SubscriptionExpenseCreatedEventData(subscriptionExpense.Id, subscriptionExpense.Description, subscriptionExpense.Amount, subscriptionExpense.User.Id, subscriptionExpense.Category.Id, subscriptionExpense.StartDate, subscriptionExpense.EndDate, subscriptionExpense.RecurrenceInterval, subscriptionExpense.BillingAmount, subscriptionExpense.CreatedAt, subscriptionExpense.CreatedBy, subscriptionExpense.LastUpdatedAt, subscriptionExpense.LastUpdatedBy, subscriptionExpense.IsDeleted, subscriptionExpense.DeletedAt, subscriptionExpense.DeletedBy);
		var @event       = SubscriptionExpenseCreatedEvent.Create(eventDetails, eventData);

		await _mediator.Publish(@event, cancellationToken);
	}
}