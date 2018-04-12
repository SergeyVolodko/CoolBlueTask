using System.Collections.Generic;
using System.IO;
using ApprovalTests;
using ApprovalTests.Reporters;
using ApprovalTests.Writers;
using CoolBlueTask.Products.Models;
using CoolBlueTask.SalesCombinations.Models;
using Newtonsoft.Json;
using Xunit;

namespace CoolBlueTask.Tests.SalesCombinations.Models
{
	public class SalesCombinationReadDtoTests
	{
		[Fact]
		[UseReporter(typeof(DiffReporter))]
		public void product_read_dto_approval()
		{
			var dto = new SalesCombinationReadDto
			{
				Id = "some-combination-id",
				MainProduct = new ProductReadDto
				{
					Id = "laptop-id",
					Name = "Laptop",
					Description = "Turbo-ultra-book",
					Price = 1000
				},
				RelatedProducts = new List<ProductReadDto>
				{
					new ProductReadDto
					{
						Id = "mouse-id",
						Name = "Mouse",
						Description = "Swift wireless mouse",
						Price = 15.5m
					},
					new ProductReadDto
					{
						Id = "headset-id",
						Name = "Headset",
						Description = "Turbo Hi-Fi headset",
						Price = 50m
					}
				}
			};

			var expected =
				Path.Combine(Consts.TestDataFolder, "sales_combination_read_dto.json");

			var actual = JsonConvert.SerializeObject(dto, Formatting.Indented);

			var writer = new ConfigurableTempTextFileWriter(expected, actual);

			Approvals.Verify(writer);
		}
	}
}
