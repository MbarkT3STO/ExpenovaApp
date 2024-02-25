using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ExpenseService.Application.Features.User.Queries.Statistics;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StatisticsController: ControllerBase
{
	readonly IMediator _mediator;
	readonly IMapper _mapper;


	public StatisticsController(IMediator mediator, IMapper mapper)
	{
		_mediator = mediator;
		_mapper   = mapper;
	}




	[HttpGet(nameof(GetBasicStatistics))]
	public async Task<IActionResult> GetBasicStatistics(string userId)
	{
		var query  = new GetUserBasicStatisticsQuery(userId);
		var result = await _mediator.Send(query);

		if (result.IsFailure)
		{
			return BadRequest(result.Error?.Message);
		}

		return Ok(result.Value);
	}





	[HttpGet(nameof(GetExpensesSumGroupedByMonthAndYear))]
	public async Task<IActionResult> GetExpensesSumGroupedByMonthAndYear(string userId)
	{
		var query  = new GetExpensesSumGroupedByMonthAndYearQuery(userId);
		var result = await _mediator.Send(query);

		if (result.IsFailure)
		{
			return BadRequest(result.Error?.Message);
		}

		return Ok(result.Value);
	}


	[HttpGet(nameof(GetExpensesSumGroupedByYear))]
	public async Task<IActionResult> GetExpensesSumGroupedByYear(string userId)
	{
		var query  = new GetExpensesSumGroupedByYearQuery(userId);
		var result = await _mediator.Send(query);

		if (result.IsFailure)
		{
			return BadRequest(result.Error?.Message);
		}

		return Ok(result.Value);
	}






	[HttpGet(nameof(GeGetSubscriptionExpensesSumGroupedByYear))]
	public async Task<IActionResult> GeGetSubscriptionExpensesSumGroupedByYear(string userId)
	{
		var query  = new GetSubscriptionExpensesSumGroupedByYearQuery(userId);
		var result = await _mediator.Send(query);

		if (result.IsFailure)
		{
			return BadRequest(result.Error?.Message);
		}

		return Ok(result.Value);
	}
}