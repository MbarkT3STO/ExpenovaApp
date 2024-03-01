using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ExpenseService.Application.Features.Income.Commands;
using ExpenseService.Application.Features.Income.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IncomesController: ControllerBase
{
	readonly IMediator _mediator;
	readonly IMapper _mapper;


	public IncomesController(IMediator mediator, IMapper mapper)
	{
		_mediator = mediator;
		_mapper   = mapper;
	}



	[HttpGet(nameof(Get))]
	public async Task<IActionResult> Get()
	{
		var query  = new GetIncomesQuery();
		var result = await _mediator.Send(query);

		if (result.IsFailure)
		{
			return BadRequest(result.Error?.Message);
		}

		return Ok(result.Value);
	}


	[HttpGet(nameof(GetIncomeById))]
	public async Task<IActionResult> GetIncomeById(Guid id)
	{
		var query  = new GetIncomeByIdQuery(id);
		var result = await _mediator.Send(query);

		if (result.IsFailure)
		{
			return BadRequest(result.Error?.Message);
		}

		return Ok(result.Value);
	}


	[HttpGet(nameof(GetIncomesByUser))]
	public async Task<IActionResult> GetIncomesByUser(string userId)
	{
		var query  = new GetIncomesByUserQuery(userId);
		var result = await _mediator.Send(query);

		if (result.IsFailure)
		{
			return BadRequest(result.Error?.Message);
		}

		return Ok(result.Value);
	}






	[HttpPost(nameof(Create))]
	public async Task<IActionResult> Create([FromBody] CreateIncomeCommand command)
	{
		var result = await _mediator.Send(command);

		if (result.IsFailure)
		{
			return BadRequest(result.Error?.Message);
		}

		return Ok(result.Value);
	}




	[HttpPut(nameof(Update))]
	public async Task<IActionResult> Update([FromBody] UpdateIncomeCommand command)
	{
		var result = await _mediator.Send(command);

		if (result.IsFailure)
		{
			return BadRequest(result.Error?.Message);
		}

		return Ok(result.Value);
	}




	[HttpDelete(nameof(Delete))]
	public async Task<IActionResult> Delete(Guid id)
	{
		var command = new DeleteIncomeCommand(id);
		var result  = await _mediator.Send(command);

		if (result.IsFailure)
		{
			return BadRequest(result.Error?.Message);
		}

		return Ok(result.Value);
	}
}
