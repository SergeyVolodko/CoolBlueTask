using AutoMapper;

namespace CoolBlueTask.Products.Models
{
	public class ProductMappingProfile : Profile
	{
		public ProductMappingProfile()
		{
			CreateMap<Product, ProductReadDto>();

			CreateMap<ProductWriteDto, Product>();
		}
	}
}