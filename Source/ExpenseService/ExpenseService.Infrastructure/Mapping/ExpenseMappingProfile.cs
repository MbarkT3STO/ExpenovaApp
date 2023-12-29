namespace ExpenseService.Infrastructure.Mapping;

public class ExpenseMappingProfile : Profile
{
	public ExpenseMappingProfile()
	{
		CreateMap<Expense, ExpenseEntity>()
			.ForMember(dest => dest.Category, opt => opt.Ignore())
			.ForMember(dest => dest.User, opt => opt.Ignore());

		CreateMap<ExpenseEntity, Expense>();
	}
}
