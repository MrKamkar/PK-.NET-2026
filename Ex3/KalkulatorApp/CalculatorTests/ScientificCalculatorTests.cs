using NUnit.Framework;
using KalkulatorApp;
using System;

namespace CalculatorTests
{
    [TestFixture]
    public class ScientificCalculatorTests
    {
        private ScientificCalculator scientificCalculator;

        [SetUp]
        public void Setup()
        {
            scientificCalculator = new ScientificCalculator();
        }

        [Test]
        public void Power_ReturnsCorrectResult()
        {
            double result = scientificCalculator.Power(2, 3);
            Assert.That(result, Is.EqualTo(8));
        }

        [Test]
        public void SquareRoot_ReturnsCorrectResult()
        {
            double result = scientificCalculator.SquareRoot(9);
            Assert.That(result, Is.EqualTo(3));
        }

        [Test]
        public void SquareRoot_NegativeNumber_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() => scientificCalculator.SquareRoot(-4));
        }

        [Test]
        public void Log_ReturnsCorrectResult()
        {
            double result = scientificCalculator.Log(Math.E);
            Assert.That(result, Is.EqualTo(1).Within(0.0001));
        }
        
        [Test]
        public void Log_NonPositiveNumber_ThrowsException()
        {
             Assert.Throws<ArgumentException>(() => scientificCalculator.Log(0));
             Assert.Throws<ArgumentException>(() => scientificCalculator.Log(-5));
        }
    }
}
