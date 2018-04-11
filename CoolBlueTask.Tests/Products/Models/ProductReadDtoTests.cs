using System.IO;
using ApprovalTests;
using ApprovalTests.Reporters;
using ApprovalTests.Writers;
using CoolBlueTask.Products.Models;
using Newtonsoft.Json;
using Xunit;

namespace CoolBlueTask.Tests.Products.Models
{
	public class ProductReadDtoTests
	{
		[Fact]
		//[MethodImpl(MethodImplOptions.NoInlining)]
		[UseReporter(typeof(DiffReporter))]
		public void product_read_dto_approval()
		{
			var dto = new ProductReadDto
			{
				Id = "some-product-id",
				Name = "The Product",
				Description = "some description",
				Price = 42
			};

			var expected =
				Path.Combine(Consts.TestDataFolder, "product_dto.json");

			var actual = JsonConvert.SerializeObject(dto, Formatting.Indented);

			var writer = new ConfigurableTempTextFileWriter(expected, actual);

			Approvals.Verify(writer);
		}
	}
}
