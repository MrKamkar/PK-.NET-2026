using System;
using System.Collections.Generic;

namespace KalkulatorApp
{
    // Kalkulator naukowy wykorzystujący kompozycję
    public class ScientificCalculator
    {
        // Obiekt podstawowego kalkulatora z którego delegujemy funkcjonalność
        private Calculator calculator;

        public ScientificCalculator()
        {
            calculator = new Calculator();
        }

        // Delegacja podstawowych metod do obiektu Calculator (Kompozycja)
        public double Add(double a, double b) => calculator.Add(a, b);
        public double Subtract(double a, double b) => calculator.Subtract(a, b);
        public double Multiply(double a, double b) => calculator.Multiply(a, b);
        public double Divide(double a, double b) => calculator.Divide(a, b);
        public double SumSequence(IEnumerable<double> numbers) => calculator.SumSequence(numbers);
        public double Average(IEnumerable<double> numbers) => calculator.Average(numbers);
        public double Max(IEnumerable<double> numbers) => calculator.Max(numbers);
        public double Min(IEnumerable<double> numbers) => calculator.Min(numbers);

        // Potęgowanie liczb
        public double Power(double a, double b)
        {
            return Math.Pow(a, b);
        }

        // Obliczanie pierwiastka kwadratowego
        public double SquareRoot(double a)
        {
            if (a < 0)
            {
                throw new ArgumentException("Błąd: Nie można obliczyć pierwiastka z liczby ujemnej.");
            }
            return Math.Sqrt(a);
        }

        // Obliczanie logarytmu naturalnego
        public double Log(double a)
        {
            if (a <= 0)
            {
                throw new ArgumentException("Błąd: Logarytm jest zdefiniowany tylko dla liczb dodatnich.");
            }
            return Math.Log(a);
        }
    }
}
