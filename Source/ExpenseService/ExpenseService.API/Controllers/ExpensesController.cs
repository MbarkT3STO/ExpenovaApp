using System.Security.Claims;
using AutoMapper;
using ExpenseService.Application.Expense.Queries;
using ExpenseService.Application.Features.Expense.Commands;
using ExpenseService.Application.Features.Expense.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExpensesController: ControllerBase
{
	private readonly IMediator _mediator;
	private readonly IMapper _mapper;

	public ExpensesController(IMediator mediator, IMapper mapper)
	{
		_mediator = mediator;
		_mapper   = mapper;
	}


	[HttpGet(nameof(GetAll))]
	public async Task<IActionResult> GetAll()
	{
		var queryResult = await _mediator.Send(new GetExpensesQuery());

		if (queryResult.IsFailure)
		{
			return BadRequest(queryResult.Error);
		}

		return Ok(queryResult.Value);
	}


	[HttpGet(nameof(GetByUser))]
	public async Task<IActionResult> GetByUser(string userId)
	{
		var query       = new GetExpensesByUserQuery(userId);
		var queryResult = await _mediator.Send(query);

		if (queryResult.IsFailure)
		{
			return BadRequest(queryResult.Error);
		}

		return Ok(queryResult.Value);
	}


	[HttpPost(nameof(Create))]
	public async Task<IActionResult> Create([FromBody] CreateExpenseCommand command)
	{
		var commandResult = await _mediator.Send(command);

		if (commandResult.IsFailure)
		{
			return BadRequest(commandResult.Error.Message);
		}

		return Ok(commandResult.Value);
	}


	[HttpGet(nameof(GetById))]
	public async Task<IActionResult> GetById([FromQuery] Guid id)
	{
		var queryResult = await _mediator.Send(new GetExpenseByIdQuery(id));

		if (queryResult.IsFailure)
		{
			return BadRequest(queryResult.Error);
		}

		return Ok(queryResult.Value);
	}


	[HttpPut(nameof(Update))]
	public async Task<IActionResult> Update([FromBody] UpdateExpenseCommand command)
	{
		var commandResult = await _mediator.Send(command);

		if (commandResult.IsFailure)
		{
			return BadRequest(commandResult.Error.Message);
		}

		return Ok(commandResult.Value);
	}


	[HttpDelete(nameof(Delete))]
	public async Task<IActionResult> Delete([FromQuery] Guid id)
	{
		var actionBy      = User.FindFirstValue(ClaimTypes.NameIdentifier);
		var command       = new DeleteExpenseCommand(id, actionBy);
		var commandResult = await _mediator.Send(command);

		if (commandResult.IsFailure)
		{
			return BadRequest(commandResult.Error.Message);
		}

		return Ok(commandResult.Value);
	}
}
