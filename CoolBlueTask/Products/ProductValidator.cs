using System.Collections.Generic;
using CoolBlueTask.Products.Models;
using FluentValidation;
using FluentValidation.Results;

namespace CoolBlueTask.Products
{
	public class ProductValidator : AbstractValidator<Product>
	{
		public ProductValidator()
		{
			RuleFor(p => p.Name).NotEmpty();

			RuleFor(p => p.Price).GreaterThanOrEqualTo(0m);
		}

		public override ValidationResult Validate(
			ValidationContext<Product> context)
		{
			if (context.InstanceToValidate == null)
			{
				return new ValidationResult(new List<ValidationFailure>
				{
					new ValidationFailure("Product", "Object can't be null.")
				});
			}

			return base.Validate(context);
		}
	}
}