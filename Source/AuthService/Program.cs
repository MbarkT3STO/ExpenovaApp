using System.Reflection;
using FluentValidation;
using MassTransit;
using RabbitMqSettings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
{
	options.UseNpgsql(builder.Configuration.GetConnectionString("AuthDBConnection"));
});

// Add Identity
builder.Services.AddIdentity<AppUser, AppRole>(options =>
{
	options.Password.RequireDigit              = false;
	options.Password.RequireLowercase          = false;
	options.Password.RequireNonAlphanumeric    = false;
	options.Password.RequireUppercase          = false;
	options.Password.RequiredLength            = 6;
	options.Password.RequiredUniqueChars       = 0;
	options.SignIn.RequireConfirmedAccount     = false;
	options.SignIn.RequireConfirmedEmail       = false;
	options.SignIn.RequireConfirmedPhoneNumber = false;
	options.User.RequireUniqueEmail            = true;
})
	.AddRoles<AppRole>()
	.AddEntityFrameworkStores<AppDbContext>();

// Add Authentication

// Add MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

// Add AutoMapper
builder.Services.AddAutoMapper(typeof(Program).Assembly);

// Add FluentValidation
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

// Add MassTransit
// builder.Services.AddMassTransit(busConfig =>
// {
// 	busConfig.UsingRabbitMq((context, config) =>
// 	{
// 		config.Host("rabbitmq://localhost", hostConfig => 
// 		{
// 			hostConfig.Username("guest");
// 			hostConfig.Password("guest");
// 		});
// 	});
// });

// RabbitMqOptions 

// builder.Services.ConfigureRabbitMQ(builder.Configuration);

// var rabbitMqOptions = builder.Configuration.GetSection("RabbitMQ:Settings").Get<RabbitMqOptions>();

// builder.Services.AddMassTransit(busConfigurator => 
// {
// 	busConfigurator.UsingRabbitMq((context, cfg) => 
// 	{
// 		cfg.Host($"{rabbitMqOptions.HostName}:{rabbitMqOptions.Port}", h =>
// 		{
// 			h.Username(rabbitMqOptions.UserName);
// 			h.Password(rabbitMqOptions.Password);
// 		});
// 	});
// });

builder.Services.ConfigureRabbitMQ(builder.Configuration);

builder.Services.AddMassTransit( x => x.AddBus( provider => Bus.Factory.CreateUsingRabbitMq( cfg =>
{
	cfg.Host("rabbitmq://localhost", hostConfig => 
	{
		hostConfig.Username("guest");
		hostConfig.Password("guest");
	});
})));



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
