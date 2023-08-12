namespace ExpenseService.Infrastructure.Mapping;

public class UserMappingProfile : Profile
{
	public UserMappingProfile()
	{
		CreateMap<User, UserEntity>();
		CreateMap<UserEntity, User>();
	}
}
