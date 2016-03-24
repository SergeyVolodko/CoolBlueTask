using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xunit;

namespace CoolBlueTask.Tests
{
    public class AdHocTests
    {
        [Fact]
        public void kick_start()
        {
            1.Should().Be(1);
        }
    }
}
