using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseService.Infrastructure.Mapping;

public class IncomeMappingProfile : Profile
{
	public IncomeMappingProfile()
	{
		CreateMap<Income, IncomeEntity>()
			.ForMember(dest => dest.Category, opt => opt.Ignore())
			.ForMember(dest => dest.User, opt => opt.Ignore());

		CreateMap<IncomeEntity, Income>();
	}
}
