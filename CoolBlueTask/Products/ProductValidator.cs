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
			Product instance)
		{
			if (instance == null)
			{
				return new ValidationResult(new List<ValidationFailure>
				{
					new ValidationFailure("Product", "Object can't be null.") { ErrorCode = "not_null" }
				});
			}

			return base.Validate(instance);
		}
	}
}