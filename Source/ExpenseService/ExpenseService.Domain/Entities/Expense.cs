using ExpenseService.Domain.Shared.Common;

namespace ExpenseService.Domain.Entities;

public class Expense: AuditableAggregateRoot<Guid>
{
	public decimal Amount { get; private set; }
	public DateTime Date { get; private set; }
	public string Description { get; private set; }
	public Category Category { get; private set; }
	public User User { get; private set; }

	protected Expense() { }

	public Expense(decimal amount, DateTime date, string description, Category category, User user)
	{
		Amount      = amount;
		Date        = date;
		Description = description;
		Category    = category;
		User        = user;

		CreatedAt = DateTime.UtcNow;
		CreatedBy   = user.Id;
	}

	/// <summary>
	/// Validates the expense entity against the given specification and throws a SpecificationException if the specification is not satisfied.
	/// </summary>
	/// <param name="specification">The specification to validate against.</param>
	/// <exception cref="SpecificationException">Thrown if the specification is not satisfied.</exception>
	public void Validate(ISpecification<Expense> specification)
	{
		var satisfactionResult = specification.IsSatisfiedBy(this);

		if (!satisfactionResult.IsSatisfied)
		{
			throw new SpecificationException(satisfactionResult.Errors);
		}
	}


	/// <summary>
	/// Validates the expense entity against the given specification and throws a SpecificationException if the specification is not satisfied.
	/// </summary>
	/// <param name="specification">The specification to validate against.</param>
	/// <exception cref="SpecificationException">Thrown if the specification is not satisfied.</exception>
	public void Validate(ICompositeSpecification<Expense> specification)
	{
		var satisfactionResult = specification.IsSatisfiedBy(this);

		if (!satisfactionResult.IsSatisfied)
		{
			throw new SpecificationException(satisfactionResult.Errors);
		}
	}

	/// <summary>
	/// Sets the ID of the expense.
	/// </summary>
	/// <param name="id">The ID to set.</param>
	public void SetId(Guid id)
	{
		Id = id;
	}


	public void Update(decimal amount, DateTime date, string description, Category category, User user)
	{
		UpdateAmount(amount);
		UpdateDate(date);
		UpdateDescription(description);
		UpdateCategory(category);
		UpdateUser(user);
	}


	/// <summary>
	/// Updates the date of the expense.
	/// </summary>
	/// <param name="date">The new date for the expense.</param>
	public void UpdateDate(DateTime date)
	{
		Date = date;
	}

	/// <summary>
	/// Updates the description of the expense.
	/// </summary>
	/// <param name="description">The new description.</param>
	public void UpdateDescription(string description)
	{
		Description = description;
	}

	/// <summary>
	/// Updates the category of the expense.
	/// </summary>
	/// <param name="category">The new category to be assigned.</param>
	public void UpdateCategory(Category category)
	{
		if (HasCategoryChanged(category))
		{
			Category = category;
		}
	}

	/// <summary>
	/// Updates the user associated with the expense.
	/// </summary>
	/// <param name="user">The new user to be associated with the expense.</param>
	public void UpdateUser(User user)
	{
		if (HasUserChanged(user))
		{
			User = user;
		}
	}

	/// <summary>
	/// Updates the amount of the expense.
	/// </summary>
	/// <param name="amount">The new amount of the expense.</param>
	public void UpdateAmount(decimal amount)
	{
		Amount = amount;
	}





	/// <summary>
	/// Checks if the category of the expense has changed.
	/// </summary>
	/// <param name="category">The new category to compare with.</param>
	/// <returns>True if the category has changed, false otherwise.</returns>
	private bool HasCategoryChanged(Category category)
	{
		return Category.Id != category.Id;
	}

	/// <summary>
	/// Checks if the user has changed.
	/// </summary>
	/// <param name="user">The new user.</param>
	/// <returns>True if the user has changed, false otherwise.</returns>
	private bool HasUserChanged(User user)
	{
		return User.Id != user.Id;
	}
}
