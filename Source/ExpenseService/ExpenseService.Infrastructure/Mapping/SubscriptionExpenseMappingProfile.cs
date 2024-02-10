
namespace ExpenseService.Infrastructure.Mapping;

public class SubscriptionExpenseMappingProfile : Profile
{
	public SubscriptionExpenseMappingProfile()
	{
		CreateMap<SubscriptionExpense, SubscriptionExpenseEntity>()
		.ForMember(dest => dest.User, opt => opt.Ignore())
		.ForMember(dest => dest.Category, opt => opt.Ignore());


		CreateMap<SubscriptionExpenseEntity, SubscriptionExpense>();
	}
}
