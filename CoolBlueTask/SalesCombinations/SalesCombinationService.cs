using System;
using System.Collections.Generic;
using AutoMapper;
using CoolBlueTask.Products;
using CoolBlueTask.SalesCombinations.Models;

namespace CoolBlueTask.SalesCombinations
{
	public interface ISalesCombinationService
	{
		IList<SalesCombinationReadDto> GetCombinationsForProduct(string productId);
		SalesCombinationReadDto CreateSalesCombination(
			SalesCombinationWriteDto combination);
	}


	public class SalesCombinationService : ISalesCombinationService
	{
		private readonly ISalesCombinationRepository combinationRepository;
		private readonly IProductRepository productRepository;
		private readonly IMapper mapper;

		public SalesCombinationService(
			ISalesCombinationRepository combinationRepository,
			IProductRepository productRepository,
			IMapper mapper)
		{
			this.combinationRepository = combinationRepository;
			this.productRepository = productRepository;
			this.mapper = mapper;
		}

		public IList<SalesCombinationReadDto> GetCombinationsForProduct(
			string productId)
		{
			if (!productRepository.Exists(productId))
			{
				throw new EntityNotFoundException();
			}

			var combinations = combinationRepository
				.LoadByProduct(productId);

			return mapper.Map<IList<SalesCombination>, IList<SalesCombinationReadDto>>(
				combinations);
		}

		public SalesCombinationReadDto CreateSalesCombination(SalesCombinationWriteDto combination)
		{
			throw new NotImplementedException();
		}
	}
}