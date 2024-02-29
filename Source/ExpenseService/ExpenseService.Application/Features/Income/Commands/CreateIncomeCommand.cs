using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExpenseService.Application.Extensions;
using ExpenseService.Domain.Entities;
using ExpenseService.Domain.Specifications.IncomeSpecifications.Composite;

namespace ExpenseService.Application.Features.Income.Commands;

public class CreateIncomeCommandResultDTO: AuditableDTO
{
	public Guid Id { get; set; }
	public string Description { get; set; }
	public DateTime Date { get; set; }
	public decimal Amount { get; set; }
	public Guid CategoryId { get; set; }
	public string UserId { get; set; }
}

public class CreateIncomeCommandResult: CommandResult<CreateIncomeCommandResultDTO, CreateIncomeCommandResult>
{
	public CreateIncomeCommandResult(CreateIncomeCommandResultDTO data): base(data)
	{
	}

	public CreateIncomeCommandResult(Error error): base(error)
	{
	}
}

public class CreateIncomeCommandMapperProfile: Profile
{
	public CreateIncomeCommandMapperProfile()
	{
		CreateMap<Domain.Entities.Income, CreateIncomeCommandResultDTO>();
	}
}



/// <summary>
/// Command to create an income.
/// </summary>
public class CreateIncomeCommand: IRequest<CreateIncomeCommandResult>
{
	public string Description { get; set; }
	public DateTime Date { get; set; }
	public decimal Amount { get; set; }
	public Guid CategoryId { get; set; }
	public string UserId { get; set; }
}

public class CreateIncomeCommandHandler: BaseCommandHandler<CreateIncomeCommand, CreateIncomeCommandResult, CreateIncomeCommandResultDTO>
{
	readonly IUserRepository _userRepository;
	readonly ICategoryRepository _categoryRepository;
	readonly IIncomeRepository _incomeRepository;

	public CreateIncomeCommandHandler(IMediator mediator, IMapper mapper, IUserRepository userRepository, ICategoryRepository categoryRepository, IIncomeRepository incomeRepository): base(mediator, mapper)
	{
		_userRepository     = userRepository;
		_categoryRepository = categoryRepository;
		_incomeRepository   = incomeRepository;
	}

	public override async Task<CreateIncomeCommandResult> Handle(CreateIncomeCommand request, CancellationToken cancellationToken)
	{
		try
		{
			var user     = await _userRepository.GetByIdOrThrowAsync(request.UserId, cancellationToken);
			var category = await _categoryRepository.GetByIdOrThrowAsync(request.CategoryId, cancellationToken);
			var income   = CreateAndAuditIncome(request, category, user);

			income.Validate(new IsValidIncomeForCreateSpecification());

			var createdIncome = await _incomeRepository.AddAsync(income, cancellationToken);
			var resultDTO     = _mapper.Map<CreateIncomeCommandResultDTO>(createdIncome);
			var result        = CreateIncomeCommandResult.Succeeded(resultDTO);

			await PublishIncomeCreatedEventAsync(createdIncome);

			return result;
		}
		catch (Exception ex)
		{
			return CreateIncomeCommandResult.Failed(ex);
		}
	}


	/// <summary>
	/// Creates a new income entity and performs auditing.
	/// </summary>
	/// <param name="request">The command request containing the income details.</param>
	/// <param name="category">The category associated with the income.</param>
	/// <param name="user">The user associated with the income.</param>
	/// <returns>The newly created income entity.</returns>
	private Domain.Entities.Income CreateAndAuditIncome(CreateIncomeCommand request, Domain.Entities.Category category, Domain.Entities.User user)
	{
		var income = new Domain.Entities.Income(request.Description, request.Date, request.Amount, category, user);
		income.WriteCreatedAudit(user.Id);

		return income;
	}


	private async Task PublishIncomeCreatedEventAsync(Domain.Entities.Income income)
	{
		var eventData    = new IncomeCreatedEventData(income.Id, income.Description, income.Amount, income.Date, income.Category.Id, income.User.Id);
		var eventDetails = new DomainEventDetails(nameof(IncomeCreatedEvent), income.User.Id);
		var @event       = IncomeCreatedEvent.Create(eventDetails, eventData);

		await _mediator.Publish(@event);
	}
}