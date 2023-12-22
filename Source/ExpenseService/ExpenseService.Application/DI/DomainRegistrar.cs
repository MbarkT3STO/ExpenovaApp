using ExpenseService.Domain.Services.Category;
using ExpenseService.Domain.Shared.Interfaces;
using ExpenseService.Domain.Specifications;
using Microsoft.Extensions.DependencyInjection;

namespace ExpenseService.Application.DI;

public static class DomainRegistrar
{

	/// <summary>
	/// Registers the domain services in the dependency injection container.
	/// </summary>
	/// <param name="services">The service collection.</param>
	public static void RegisterDomainServices(this IServiceCollection services)
	{
		services.AddTransient<ICategoryUniquenessChecker, CategoryUniquenessChecker>();
	}

	/// <summary>
	/// Registers the specifications and composite specifications for the application.
	/// </summary>
	/// <param name="services">The service collection to register the specifications with.</param>
	public static void RegisterSpecifications(this IServiceCollection services)
	{
		#region Category Specifications
		services.AddTransient<IsValidCategoryNameSpecification>();
		services.AddTransient<IsValidCategoryDescriptionSpecification>();
		services.AddTransient<IsValidCategoryCreationAuditSpecification>();
		services.AddTransient<IsUniqueCategoryNameSpecification>();

		services.AddTransient<IsValidCategoryForCreateSpecification>();
		#endregion
	}
}