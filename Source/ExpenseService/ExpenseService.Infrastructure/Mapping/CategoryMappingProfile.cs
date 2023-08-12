namespace ExpenseService.Infrastructure.Mapping;

public class CategoryMappingProfile : Profile
{
	public CategoryMappingProfile()
	{
		CreateMap<Category, CategoryEntity>();
		CreateMap<CategoryEntity, Category>();
	}
}
