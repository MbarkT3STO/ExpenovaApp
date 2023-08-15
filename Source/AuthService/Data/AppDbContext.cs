namespace AuthService.Data;

public class AppDbContext : IdentityDbContext<AppUser, AppRole, string, IdentityUserClaim<string>, AppUserRole, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>
{
	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
	{
		
	}
	
	protected override void OnModelCreating(ModelBuilder builder)
	{
		// Apply configurations from assembly
		builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
		
		// Seed App Roles
		builder.Entity<AppRole>().HasData(
			new AppRole { Id = "Admin", Name = "Admin", NormalizedName = "ADMIN" },
			new AppRole { Id = "User", Name = "User", NormalizedName = "USER" }
		);

		base.OnModelCreating(builder);
	}
	

	public DbSet<AppUser> AppUsers { get; set; }
	public DbSet<AppRole> AppRoles { get; set; }
	public DbSet<AppUserRole> AppUserRoles { get; set; }
	public DbSet<RefreshToken> RefreshTokens { get; set; }
}
