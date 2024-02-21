using System.Linq.Expressions;
using ExpenseService.Application.Interfaces;
using ExpenseService.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExpenseService.Application.Common;

/// <summary>
/// Service for deduplication check and processing of domain events based on database storage.
/// </summary>
public class DomainEventDatabaseDeduplicationService : IDomainEventDeduplicationService
{
	private readonly AppDbContext _dbContext;

	public DomainEventDatabaseDeduplicationService(AppDbContext dbContext)
	{
		_dbContext = dbContext;
	}

	/// <summary>
	/// Checks if the specified event has already been processed.
	/// </summary>
	/// <param name="eventId">The ID of the event to check.</param>
	/// <returns>True if the event has been processed, false otherwise.</returns>
	public async Task<bool> HasProcessed(Guid eventId)
	{
		return await _dbContext.OutboxMessages.AnyAsync(e => e.EventId == eventId);
	}

	/// <summary>
	/// Processes the specified event asynchronously.
	/// </summary>
	/// <param name="processEventFunc">The function that represents the event processing logic.</param>
	/// <returns>A task representing the asynchronous operation.</returns>
	public async Task ProcessEventAsync(Expression<Func<Task>> processEventFunc)
	{
		await processEventFunc.Compile().Invoke();
	}
}