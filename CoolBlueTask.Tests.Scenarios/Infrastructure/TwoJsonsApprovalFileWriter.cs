using System.IO;
using ApprovalTests;

namespace CoolBlueTask.Tests.Scenarios.Infrastructure
{
	public class TwoJsonsApprovalFileWriter : ApprovalTextWriter
	{
		private readonly string approvedFilePath;
		private string receivedFilePath;

		public TwoJsonsApprovalFileWriter(string data) : base(data)
		{
		}

		public TwoJsonsApprovalFileWriter(string data, string extensionWithoutDot)
			: base(data, extensionWithoutDot)
		{
		}

		public TwoJsonsApprovalFileWriter(
			string jsonStringApproved, string data, string nameOfExpected = "")
			: base(data, "json")
		{
			var tmpFolder = Path.GetTempPath();
			var tmpFile = "tmpExpected.json";
			if (!string.IsNullOrWhiteSpace(nameOfExpected))
			{
				tmpFile = nameOfExpected;
			}

			approvedFilePath = Path.Combine(tmpFolder, tmpFile);
			File.WriteAllText(approvedFilePath, jsonStringApproved);
		}

		public override string GetApprovalFilename(string basename)
		{
			return approvedFilePath;
		}

		public override string GetReceivedFilename(string basename)
		{
			if (string.IsNullOrEmpty(receivedFilePath))
			{
				receivedFilePath = Path.ChangeExtension(Path.Combine(Path.GetTempPath(), Path.GetTempFileName()), ExtensionWithDot);
			}
			return receivedFilePath;
		}
	}
}
