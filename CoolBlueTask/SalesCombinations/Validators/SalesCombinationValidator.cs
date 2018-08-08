using System.Collections.Generic;
using System.Linq;
using CoolBlueTask.SalesCombinations.Models;
using FluentValidation;
using FluentValidation.Results;

namespace CoolBlueTask.SalesCombinations.Validators
{
	public class SalesCombinationValidator
		: AbstractValidator<SalesCombination>
	{
		public SalesCombinationValidator()
		{
			RuleFor(c => c.MainProduct)
				.NotNull()
				.WithErrorCode("not_existing_main_product")
				.WithMessage("Main product does not exist.");

			RuleFor(c => c.RelatedProducts)
				.Must(relatedProducts => relatedProducts.All(p => p != null))
				.WithErrorCode("not_existing_related_product")
				.WithMessage("A related product does not exist.");
		}

		public override ValidationResult Validate(
			SalesCombination instance)
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