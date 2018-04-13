using AutoMapper;

namespace CoolBlueTask.SalesCombinations.Models
{
	public class SalesCombinationMappingProfile : Profile
	{
		public SalesCombinationMappingProfile()
		{
			CreateMap<SalesCombination, SalesCombinationReadDto>();
		}
	}
}