using ExpenseService.API.BackgroundServices;

namespace ExpenseService.API.DI;

public static class HostedServicesRegistrar
{
	public static IServiceCollection RegisterHostedServices(this IServiceCollection services)
	{
		services.AddSingleton<OutboxProcessorService>();
		services.AddHostedService( provider => provider.GetService<OutboxProcessorService>()!);

		return services;
	}
}
