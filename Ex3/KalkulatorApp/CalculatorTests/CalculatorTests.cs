using NUnit.Framework;
using KalkulatorApp;
using System;
using System.Collections.Generic;

namespace CalculatorTests
{
    [TestFixture]
    public class CalculatorTests
    {
        private Calculator calculator;

        [SetUp]
        public void Setup()
        {
            calculator = new Calculator();
        }

        [Test]
        public void Add_ReturnsCorrectResult()
        {
            double result = calculator.Add(5, 3);
            Assert.That(result, Is.EqualTo(8));
        }

        [Test]
        public void Subtract_ReturnsCorrectResult()
        {
            double result = calculator.Subtract(10, 4);
            Assert.That(result, Is.EqualTo(6));
        }

        [Test]
        public void Multiply_ReturnsCorrectResult()
        {
            double result = calculator.Multiply(7, 6);
            Assert.That(result, Is.EqualTo(42));
        }

        [Test]
        public void Divide_ReturnsCorrectResult()
        {
            double result = calculator.Divide(20, 5);
            Assert.That(result, Is.EqualTo(4));
        }

        [Test]
        public void Divide_ByZero_ThrowsException()
        {
            Assert.Throws<DivideByZeroException>(() => calculator.Divide(10, 0));
        }

        [Test]
        public void SumSequence_ReturnsCorrectResult()
        {
            var numbers = new List<double> { 1.5, 2.5, 6.0 };
            double result = calculator.SumSequence(numbers);
            Assert.That(result, Is.EqualTo(10.0));
        }

        [Test]
        public void Average_ReturnsCorrectResult()
        {
            var numbers = new List<double> { 2, 4, 6, 8 };
            double result = calculator.Average(numbers);
            Assert.That(result, Is.EqualTo(5));
        }
        
        [Test]
        public void Average_EmptyCollection_ThrowsException()
        {
            var numbers = new List<double>();
            Assert.Throws<InvalidOperationException>(() => calculator.Average(numbers));
        }

        [Test]
        public void Max_ReturnsCorrectResult()
        {
             var numbers = new List<double> { 1, 9, 3, 7 };
             double result = calculator.Max(numbers);
             Assert.That(result, Is.EqualTo(9));
        }
        
        [Test]
        public void Max_EmptyCollection_ThrowsException()
        {
            var numbers = new List<double>();
            Assert.Throws<InvalidOperationException>(() => calculator.Max(numbers));
        }

        [Test]
        public void Min_ReturnsCorrectResult()
        {
            var numbers = new List<double> { 4, 2, 8, 1 };
            double result = calculator.Min(numbers);
            Assert.That(result, Is.EqualTo(1));
        }
        
        [Test]
        public void Min_EmptyCollection_ThrowsException()
        {
            var numbers = new List<double>();
            Assert.Throws<InvalidOperationException>(() => calculator.Min(numbers));
        }
    }
}
