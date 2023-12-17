using ExpenseService.Application.MessageConsumers;
using ExpenseService.Domain.Repositories;
using ExpenseService.Infrastructure.Data;
using ExpenseService.Infrastructure.Repositories;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RabbitMqSettings;
using ExpenseService.Application.DI;
using ExpenseService.API.DI;
using Serilog;
using ExpenseService.Application.Behaviors;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);
// var serviceProvider = builder.Services.BuildServiceProvider();

// Add services to the container.


// Register AutoMapper automatically from all assemblies
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Register MediatR automatically from all assemblies
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));



// Register the Application Services
builder.Services.RegisterApplicationServices(builder.Configuration);

// Register the Specifications
builder.Services.RegisterSpecifications();



// RabbitMQ registration
builder.Services.ConfigureRabbitMQ(builder.Configuration);

// Register the Message Consumers
builder.Services.RegisterMessageConsumers();

// Register the Hosted Services
builder.Services.RegisterHostedServices();

// Register the ILogger
builder.Services.AddLogging(loggingBuilder =>
{
	loggingBuilder.AddSerilog(dispose: true);

	loggingBuilder.AddFilter("Microsoft", LogLevel.Information);
});


// Configure Serilog
Log.Logger = new LoggerConfiguration()
	.MinimumLevel.Information()
	.MinimumLevel.Override("Microsoft", LogEventLevel.Error)
	.Enrich.FromLogContext()
	.WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
	.CreateLogger();

builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ExceptionHandlingBehavior<,>));


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

// app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
