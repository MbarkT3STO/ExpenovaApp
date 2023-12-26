using System.Reflection;
using System.Text;
using AuthService.DI;
using AuthService.Options;
using FluentValidation;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
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

// Add JWT
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
builder.Services.AddOptions<JwtSettings>().Bind(builder.Configuration.GetSection("JwtSettings"));

var issuer       = builder.Configuration["JwtSettings:Issuer"];
var audience     = builder.Configuration["JwtSettings:Audience"];
var key          = builder.Configuration["JwtSettings:Key"];
var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

builder.Services.AddAuthentication(options =>
{
	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme    = JwtBearerDefaults.AuthenticationScheme;
})
	.AddJwtBearer(options =>
	{
		options.TokenValidationParameters = new TokenValidationParameters
		{
			ValidateIssuer           = true,
			ValidateAudience         = true,
			ValidateLifetime         = true,
			ValidateIssuerSigningKey = true,
			ValidIssuer              = issuer,
			ValidAudience            = audience,
			IssuerSigningKey         = symmetricKey,
			ClockSkew				= TimeSpan.Zero
		};
	});

// Add Authentication

// Add MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

// Add AutoMapper
builder.Services.AddAutoMapper(typeof(Program).Assembly);

// Add FluentValidation
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

// Add MassTransit with RabbitMQ
builder.Services.ConfigureRabbitMQ(builder.Configuration);



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
