using System;
using System.Collections.Generic;
using System.Linq;

namespace TextAnalyzer
{
    public static class TextAnalyzer
    {
        //Metody do znaków
        public static int CountCharacters(string text) => text.Length;

        public static int CountCharactersWithoutSpaces(string text) => text.Count(c => !char.IsWhiteSpace(c));

        public static int CountLetters(string text) => text.Count(char.IsLetter);

        public static int CountDigits(string text) => text.Count(char.IsDigit);

        public static int CountPunctuation(string text) => text.Count(char.IsPunctuation);

        //Metody do słów
        private static string[] GetWords(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return Array.Empty<string>();

            //Usuwamy interpunkcję z każdego tokenu i odrzucamy puste
            return text
                .Split(new[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(w => new string(w.Where(c => !char.IsPunctuation(c)).ToArray()))
                .Where(w => w.Length > 0)
                .ToArray();
        }

        public static int CountWords(string text) => GetWords(text).Length;

        public static int CountUniqueWords(string text)
        {
            var words = GetWords(text);
            return new HashSet<string>(words, StringComparer.OrdinalIgnoreCase).Count;
        }

        public static string FindMostCommonWord(string text)
        {
            var words = GetWords(text);
            if (words.Length == 0) return string.Empty;

            var freq = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            foreach (var w in words)
            {
                if (freq.ContainsKey(w)) freq[w]++;
                else freq[w] = 1;
            }

            string best = string.Empty;
            int max = 0;
            foreach (var kvp in freq)
            {
                if (kvp.Value > max)
                {
                    best = kvp.Key;
                    max = kvp.Value;
                }
            }
            return best.ToLower();
        }

        public static float CalculateAverageWordLength(string text)
        {
            var words = GetWords(text);
            if (words.Length == 0) return 0;
            return (float)words.Sum(w => w.Length) / words.Length;
        }

        public static string FindLongestWord(string text)
        {
            var words = GetWords(text);
            if (words.Length == 0) return string.Empty;
            return words.OrderByDescending(w => w.Length).First();
        }

        public static string FindShortestWord(string text)
        {
            var words = GetWords(text);
            if (words.Length == 0) return string.Empty;
            return words.OrderBy(w => w.Length).First();
        }

        //Metody do zdań
        private static readonly char[] SentenceDelimiters = { '.', '!', '?' };

        private static string[] GetSentences(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return Array.Empty<string>();

            return text
                .Split(SentenceDelimiters, StringSplitOptions.RemoveEmptyEntries)
                .Select(s => s.Trim())
                .Where(s => s.Length > 0)
                .ToArray();
        }

        public static int CountSentences(string text) => GetSentences(text).Length;

        public static double AverageWordsPerSentence(string text)
        {
            var sentences = GetSentences(text);
            if (sentences.Length == 0) return 0;
            return (double)sentences.Sum(s => CountWords(s)) / sentences.Length;
        }

        public static string FindLongestSentence(string text)
        {
            var sentences = GetSentences(text);
            if (sentences.Length == 0) return string.Empty;
            return sentences.OrderByDescending(s => CountWords(s)).First();
        }

        //Zwrócenie podsumowania analizy tekstu
        public static TextStatistics AnalyzeText(string text)
        {
            return new TextStatistics
            {
                CharacterCount         = CountCharacters(text),
                CharactersWithoutSpaces = CountCharactersWithoutSpaces(text),
                LetterCount            = CountLetters(text),
                DigitCount             = CountDigits(text),
                PunctuationCount       = CountPunctuation(text),
                WordCount              = CountWords(text),
                UniqueWordCount        = CountUniqueWords(text),
                MostCommonWord         = FindMostCommonWord(text),
                AverageWordLength      = CalculateAverageWordLength(text),
                LongestWord            = FindLongestWord(text),
                ShortestWord           = FindShortestWord(text),
                SentenceCount          = CountSentences(text),
                AverageWordsPerSentence = AverageWordsPerSentence(text),
                LongestSentence        = FindLongestSentence(text)
            };
        }
    }
}
