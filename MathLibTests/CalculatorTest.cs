using MathLib;
using Xunit;

namespace MathLibTests
{
    public class CalculatorTest
    {
        [Fact]
        public void TestAdd()
        {
            var c = new Calculator();
            Assert.Equal(23, c.Add(15, 8));
        }

        [Fact]
        public void TestAdd2()
        {
            var c = new Calculator();
            Assert.Equal(1800, c.Add(1000, 800));
        }
    }
}
