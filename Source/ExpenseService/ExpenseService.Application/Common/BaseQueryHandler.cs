namespace ExpenseService.Application.Common;

/// <summary>
/// Base class for query handlers that implements IRequestHandler interface.
/// </summary>
/// <typeparam name="TQuery">The type of the query.</typeparam>
/// <typeparam name="TResponse">The type of the response.</typeparam>
public abstract class BaseQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, TResponse> where TQuery : IRequest<TResponse>
{
	protected readonly IMapper _mapper;

	protected BaseQueryHandler(IMapper mapper)
	{
		_mapper = mapper;
	}

	public virtual async Task<TResponse> Handle(TQuery request, CancellationToken cancellationToken)
	{
		return await Task.FromResult(default(TResponse));
	}
}
