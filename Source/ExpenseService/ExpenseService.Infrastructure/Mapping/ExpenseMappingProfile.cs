namespace ExpenseService.Infrastructure.Mapping;

public class ExpenseMappingProfile : Profile
{
	public ExpenseMappingProfile()
	{
		CreateMap<Expense, ExpenseEntity>();
		CreateMap<ExpenseEntity, Expense>();
	}
}
