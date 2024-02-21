using ExpenseService.Application.ApplicationServices;
using ExpenseService.Application.Behaviors;
using ExpenseService.Application.Interfaces;
using ExpenseService.Domain.Shared.Interfaces;
using ExpenseService.Domain.Specifications.CategorySpecifications;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ExpenseService.Application.DI;

public static class ApplicationRegistrar
{
	/// <summary>
	/// Registers all the necessary application services in the specified <see cref="IServiceCollection"/> instance.
	/// </summary>
	/// <param name="services">The <see cref="IServiceCollection"/> instance to register the services in.</param>
	/// <param name="configuration">The <see cref="IConfiguration"/> instance to use for configuring the services.</param>
	public static void RegisterApplicationServices(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
		services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));


		// Register the database context as a service and configure it to use PostgreSQL.
		services.AddDbContext<AppDbContext>(options =>
		{
			options.UseNpgsql(configuration.GetConnectionString("AppDBConnection"),
			b => b.MigrationsAssembly("ExpenseService.API"));
			options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
		});


		// MediatR Pipeline Behaviors
		services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
		services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ExceptionHandlingBehavior<,>));
		services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));


		// Register FluentValidation validators
		services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());


		// Register all Repositories automatically from all assemblies
		services.AddTransient(typeof(ICategoryRepository), typeof(CategoryRepository));
		services.AddTransient(typeof(IExpenseRepository), typeof(ExpenseRepository));
		services.AddTransient(typeof(ISubscriptionExpenseRepository), typeof(SubscriptionExpenseRepository));
		services.AddTransient(typeof(IUserRepository), typeof(UserRepository));


		// Register Application Services (Services that are used by the Application Layer)
		services.AddTransient<ApplicationUserService>();
		services.AddTransient<ApplicationCategoryService>();
		services.AddTransient<ApplicationExpenseService>();


		// Register Deduplication Services
		services.AddTransient<IDomainEventDeduplicationService, DomainEventDatabaseDeduplicationService>();
	}


	// /// <summary>
	// /// Registers the specifications and composite specifications for the application.
	// /// </summary>
	// /// <param name="services">The service collection to register the specifications with.</param>
	// public static void RegisterSpecifications(this IServiceCollection services)
	// {
	// 	// Specifications
	// 	services.AddTransient<ISpecification<Domain.Entities.Category>, IsValidCategoryNameSpecification>();
	// 	services.AddTransient<ISpecification<Domain.Entities.Category>, IsValidCategoryDescriptionSpecification>();
	// 	services.AddTransient<ISpecification<Domain.Entities.Category>, IsValidCategoryCreationAuditSpecification>();


	// 	// Composite Specifications
	// 	services.AddTransient<ICompositeSpecification<Domain.Entities.Category>, IsValidCategoryForCreateSpecification>();
	// }

}
