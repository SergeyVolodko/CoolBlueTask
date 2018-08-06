using System.Collections.Generic;
using AutoMapper;
using CoolBlueTask.Products;
using CoolBlueTask.SalesCombinations.Models;
using FluentValidation;

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
		private readonly AbstractValidator<SalesCombinationWriteDto> inputValidator;
		private readonly ISalesCombinationRepository combinationRepository;
		private readonly IProductRepository productRepository;
		private readonly ISalesCombinationBuilder combinationBuilder;
		private readonly IMapper mapper;

		public SalesCombinationService(
			AbstractValidator<SalesCombinationWriteDto> inputValidator,
			ISalesCombinationRepository combinationRepository,
			IProductRepository productRepository,
			ISalesCombinationBuilder combinationBuilder,
			IMapper mapper)
		{
			this.inputValidator = inputValidator;
			this.combinationRepository = combinationRepository;
			this.productRepository = productRepository;
			this.combinationBuilder = combinationBuilder;
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

		public SalesCombinationReadDto CreateSalesCombination(
			SalesCombinationWriteDto combination)
		{
			var inputValidationResult = inputValidator.Validate(combination);
			if (!inputValidationResult.IsValid)
			{
				throw new ValidationException(inputValidationResult.Errors);
			}

			var newCombination = combinationBuilder
				.WithMainProduct(combination.MainProductId)
				.WithRelatedProducts(combination.RelatedProducts)
				.Build();

			var createdCombination = combinationRepository
				.Save(newCombination);

			return mapper
				.Map<SalesCombination, SalesCombinationReadDto>(createdCombination);
		}
	}
}