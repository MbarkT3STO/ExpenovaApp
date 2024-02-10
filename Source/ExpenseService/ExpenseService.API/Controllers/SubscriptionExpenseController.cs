using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ExpenseService.Application.Features.SubscriptionExpense.Commands;
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
