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


	[HttpGet(nameof(GetTopExpenseCategory))]
	public async Task<IActionResult> GetTopExpenseCategory(string userId)
	{
		var query  = new GetTopExpenseCategoryQuery(userId);
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


	[HttpGet(nameof(GetTopSubscriptionExpenseCategory))]
	public async Task<IActionResult> GetTopSubscriptionExpenseCategory(string userId)
	{
		var query  = new GetTopSubscriptionExpenseCategoryQuery(userId);
		var result = await _mediator.Send(query);

		if (result.IsFailure)
		{
			return BadRequest(result.Error?.Message);
		}

		return Ok(result.Value);
	}






	[HttpGet(nameof(GetIncomesSumGroupedByMonthAndYear))]
	public async Task<IActionResult> GetIncomesSumGroupedByMonthAndYear(string userId)
	{
		var query  = new GetIncomesSumGroupedByMonthAndYearQuery(userId);
		var result = await _mediator.Send(query);

		if (result.IsFailure)
		{
			return BadRequest(result.Error?.Message);
		}

		return Ok(result.Value);
	}


	[HttpGet(nameof(GetIncomesSumGroupedByYear))]
	public async Task<IActionResult> GetIncomesSumGroupedByYear(string userId)
	{
		var query  = new GetIncomesSumGroupedByYearQuery(userId);
		var result = await _mediator.Send(query);

		if (result.IsFailure)
		{
			return BadRequest(result.Error?.Message);
		}

		return Ok(result.Value);
	}


	[HttpGet(nameof(GetIncomesSumGroupedByCategory))]
	public async Task<IActionResult> GetIncomesSumGroupedByCategory(string userId)
	{
		var query  = new GetIncomesSumGroupedByCategoryQuery(userId);
		var result = await _mediator.Send(query);

		if (result.IsFailure)
		{
			return BadRequest(result.Error?.Message);
		}

		return Ok(result.Value);
	}


	[HttpGet(nameof(GetTopIncomeCategory))]
	public async Task<IActionResult> GetTopIncomeCategory(string userId)
	{
		var query  = new GetTopIncomeCategoryQuery(userId);
		var result = await _mediator.Send(query);

		if (result.IsFailure)
		{
			return BadRequest(result.Error?.Message);
		}

		return Ok(result.Value);
	}
}