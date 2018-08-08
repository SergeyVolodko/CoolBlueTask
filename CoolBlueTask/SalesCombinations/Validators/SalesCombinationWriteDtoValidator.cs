using System.Collections.Generic;
using CoolBlueTask.SalesCombinations.Models;
using FluentValidation;
using FluentValidation.Results;

namespace CoolBlueTask.SalesCombinations.Validators
{
	public class SalesCombinationWriteDtoValidator 
		: AbstractValidator<SalesCombinationWriteDto>
	{
		public SalesCombinationWriteDtoValidator()
		{
			RuleFor(c => c.MainProductId)
				.Must(id => !string.IsNullOrWhiteSpace(id))
				.OverridePropertyName("MainProduct")
				.WithErrorCode("notempty_error")
				.WithMessage("Main product is missing.");

			RuleFor(c => c.RelatedProducts)
				.Must(relatedProducts =>
							relatedProducts != null && 
							relatedProducts.Count > 0)
				.WithErrorCode("notempty_error")
				.WithMessage("Related products are missing.");

			var maxRelatedProducts = 5;
			RuleFor(c => c.RelatedProducts)
				.Must(relatedProducts => 
							(relatedProducts?.Count ?? 0) <= maxRelatedProducts)
				.WithErrorCode("too_many_related_products")
				.WithMessage("The number of related products should be less than 5.");
		}

		public override ValidationResult Validate(
			SalesCombinationWriteDto instance)
		{
			if (instance == null)
			{
				return new ValidationResult(new List<ValidationFailure>
				{
					new ValidationFailure("Sales Combination", "Object can't be null.") { ErrorCode = "not_null"}
				});
			}
			return base.Validate(instance);
		}
	}
}