
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
	

	/// <summary>
	/// Validates the specifications for the given entity.
	/// </summary>
	/// <typeparam name="TEntity">The type of the entity.</typeparam>
	/// <param name="entity">The entity to validate.</param>
	/// <param name="specifications">The specifications to validate against.</param>
	/// <returns>A task representing the asynchronous operation.</returns>
	protected virtual Task ValidateSpecificationsAsync<TEntity>(TEntity entity, params ISpecification<TEntity>[] specifications) where TEntity : class
	{
		var errors = new List<Error>();
		
		foreach (var specification in specifications)
		{
			var satisfactionResult = specification.IsSatisfiedBy(entity);
			
			if (!satisfactionResult.IsSatisfied)
			{
				errors.AddRange(satisfactionResult.Errors);
			}
		}
		
		if (errors.Any())
		{
			var exception = new SpecificationException(errors);
			throw exception;
		}
		
		return Task.CompletedTask;
	}



	/// <summary>
	/// Validates the specifications for the given entity.
	/// </summary>
	/// <typeparam name="TEntity">The type of the entity.</typeparam>
	/// <param name="entity">The entity to validate.</param>
	/// <param name="specifications">The specifications to validate against.</param>
	/// <returns>A task representing the asynchronous operation.</returns>
	/// <exception cref="SpecificationException">Thrown when one or more specifications are not satisfied.</exception>
	protected virtual Task ValidateSpecificationsAsync<TEntity>(TEntity entity, params ICompositeSpecification<TEntity>[] specifications) where TEntity : class
	{
		var errors = new List<Error>();

		foreach (var specification in specifications)
		{
			var satisfactionResult = specification.IsSatisfiedBy(entity);

			if (!satisfactionResult.IsSatisfied)
			{
				errors.AddRange(satisfactionResult.Errors);
			}
		}

		if (errors.Any())
		{
			var exception = new SpecificationException(errors);
			throw exception;
		}

		return Task.CompletedTask;
	}
}