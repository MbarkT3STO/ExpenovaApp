using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace RabbitMqSettings;

public static class RabbitMqDIExtensions
{
	/// <summary>
	/// Configures RabbitMQ settings for the application.
	/// </summary>
	/// <param name="services">The <see cref="IServiceCollection"/> to add the configuration to.</param>
	/// <param name="configuration">The <see cref="IConfiguration"/> instance to retrieve the configuration from.</param>
	public static void ConfigureRabbitMQ(this IServiceCollection services, IConfiguration configuration)
	{
		services.Configure<RabbitMqOptions>(configuration.GetSection("RabbitMQ:Settings"));

		services.Configure<AuthServiceRabbitMqEndpointsOptions>(configuration.GetSection("RabbitMQ:Endpoints:AuthService"));

		// RabbitMqEndpointsOptions
		services.AddSingleton(sp =>
		{
			var authServiceRabbitMqEndpointsOptions = sp.GetRequiredService<IOptions<AuthServiceRabbitMqEndpointsOptions>>();

			var rabbitMqEndpointsOptions = new RabbitMqEndPointsOptions
			{
				AuthServiceRabbitMqEndpointsOptions = authServiceRabbitMqEndpointsOptions.Value
			};

			return rabbitMqEndpointsOptions;
		});
	}
}
