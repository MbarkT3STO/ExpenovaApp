using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExpenseService.Application.Extensions;
using ExpenseService.Domain.Enums;
using ExpenseService.Domain.Specifications.SubscriptionExpenseSpecifications.Composite;

namespace ExpenseService.Application.Features.SubscriptionExpense.Commands;

public class UpdateSubscriptionExpenseCommandResultDTO: AuditableDTO
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
/// Represents the result of the UpdateSubscriptionExpenseCommand.
/// </summary>
public class UpdateSubscriptionExpenseCommandResult: CommandResult<UpdateSubscriptionExpenseCommandResultDTO, UpdateSubscriptionExpenseCommandResult>
{
	public UpdateSubscriptionExpenseCommandResult(UpdateSubscriptionExpenseCommandResultDTO value): base(value)
	{
	}

	public UpdateSubscriptionExpenseCommandResult(Error error): base(error)
	{
	}
}


public class UpdateSubscriptionExpenseCommandMappingProfile: Profile
{
	public UpdateSubscriptionExpenseCommandMappingProfile()
	{
		CreateMap<Domain.Entities.SubscriptionExpense, UpdateSubscriptionExpenseCommandResultDTO>();
	}
}




/// <summary>
/// Represents a command to update a subscription expense.
/// </summary>
public class UpdateSubscriptionExpenseCommand: IRequest<UpdateSubscriptionExpenseCommandResult>
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

	public UpdateSubscriptionExpenseCommand(Guid id, string description, decimal amount, string userId, Guid categoryId, DateTime startDate, DateTime endDate, RecurrenceInterval recurrenceInterval, decimal billingAmount)
	{
		Id                 = id;
		Description        = description;
		Amount             = amount;
		UserId             = userId;
		CategoryId         = categoryId;
		StartDate          = startDate;
		EndDate            = endDate;
		RecurrenceInterval = recurrenceInterval;
		BillingAmount      = billingAmount;
	}
}


public class UpdateSubscriptionExpenseCommandHandler: BaseCommandHandler<UpdateSubscriptionExpenseCommand, UpdateSubscriptionExpenseCommandResult, UpdateSubscriptionExpenseCommandResultDTO>
{
	readonly IUserRepository _userRepository;
	readonly ICategoryRepository _categoryRepository;
	readonly ISubscriptionExpenseRepository _subscriptionExpenseRepository;

	public UpdateSubscriptionExpenseCommandHandler(IMediator mediator, IMapper mapper, IUserRepository userRepository, ICategoryRepository categoryRepository, ISubscriptionExpenseRepository subscriptionExpenseRepository): base(mediator, mapper)
	{
		_userRepository                = userRepository;
		_categoryRepository            = categoryRepository;
		_subscriptionExpenseRepository = subscriptionExpenseRepository;
	}

	public override async Task<UpdateSubscriptionExpenseCommandResult> Handle(UpdateSubscriptionExpenseCommand request, CancellationToken cancellationToken)
	{
		try
		{
			var user     = await _userRepository.GetByIdOrThrowAsync(request.UserId, cancellationToken);
			var category = await _categoryRepository.GetByIdOrThrowAsync(request.CategoryId, cancellationToken);

			var expense = await _subscriptionExpenseRepository.GetByIdOrThrowAsync(request.Id, cancellationToken);

			expense.Update(request.Amount, request.Description, request.StartDate, request.EndDate, request.RecurrenceInterval, request.BillingAmount, category, user);
			expense.WriteUpdatedAudit(user.Id);


			await _subscriptionExpenseRepository.UpdateAsync(expense, cancellationToken);

			var resultDTO = _mapper.Map<UpdateSubscriptionExpenseCommandResultDTO>(expense);
			var result    = UpdateSubscriptionExpenseCommandResult.Succeeded(resultDTO);

			return result;
		}
		catch (Exception ex)
		{
			return UpdateSubscriptionExpenseCommandResult.Failed(new Error(ex.Message));
		}
	}



	async Task ApplyUpdateAsync(Domain.Entities.SubscriptionExpense expense)
	{
		// Validate the expense for update
		expense.Validate(new IsValidSubscriptionExpenseForUpdateSpecification());

		// Update the expense
		await _subscriptionExpenseRepository.UpdateAsync(expense);
	}

}