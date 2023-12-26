using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AuthService.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AuthService.Commands;

public class LoginCommandResultDto
{
	public string Token { get; set; }
	public string RefreshToken { get; set; }
	public DateTime TokenExpiration { get; set; }
	public DateTime RefreshTokenExpiration { get; set; }

	public LoginCommandResultDto(string token, DateTime tokenExpiration, string refreshToken, DateTime refreshTokenExpiration)
	{
		Token                  = token;
		TokenExpiration        = tokenExpiration;
		RefreshToken           = refreshToken;
		RefreshTokenExpiration = refreshTokenExpiration;
	}
}

public class LoginCommandResult: CommandResult<LoginCommandResultDto>
{
	public LoginCommandResult(LoginCommandResultDto value): base(value)
	{
	}

	public LoginCommandResult(Error error): base(error)
	{
	}
}

public class LoginCommand: IRequest<LoginCommandResult>
{
	public string UserName { get; set; }
	public string Password { get; set; }
}


public class LoginCommandHandler: CommandHandler, IRequestHandler<LoginCommand, LoginCommandResult>
{
	private readonly UserManager<AppUser> _userManager;
	private readonly JwtSettings _jwtSettings;
	private readonly IMapper _mapper;

	public LoginCommandHandler(AppDbContext context, IMapper mapper, UserManager<AppUser> userManager, IMediator mediator, IOptions<JwtSettings> jwtSettingsOptions): base(context, mapper, mediator, userManager)
	{
		_userManager = userManager;
		_jwtSettings = jwtSettingsOptions.Value;
		_mapper      = mapper;
	}

	public async Task<LoginCommandResult> Handle(LoginCommand request, CancellationToken cancellationToken)
	{
		try
		{
			var user = await _userManager.FindByNameAsync(request.UserName);

			if (user == null)
				return new LoginCommandResult(new Error("Invalid username or password"));

			var passwordValid = await _userManager.CheckPasswordAsync(user, request.Password);

			if (!passwordValid)
				return new LoginCommandResult(new Error("Invalid username or password"));


			var (AccessToken, AccessExpirationDate) = GenerateJwtToken(user);
			var refreshToken                        = await GenerateAndSaveRefreshTokenAsync(user);

			var loginCommandResultDto = new LoginCommandResultDto(AccessToken, AccessExpirationDate, refreshToken.Token, refreshToken.ExpiresAt);
			var result                = new LoginCommandResult(loginCommandResultDto);

			return result;
		}
		catch (Exception ex)
		{
			return new LoginCommandResult(new Error(ex.Message));
		}
	}

	private (string Token, DateTime ValidTo) GenerateJwtToken(AppUser user)
	{
		var claims = new Claim[]
		{
			new (JwtRegisteredClaimNames.Sub, user.Email.ToString()),
			new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
		};

		var securityKey        = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
		var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

		var token = new JwtSecurityToken(
			issuer            : _jwtSettings.Issuer,
			audience          : _jwtSettings.Audience,
			claims            : claims,
			expires           : DateTime.UtcNow.AddMinutes(1),
			signingCredentials: signingCredentials
		);

		var encodedToken = new JwtSecurityTokenHandler().WriteToken(token);

		return (encodedToken, token.ValidTo);
	}

	/// <summary>
	/// Generates and saves a refresh token for the specified user.
	/// </summary>
	/// <param name="user">The user for whom to generate the refresh token.</param>
	/// <returns>The generated refresh token.</returns>
	private async Task<RefreshToken> GenerateAndSaveRefreshTokenAsync(AppUser user)
	{
		var refreshToken = new RefreshToken
		{
			Token     = Guid.NewGuid().ToString(),
			UserId    = user.Id,
			CreatedAt = DateTime.UtcNow,
			ExpiresAt = DateTime.UtcNow.AddMinutes(60)
		};

		await _context.RefreshTokens.AddAsync(refreshToken);
		await _context.SaveChangesAsync();

		return refreshToken;
	}
}