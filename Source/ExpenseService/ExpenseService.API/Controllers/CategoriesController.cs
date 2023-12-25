using AutoMapper;
using ExpenseService.Application.Category.Commands;
using ExpenseService.Application.Category.Queries;
using ExpenseService.Application.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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

	// [Authorize(Roles = "User")]
	[HttpGet(nameof(GetAllCategories))]
	public async Task<IActionResult> GetAllCategories()
	{
		var queryResult = await _mediator.Send(new GetCategoriesQuery());

		if(queryResult.IsFailure)
		{
			return BadRequest(queryResult.Error);
		}

		return Ok(queryResult.Value);
	}


	[HttpGet(nameof(GetCategoryById)+"/{id}")]
	public async Task<IActionResult> GetCategoryById(Guid id)
	{
		var queryResult = await _mediator.Send(new GetCategoryByIdQuery(id));

		if(queryResult.IsFailure)
		{
			return BadRequest(queryResult.Error);
		}

		return Ok(queryResult.Value);
	}


	[HttpPost(nameof(CreateCategory))]
	public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryCommand command)
	{
		var commandResult = await _mediator.Send(command);

		if(commandResult.IsFailure)
		{
			return BadRequest(commandResult.Error.Message);
		}

		return Ok(commandResult.Value);
	}


	[HttpPut(nameof(UpdateCategory)+"/{id}")]
	public async Task<IActionResult> UpdateCategory(Guid id, [FromBody] UpdateCategoryCommand command)
	{
		var commandResult = await _mediator.Send(command);

		if(commandResult.IsFailure)
		{
			return BadRequest(commandResult.Error.Message);
		}

		return Ok(commandResult.Value);
	}


	[HttpGet(nameof(GetCategoriesForUser)+"/{userId}")]
	public async Task<IActionResult> GetCategoriesForUser(string userId)
	{
		var query       = new GetCategoriesByUserIdQuery(userId);
		var queryResult = await _mediator.Send(query);

		if(queryResult.IsFailure)
		{
			return BadRequest(queryResult.Error);
		}

		return Ok(queryResult.Value);
	}


	[HttpDelete(nameof(DeleteCategory)+"/{id}")]
	public async Task<IActionResult> DeleteCategory(Guid id)
	{
		var commandResult = await _mediator.Send(new DeleteCategoryCommand(id));

		if(commandResult.IsFailure)
		{
			return BadRequest(commandResult.Error.Message);
		}

		return Ok(commandResult.Value);
	}

}
