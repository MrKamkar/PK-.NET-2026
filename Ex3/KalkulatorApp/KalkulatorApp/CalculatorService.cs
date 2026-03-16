using System;
using System.Linq;

namespace KalkulatorApp
{
    // Serwis obsługujący interakcję z użytkownikiem
    public class CalculatorService
    {
        private ScientificCalculator scientificCalculator;

        public CalculatorService()
        {
            scientificCalculator = new ScientificCalculator();
        }

        // Uruchomienie głównej pętli kalkulatora
        public void Run()
        {
            Console.WriteLine("Kalkulator naukowy w C#");
            
            while (true)
            {
                Console.WriteLine("Wybierz operację: +, -, *, /, ^, sqrt, log, sum, avg, max, min, wyjście");
                Console.Write("> ");
                string operation = Console.ReadLine()?.Trim();

                if (string.IsNullOrEmpty(operation))
                {
                    continue;
                }

                // Opcja zakończenia działania programu
                if (operation.Equals("wyjście", StringComparison.OrdinalIgnoreCase) || operation.Equals("wyjscie", StringComparison.OrdinalIgnoreCase) || operation.Equals("exit", StringComparison.OrdinalIgnoreCase))
                {
                    break;
                }

                try
                {
                    ProcessOperation(operation);
                }
                catch (Exception ex)
                {
                    // Wypisywanie komunikatów o błędach
                    if (!ex.Message.StartsWith("Błąd:"))
                        Console.WriteLine($"Błąd: {ex.Message}");
                    else
                        Console.WriteLine(ex.Message);
                }
                Console.WriteLine();
            }
        }

        // Rozpoznanie i wywołanie odpowiedniej operacji
        private void ProcessOperation(string operation)
        {
            switch (operation)
            {
                case "+":
                case "-":
                case "*":
                case "/":
                case "^":
                    HandleTwoParameterOperation(operation);
                    break;
                case "sqrt":
                case "log":
                    HandleSingleParameterOperation(operation);
                    break;
                case "sum":
                case "avg":
                case "max":
                case "min":
                    HandleSequenceOperation(operation);
                    break;
                default:
                    Console.WriteLine("Błąd: Nieznana operacja.");
                    break;
            }
        }

        // Obsługa operacji pobierających dwa parametry
        private void HandleTwoParameterOperation(string operation)
        {
            Console.WriteLine("Podaj pierwszą liczbę:");
            Console.Write("> ");
            if (!double.TryParse(Console.ReadLine(), out double a))
            {
                Console.WriteLine("Błąd: Nieprawidłowy format liczby.");
                return;
            }

            Console.WriteLine("Podaj drugą liczbę:");
            Console.Write("> ");
            if (!double.TryParse(Console.ReadLine(), out double b))
            {
                Console.WriteLine("Błąd: Nieprawidłowy format liczby.");
                return;
            }

            double result = 0;
            switch (operation)
            {
                case "+":
                    result = scientificCalculator.Add(a, b);
                    break;
                case "-":
                    result = scientificCalculator.Subtract(a, b);
                    break;
                case "*":
                    result = scientificCalculator.Multiply(a, b);
                    break;
                case "/":
                    result = scientificCalculator.Divide(a, b);
                    break;
                case "^":
                    result = scientificCalculator.Power(a, b);
                    break;
            }
            Console.WriteLine($"Wynik: {result}");
        }

        // Obsługa operacji pobierających jeden parametr
        private void HandleSingleParameterOperation(string operation)
        {
            Console.WriteLine("Podaj liczbę:");
            Console.Write("> ");
            if (!double.TryParse(Console.ReadLine(), out double a))
            {
                Console.WriteLine("Błąd: Nieprawidłowy format liczby.");
                return;
            }

            double result = 0;
            switch (operation)
            {
                case "sqrt":
                    result = scientificCalculator.SquareRoot(a);
                    break;
                case "log":
                    result = scientificCalculator.Log(a);
                    break;
            }
            Console.WriteLine($"Wynik: {result}");
        }

        // Obsługa operacji działających na kolekcji liczb
        private void HandleSequenceOperation(string operation)
        {
            Console.WriteLine("Podaj liczby, oddzielone spacją:");
            Console.Write("> ");
            string input = Console.ReadLine();
            
            if (string.IsNullOrWhiteSpace(input))
            {
                 Console.WriteLine("Błąd: Nie podano żadnych liczb.");
                 return;
            }

            // Przetworzenie wejścia na listę liczb
            var numbers = input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                               .Select(s => 
                               {
                                   // Obsługa błędu formatowania liczby
                                   if (double.TryParse(s, out double n)) return n;
                                   throw new FormatException($"Nieprawidłowy format liczby: {s}");
                               })
                               .ToList();

            if (!numbers.Any())
            {
                Console.WriteLine("Błąd: Nie podano żadnych liczb.");
                return;
            }

            double result = 0;
            switch (operation)
            {
                case "sum":
                    result = scientificCalculator.SumSequence(numbers);
                    Console.WriteLine($"Suma: {result}");
                    break;
                case "avg":
                    result = scientificCalculator.Average(numbers);
                    Console.WriteLine($"Średnia: {result}");
                    break;
                case "max":
                    result = scientificCalculator.Max(numbers);
                    Console.WriteLine($"Maksimum: {result}");
                    break;
                case "min":
                    result = scientificCalculator.Min(numbers);
                    Console.WriteLine($"Minimum: {result}");
                    break;
            }
        }
    }
}
