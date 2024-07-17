using Microsoft.VisualStudio.TestPlatform.Utilities;
using Xunit;
using Xunit.Abstractions;

namespace UnitTesting
{
    public class UnitTest
    {
        private readonly ITestOutputHelper _output;

        public UnitTest(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void Test1()
        {
            int a = 5;
            int b = 3;
            int result = a + b;
            _output.WriteLine($"Adding {a} and {b}");
            Assert.Equal(8, result);
        }
    }
}