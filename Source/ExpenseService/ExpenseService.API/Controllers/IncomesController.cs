using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ExpenseService.Application.Features.Income.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IncomesController : ControllerBase
{
	readonly IMediator _mediator;
	readonly IMapper _mapper;


	public IncomesController(IMediator mediator, IMapper mapper)
	{
		_mediator = mediator;
		_mapper   = mapper;
	}



	[HttpGet(nameof(GetIncomes))]
	public async Task<IActionResult> GetIncomes()
	{
		var query  = new GetIncomesQuery();
		var result = await _mediator.Send(query);

		if (result.IsFailure)
		{
			return BadRequest(result.Error?.Message);
		}

		return Ok(result.Value);
	}
}
