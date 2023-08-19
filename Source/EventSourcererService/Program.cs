using EventSourcererService.DI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<AppDbContext>(options =>
{
	options.UseNpgsql(builder.Configuration.GetConnectionString("EventsDB"));
});

// Register AutoMapper
builder.Services.AddAutoMapper(typeof(Program).Assembly);

// RabbitMQ registration
builder.Services.ConfigureRabbitMQ(builder.Configuration);

// Register the Message Consumers
builder.Services.RegisterMessageConsumers();



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
