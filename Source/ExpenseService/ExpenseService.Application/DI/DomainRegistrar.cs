using ExpenseService.Domain.Services;
using ExpenseService.Domain.Services.Category;
using ExpenseService.Domain.Shared.Interfaces;
using ExpenseService.Domain.Shared.Services;
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
		services.AddTransient<ICategoryService, CategoryService>();
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
		services.AddTransient<IsUniqueCategoryNameSpecification>();

		services.AddTransient<IsValidCategoryCreationAuditSpecification>();
		services.AddTransient<IsValidCategoryUpdateAuditSpecification>();
		services.AddTransient<IsValidCategoryDeleteAuditSpecification>();

		services.AddTransient<IsValidCategoryForCreateSpecification>();
		services.AddTransient<IsValidCategoryForUpdateSpecification>();
		services.AddTransient<IsValidCategoryForDeleteSpecification>();

		// services.AddTransient<ICompositeSpecification<Domain.Entities.Category>, IsValidCategoryForUpdateSpecification>();
		#endregion
	}
}