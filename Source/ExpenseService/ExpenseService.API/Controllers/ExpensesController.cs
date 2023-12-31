using AutoMapper;
using ExpenseService.Application.Expense.Queries;
using ExpenseService.Application.Features.Expense.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExpensesController : ControllerBase
{
	private readonly IMediator _mediator;
	private readonly IMapper _mapper;

	public ExpensesController(IMediator mediator, IMapper mapper)
	{
		_mediator = mediator;
		_mapper   = mapper;
	}


	[HttpGet(nameof(GetAllExpenses))]
	public async Task<IActionResult> GetAllExpenses()
	{
		var queryResult = await _mediator.Send(new GetExpensesQuery());

		if(queryResult.IsFailure)
		{
			return BadRequest(queryResult.Error);
		}

		return Ok(queryResult.Value);
	}


	[HttpPost(nameof(CreateExpense))]
	public async Task<IActionResult> CreateExpense([FromBody] CreateExpenseCommand command)
	{
		var commandResult = await _mediator.Send(command);

		if(commandResult.IsFailure)
		{
			return BadRequest(commandResult.Error.Message);
		}

		return Ok(commandResult.Value);
	}
}
