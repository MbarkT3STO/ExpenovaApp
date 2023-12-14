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


// Configure Serilog
Log.Logger = new LoggerConfiguration()
.WriteTo.Console()
.WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
.CreateLogger();


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
