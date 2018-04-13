using System.IO;
using ApprovalTests;
using ApprovalTests.Reporters;
using ApprovalTests.Writers;
using CoolBlueTask.Products.Models;
using CoolBlueTask.Tests.Infrastructure;
using Newtonsoft.Json;
using Xunit;

namespace CoolBlueTask.Tests.Products.Models
{
	public class ProductWriteDtoTests
	{
		[Fact]
		//[MethodImpl(MethodImplOptions.NoInlining)]
		[UseReporter(typeof(DiffReporter))]
		public void product_read_dto_approval()
		{
			var dto = new ProductWriteDto
			{
				Name = "The new product",
				Description = "some new description",
				Price = 42
			};

			var expected =
				Path.Combine(Consts.TestDataFolder, "product_write_dto.json");

			var actual = JsonConvert.SerializeObject(dto, Formatting.Indented);

			var writer = new ConfigurableTempTextFileWriter(expected, actual);

			Approvals.Verify(writer);
		}
	}
}
