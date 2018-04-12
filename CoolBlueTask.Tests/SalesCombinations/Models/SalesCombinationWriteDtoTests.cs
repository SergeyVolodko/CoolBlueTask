using System.Collections.Generic;
using System.IO;
using ApprovalTests;
using ApprovalTests.Reporters;
using ApprovalTests.Writers;
using CoolBlueTask.SalesCombinations.Models;
using Newtonsoft.Json;
using Xunit;

namespace CoolBlueTask.Tests.SalesCombinations.Models
{
	public class SalesCombinationWriteDtoTests
	{
		[Fact]
		[UseReporter(typeof(DiffReporter))]
		public void product_read_dto_approval()
		{
			var dto = new SalesCombinationWriteDto
			{
				MainProductId = "laptop-id",
				RelatedProducts = new List<string>
				{
					"mouse-id","headset-id"
				}
			};

			var expected =
				Path.Combine(Consts.TestDataFolder, "sales_combination_write_dto.json");

			var actual = JsonConvert.SerializeObject(dto, Formatting.Indented);

			var writer = new ConfigurableTempTextFileWriter(expected, actual);

			Approvals.Verify(writer);
		}
	}
}
