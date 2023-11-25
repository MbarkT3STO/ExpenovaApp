using EventSourcererService.MessageConsumers.AuthService;
using EventSourcererService.MessageConsumers.ExpenseService.Category;
using MassTransit;
using RabbitMqSettings;
using RabbitMqSettings.QueueRoutes.EventSourcerer;

namespace EventSourcererService.DI;

public static class RabbitMQRegistrar
{
	/// <summary>
	/// Configures RabbitMQ for the application using the provided configuration.
	/// </summary>
	/// <param name="services">The <see cref="IServiceCollection"/> to add the RabbitMQ configuration to.</param>
	/// <param name="configuration">The <see cref="IConfiguration"/> containing the RabbitMQ configuration settings.</param>
	public static void ConfigureRabbitMQ(this IServiceCollection services, IConfiguration configuration)
	{
		var rabbitMqOptions = configuration.GetSection("RabbitMQ:Settings").Get<RabbitMqOptions>();
		var authServiceRabbitMqEndPointsOptions = configuration.GetSection("RabbitMQ:EndPoints:AuthService").Get<AuthServiceRabbitMqEndpointsOptions>();
		var authServiceRabbitMqEndPoints = authServiceRabbitMqEndPointsOptions;


		services.AddMassTransit(busConfigurator => busConfigurator.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
		{
			cfg.Host(rabbitMqOptions.HostName, hostConfig =>
			{
				hostConfig.Username(rabbitMqOptions.UserName);
				hostConfig.Password(rabbitMqOptions.Password);
			});

			cfg.ReceiveEndpoint(AuthServiceEventSourcererQueues.UserEventSourcererQueue, ep => ep.Consumer<AuthServiceUserCreatedMessageConsumer>(provider));
			
			cfg.ReceiveEndpoint(ExpenseServiceEventSourcererQueues.CategoryCreatedEventSourcererQueue, ep => ep.Consumer<ExpenseServiceCategoryCreatedMessageConsumer>(provider));
			cfg.ReceiveEndpoint(ExpenseServiceEventSourcererQueues.CategoryUpdatedEventSourcererQueue, ep => ep.Consumer<ExpenseServiceCategoryUpdatedMessageConsumer>(provider));
			cfg.ReceiveEndpoint(ExpenseServiceEventSourcererQueues.CategoryDeletedEventSourcererQueue, ep => ep.Consumer<ExpenseServiceCategoryDeletedMessageConsumer>(provider));
			
		})));

		services.ConfigureRabbitMQBaseOptions(configuration);
	}
}