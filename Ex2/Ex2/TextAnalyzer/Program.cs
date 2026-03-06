using System;
using System.IO;
using TextAnalyzer;

class Program
{
    static void Main(string[] args)
    {
        string text;

        try
        {
            if (args.Length > 0) text = ReadFile(args[0]);
            else
            {
                Console.WriteLine("Wybierz źródło danych:");
                Console.WriteLine("1) Wprowadź tekst ręcznie");
                Console.WriteLine("2) Wczytaj tekst z pliku");
                Console.Write("Twój wybór: ");

                string choice = Console.ReadLine()!;

                if (choice == "2")
                {
                    Console.Write("Podaj ścieżkę do pliku: ");
                    string path = Console.ReadLine()!;
                    text = ReadFile(path);
                }
                else
                {
                    Console.WriteLine("Wpisz tekst:");
                    text = Console.ReadLine()!;
                }
            }

            if (string.IsNullOrWhiteSpace(text))
            {
                Console.WriteLine("Podany tekst jest pusty, nie ma czego analizować.");
                return;
            }

            var stats = TextAnalyzer.TextAnalyzer.AnalyzeText(text);
            PrintResults(stats);
        }
        catch (FileNotFoundException ex)
        {
            Console.WriteLine($"Nie znaleziono pliku: {ex.FileName}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Wystąpił błąd: {ex.Message}");
        }
    }

    private static string ReadFile(string path)
    {
        if (!File.Exists(path)) throw new FileNotFoundException("Plik nie istnieje.", path);

        string content = File.ReadAllText(path);

        if (string.IsNullOrWhiteSpace(content)) throw new InvalidOperationException("Plik jest pusty.");

        return content;
    }

    private static void PrintResults(TextStatistics stats)
    {
        Console.WriteLine();
        Console.WriteLine("=== WYNIKI ANALIZY TEKSTU ===");
        Console.WriteLine($"Znaki (ze spacjami): {stats.CharacterCount}");
        Console.WriteLine($"Znaki (bez spacji): {stats.CharactersWithoutSpaces}");
        Console.WriteLine($"Litery: {stats.LetterCount}");
        Console.WriteLine($"Cyfry: {stats.DigitCount}");
        Console.WriteLine($"Znaki interpunkcyjne: {stats.PunctuationCount}");
        Console.WriteLine("---- ANALIZA SŁÓW ----");
        Console.WriteLine($"Słowa: {stats.WordCount}");
        Console.WriteLine($"Unikalne słowa: {stats.UniqueWordCount}");
        Console.WriteLine($"Najczęstsze słowo: {stats.MostCommonWord}");
        Console.WriteLine($"Średnia długość słowa: {stats.AverageWordLength:F2}");
        Console.WriteLine($"Najdłuższe słowo: {stats.LongestWord}");
        Console.WriteLine($"Najkrótsze słowo: {stats.ShortestWord}");
        Console.WriteLine("---- ANALIZA ZDAŃ ----");
        Console.WriteLine($"Zdania: {stats.SentenceCount}");
        Console.WriteLine($"Śr. słów na zdanie: {stats.AverageWordsPerSentence:F2}");
        Console.WriteLine($"Najdłuższe zdanie: {stats.LongestSentence}");
    }
}
