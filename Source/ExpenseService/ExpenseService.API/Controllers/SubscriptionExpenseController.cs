using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ExpenseService.Application.Features.SubscriptionExpense.Commands;
using ExpenseService.Application.Features.SubscriptionExpense.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SubscriptionExpenseController : ControllerBase
{
	readonly IMapper _mapper;
	readonly IMediator _mediator;

	public SubscriptionExpenseController(IMediator mediator, IMapper mapper)
	{
		_mediator = mediator;
		_mapper   = mapper;
	}


	[HttpGet(nameof(GetAll))]
	public async Task<IActionResult> GetAll()
	{
		var result = await _mediator.Send(new GetSubscriptionExpensesQuery());

		if (result.IsFailure)
		{
			return BadRequest(result.Error);
		}

		return Ok(result.Value);
	}


	[HttpGet(nameof(GetById))]
	public async Task<IActionResult> GetById(Guid id)
	{
		var query = new GetSubscriptionExpenseByIdQuery(id);
		var result = await _mediator.Send(query);

		if (result.IsFailure)
		{
			return BadRequest(result.Error);
		}

		return Ok(result.Value);
	}


	[HttpGet(nameof(GetByCategory))]
	public async Task<IActionResult> GetByCategory(Guid categoryId)
	{
		var query = new GetSubscriptionExpensesByCategoryQuery(categoryId);
		var result = await _mediator.Send(query);

		if (result.IsFailure)
		{
			return BadRequest(result.Error);
		}

		return Ok(result.Value);
	}


	[HttpGet(nameof(GetByUser))]
	public async Task<IActionResult> GetByUser(string userId)
	{
		var query = new GetSubscriptionExpensesByUserQuery(userId);
		var result = await _mediator.Send(query);

		if (result.IsFailure)
		{
			return BadRequest(result.Error);
		}

		return Ok(result.Value);
	}




	[HttpPost(nameof(Create))]
	public async Task<IActionResult> Create(CreateSubscriptionExpenseCommand command)
	{
		var result = await _mediator.Send(command);

		if (result.IsFailure)
		{
			return BadRequest(result.Error);
		}

		return Ok(result.Value);
	}
}
