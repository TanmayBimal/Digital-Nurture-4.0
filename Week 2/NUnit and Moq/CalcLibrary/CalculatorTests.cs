using NUnit.Framework;
using CalcLibrary;

namespace CalcLibrary.Tests
{
    [TestFixture]
    public class CalculatorTests
    {
        private SimpleCalculator _calculator;

        [SetUp]
        public void Setup()
        {
            _calculator = new SimpleCalculator();
        }

        [TearDown]
        public void TearDown()
        {
            _calculator.AllClear();
        }

        [Test]
        [TestCase(5, 3, 8)]
        [TestCase(-1, -4, -5)]
        [TestCase(0, 0, 0)]
        public void Addition_ReturnsCorrectResult(double a, double b, double expected)
        {
            double actual = _calculator.Addition(a, b);
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        [Ignore("This is an ignored test demo")]
        public void IgnoredTest_Demo()
        {
            Assert.Fail("This should be ignored.");
        }
    }
}
