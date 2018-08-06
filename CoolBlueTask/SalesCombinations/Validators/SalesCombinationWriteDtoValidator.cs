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
			RuleFor(s => s.MainProductId)
				.Must(id => !string.IsNullOrWhiteSpace(id))
				.OverridePropertyName("MainProduct")
				.WithErrorCode("notempty_error");

			RuleFor(s => s.RelatedProducts)
				.NotNull()
				.NotEmpty();
		}

		public override ValidationResult Validate(
			SalesCombinationWriteDto instance)
		{
			if (instance == null)
			{
				return new ValidationResult(new List<ValidationFailure>
				{
					new ValidationFailure("Sales Combination", "Object can't be null.")
				});
			}

			return base.Validate(instance);
		}
	}
}