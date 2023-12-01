
using ExpenseService.Domain.Shared.Interfaces;

namespace ExpenseService.Application.Common;

/// <summary>
/// Represents a base class for command handlers.
/// </summary>
/// <typeparam name="TCommand">The type of the command.</typeparam>
/// <typeparam name="TCommandResult">The type of the command result.</typeparam>
public abstract class BaseCommandHandler<TCommand, TCommandResult> : IRequestHandler<TCommand, TCommandResult> where TCommand : IRequest<TCommandResult> where TCommandResult : ICommandResult
{
	private protected readonly IMediator _mediator;
	private protected readonly IMapper _mapper;
	
	
	protected BaseCommandHandler(IMediator mediator, IMapper mapper)
	{
		_mediator = mediator;
		_mapper   = mapper;
	}
	
	public Task<TCommandResult> Handle(TCommand request, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}
}



/// <summary>
/// Represents a base class for command handlers.
/// </summary>
/// <typeparam name="TCommand">The type of the command.</typeparam>
/// <typeparam name="TCommandResult">The type of the command result.</typeparam>
/// <typeparam name="TCommandResultValue">The type of the command result value.</typeparam>
public abstract class BaseCommandHandler<TCommand, TCommandResult, TCommandResultValue> : IRequestHandler<TCommand, TCommandResult> where TCommand : IRequest<TCommandResult> where TCommandResult : ICommandResult<TCommandResultValue>
{
	private protected readonly IMediator _mediator;
	private protected readonly IMapper _mapper;


	protected BaseCommandHandler(IMediator mediator, IMapper mapper)
	{
		_mediator = mediator;
		_mapper = mapper;
	}


	public abstract Task<TCommandResult> Handle(TCommand request, CancellationToken cancellationToken);


	public virtual CommandResult<TCommandResult> ApplySpecification<TEntity>(ISpecification<TEntity> specification, TEntity entity) where TEntity : class
	{
		var satisfactionResult = specification.IsSatisfiedBy(entity);

		if (!satisfactionResult.IsSatisfied)
		{
			var error = satisfactionResult.Errors.First();
			return CommandResult<TCommandResult>.Failed(error);
		}

	}
}