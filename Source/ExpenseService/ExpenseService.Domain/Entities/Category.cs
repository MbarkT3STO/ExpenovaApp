using ExpenseService.Domain.Shared.Common;

namespace ExpenseService.Domain.Entities;

public class Category: AuditableEntity<Guid>
{
	public string Name { get; private set; }
	public string Description { get; private set; }
	public string UserId { get; private set; }

	protected Category() { }
	public Category( string name, string description, string userId)
	{
		Id          = Guid.NewGuid();
		Name        = name;
		Description = description;
		UserId      = userId;
	}


	/// <summary>
	/// Sets the ID of the category.
	/// </summary>
	/// <param name="id">The ID to set.</param>
	public void SetId(Guid id)
	{
		Id = id;
	}

	/// <summary>
	/// Updates the name of the category.
	/// </summary>
	/// <param name="newName">The new name of the category.</param>
	public void UpdateName(string newName)
	{
		Name = newName;
	}

	/// <summary>
	/// Updates the description of the category.
	/// </summary>
	/// <param name="newDescription">The new description to set for the category.</param>
	public void UpdateDescription(string newDescription)
	{
		Description = newDescription;
	}


	/// <summary>
	/// Marks the category as deleted.
	/// </summary>
	public void MarkAsDeleted()
	{
		IsDeleted = true;
	}


	/// <summary>
	/// Validates the category entity against the given specification.
	/// Throws a SpecificationException if the specification is not satisfied.
	/// </summary>
	/// <param name="specification">The specification to validate against.</param>
	public void Validate(ISpecification<Category> specification)
	{
		var satisfactionResult = specification.IsSatisfiedBy(this);

		if (!satisfactionResult.IsSatisfied)
		{
			throw new SpecificationException(satisfactionResult.Errors);
		}
	}


	/// <summary>
	/// Validates the category entity against the given specification and throws an exception if the specification is not satisfied.
	/// </summary>
	/// <param name="specification">The specification to validate against.</param>
	/// <exception cref="SpecificationException">Thrown if the specification is not satisfied.</exception>
	public void Validate(ICompositeSpecification<Category> specification)
	{
		var satisfactionResult = specification.IsSatisfiedBy(this);

		if (!satisfactionResult.IsSatisfied)
		{
			throw new SpecificationException(satisfactionResult.Errors);
		}
	}
}
