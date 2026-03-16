using System;
using System.Collections.Generic;
using System.Linq;

namespace KalkulatorApp
{
    // Klasa Kalkulatora - podstawowe operacje matematyczne i na kolekcjach
    public class Calculator
    {
        // Dodawanie
        public double Add(double a, double b)
        {
            return a + b;
        }

        // Odejmowanie
        public double Subtract(double a, double b)
        {
            return a - b;
        }

        // Mnożenie
        public double Multiply(double a, double b)
        {
            return a * b;
        }

        // Dzielenie
        public double Divide(double a, double b)
        {
            if (b == 0)
            {
                // Zabezpieczenie przed dzieleniem przez zero
                throw new DivideByZeroException("Błąd: Nie można dzielić przez zero.");
            }
            return a / b;
        }

        // Sumowanie elementów kolekcji
        public double SumSequence(IEnumerable<double> numbers)
        {
            return numbers.Sum();
        }

        // Obliczanie średniej wartości
        public double Average(IEnumerable<double> numbers)
        {
            if (numbers == null || !numbers.Any())
            {
                throw new InvalidOperationException("Kolekcja nie może być pusta.");
            }
            return numbers.Average();
        }

        // Znajdowanie wartości maksymalnej
        public double Max(IEnumerable<double> numbers)
        {
             if (numbers == null || !numbers.Any())
            {
                throw new InvalidOperationException("Kolekcja nie może być pusta.");
            }
            return numbers.Max();
        }

        // Znajdowanie wartości minimalnej
        public double Min(IEnumerable<double> numbers)
        {
             if (numbers == null || !numbers.Any())
            {
                throw new InvalidOperationException("Kolekcja nie może być pusta.");
            }
            return numbers.Min();
        }
    }
}
