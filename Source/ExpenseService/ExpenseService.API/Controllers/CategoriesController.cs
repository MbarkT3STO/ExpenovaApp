using AutoMapper;
using ExpenseService.Application.Category.Commands;
using ExpenseService.Application.Category.Queries;
using ExpenseService.Application.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController: ControllerBase
{
	private readonly IMediator _mediator;
	private readonly IMapper _mapper;
	
	public CategoriesController(IMediator mediator, IMapper mapper)
	{
		_mediator = mediator;
		_mapper   = mapper;
	}
	
	[HttpGet]
	public async Task<IActionResult> Get()
	{
		var queryResult = await _mediator.Send(new GetCategoriesQuery());

		if(queryResult.IsFailure)
		{
			return BadRequest(queryResult.Error);
		}
		
		return Ok(queryResult.Value);
	}
	
	
	[HttpGet("{id}")]
	public async Task<IActionResult> Get(Guid id)
	{
		var queryResult = await _mediator.Send(new GetCategoryByIdQuery(id));

		if(queryResult.IsFailure)
		{
			return BadRequest(queryResult.Error);
		}
		
		return Ok(queryResult.Value);
	}
	
	
	[HttpPost]
	public async Task<IActionResult> Post([FromBody] CreateCategoryCommand command)
	{
		var commandResult = await _mediator.Send(command);
		
		if(commandResult.IsFailure)
		{
			return BadRequest(commandResult.Error.Message);
		}
		
		return Ok(commandResult.Value);
	}
	
	
	[HttpPut("{id}")]
	public async Task<IActionResult> Put(Guid id, [FromBody] UpdateCategoryCommand command)
	{
		var commandResult = await _mediator.Send(command);
		
		if(commandResult.IsFailure)
		{
			return BadRequest(commandResult.Error.Message);
		}
		
		return Ok(commandResult.Value);
	}
	
	
	[HttpGet("GetCategoriesByUserId/{userId}")]
	public async Task<IActionResult> GetCategoriesByUserId(string userId)
	{
		var query       = new GetCategoriesByUserIdQuery(userId);
		var queryResult = await _mediator.Send(query);

		if(queryResult.IsFailure)
		{
			return BadRequest(queryResult.Error);
		}
		
		return Ok(queryResult.Value);
	}
}
