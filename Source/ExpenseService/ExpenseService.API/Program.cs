using ExpenseService.Application.MessageConsumers;
using ExpenseService.Domain.Repositories;
using ExpenseService.Infrastructure.Data;
using ExpenseService.Infrastructure.Repositories;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RabbitMqSettings;


var builder = WebApplication.CreateBuilder(args);
// var serviceProvider = builder.Services.BuildServiceProvider();

// Add services to the container.


// Register the database context as a service and configure it to use PostgreSQL.
builder.Services.AddDbContext<AppDbContext>(options =>
{
	options.UseNpgsql(builder.Configuration.GetConnectionString("AppDBConnection"),
	b => b.MigrationsAssembly("ExpenseService.API"));
});

// Register AutoMapper automatically from all assemblies
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


// Register MediatR automatically from all assemblies
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));


// Register all Repositories automatically from all assemblies
builder.Services.AddTransient(typeof(ICategoryRepository), typeof(CategoryRepository));
builder.Services.AddTransient(typeof(IExpenseRepository), typeof(ExpenseRepository));
builder.Services.AddTransient(typeof(ISubscriptionExpenseRepository), typeof(SubscriptionExpenseRepository));
builder.Services.AddTransient(typeof(IUserRepository), typeof(UserRepository));

// RabbitMQ registration

var rabbitMqOptions = builder.Configuration.GetSection("RabbitMQ:Settings").Get<RabbitMqOptions>();
var authServiceRabbitMqEndPointsOptions = builder.Configuration.GetSection("RabbitMQ:EndPoints:AuthService").Get<AuthServiceRabbitMqEndpointsOptions>();
var authServiceRabbitMqEndPoints = authServiceRabbitMqEndPointsOptions;


// builder.Services.AddMassTransit( busConfigurator => 
// {
// 	busConfigurator.UsingRabbitMq((context, cfg) => 
// 	{
// 		cfg.ReceiveEndpoint(authServiceRabbitMqEndPoints.UserCreatedEventQueue, ep => ep.Consumer<UserCreatedMessageConsumer>());
// 	});
// });

// builder.Services.AddMassTransit(busConfigurator => 
// {
// 	busConfigurator.UsingRabbitMq((context, cfg) => 
// 	{
// 		cfg.Host($"{rabbitMqOptions.HostName}:{rabbitMqOptions.Port}", h =>
// 		{
// 			h.Username(rabbitMqOptions.UserName);
// 			h.Password(rabbitMqOptions.Password);
// 		});

// 		cfg.ReceiveEndpoint(authServiceRabbitMqEndPoints.UserCreatedEventQueue, ep => ep.Consumer<UserCreatedMessageConsumer>());
// 	});
// });

builder.Services.AddMassTransit(x => x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
{
	cfg.Host("rabbitmq://localhost", hostConfig =>
	{
		hostConfig.Username("guest");
		hostConfig.Password("guest");
	});
	
	cfg.ReceiveEndpoint(authServiceRabbitMqEndPoints.UserCreatedEventQueue, ep => ep.Consumer<UserCreatedMessageConsumer>(provider));
	
})));


builder.Services.ConfigureRabbitMQ(builder.Configuration);

builder.Services.AddScoped<UserCreatedMessageConsumer>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
