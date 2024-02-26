using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseService.Domain.Repositories;

/// <summary>
/// Represents a repository for managing income entities.
/// </summary>
public interface IIncomeRepository : IRepository<Income, Guid>
{

}
