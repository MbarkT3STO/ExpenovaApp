using ExpenseService.Domain.Repositories;
using ExpenseService.Infrastructure.Data;
using ExpenseService.Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

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
// builder.Services.AddTransient(typeof(IRepository<,>), typeof(Repository));

builder.Services.AddTransient(typeof(ICategoryRepository), typeof(CategoryRepository));
builder.Services.AddTransient(typeof(IExpenseRepository), typeof(ExpenseRepository));
builder.Services.AddTransient(typeof(ISubscriptionExpenseRepository), typeof(SubscriptionExpenseRepository));
builder.Services.AddTransient(typeof(IUserRepository), typeof(UserRepository));

	
 

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
