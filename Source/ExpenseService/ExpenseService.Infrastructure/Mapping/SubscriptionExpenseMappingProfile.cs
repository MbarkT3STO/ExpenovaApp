
namespace ExpenseService.Infrastructure.Mapping;

public class SubscriptionExpenseMappingProfile : Profile
{
    public SubscriptionExpenseMappingProfile()
    {
        CreateMap<SubscriptionExpense, SubscriptionExpenseEntity>();
        CreateMap<SubscriptionExpenseEntity, SubscriptionExpense>();
    }
}
