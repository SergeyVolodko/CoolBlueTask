using System;
using System.IO;
using FluentAssertions;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace CoolBlueTask.Tests
{
	public class ApiLoggerFactoryTests
	{
		[Theory]
		[AutoData]
		public void created_file_logger_writes_messages_to_files(
			string testMessage)
		{
			// Arrange
			var sut = ApiLoggerFactory.CreateFileLogger();
			var recordTime = DateTime.UtcNow.ToShortTimeString();

			// Act
			sut.Trace(testMessage);
			var actual = File.ReadAllText("Trace.log");

			// Assert
			actual.Should().Contain(testMessage);
			actual.Should().Contain(recordTime);
		}
	}
}
