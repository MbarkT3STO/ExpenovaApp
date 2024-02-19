using EventSourcererService.MessageConsumers.AuthService;
using EventSourcererService.MessageConsumers.ExpenseService.Category;
using EventSourcererService.MessageConsumers.ExpenseService.Expense;
using EventSourcererService.MessageConsumers.ExpenseService.SubscriptionExpense;

namespace EventSourcererService.DI;

public static class MessageConsumersRegistrar
{
	/// <summary>
	/// Registers message consumers for RabbitMQ.
	/// </summary>
	/// <param name="services">The service collection to register the message consumers with.</param>
	public static void RegisterMessageConsumers(this IServiceCollection services)
	{
		services.AddScoped<AuthServiceUserCreatedMessageConsumer>();

		#region ExpenseService - Category
		services.AddScoped<ExpenseServiceCategoryCreatedMessageConsumer>();
		services.AddScoped<ExpenseServiceCategoryUpdatedMessageConsumer>();
		services.AddScoped<ExpenseServiceCategoryDeletedMessageConsumer>();
		#endregion

		#region ExpenseService - Expense
		services.AddScoped<ExpenseServiceExpenseCreatedMessageConsumer>();
		services.AddScoped<ExpenseServiceExpenseUpdatedMessageConsumer>();
		services.AddScoped<ExpenseServiceExpenseDeletedMessageConsumer>();
		#endregion

		#region ExpenseService - SubscriptionExpense
		services.AddScoped<SubscriptionExpenseCreatedMessageConsumer>();
		services.AddScoped<SubscriptionExpenseUpdatedMessageConsumer>();
		#endregion
	}
}
