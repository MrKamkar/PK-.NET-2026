using NUnit.Framework;
using TextAnalyzer;

namespace TextAnalyzer.Tests
{
    [TestFixture]
    public class TextAnalyzerTests
    {
        //Testy zliczania znaków
        [Test]
        public void CountCharacters_ShouldReturnCorrectNumber()
        {
            var text = "Hello, world!";
            int result = TextAnalyzer.CountCharacters(text);
            Assert.AreEqual(13, result);
        }

        [Test]
        public void CountCharactersWithoutSpaces_ShouldExcludeSpaces()
        {
            var text = "Hello, world!";
            int result = TextAnalyzer.CountCharactersWithoutSpaces(text);
            Assert.AreEqual(12, result);
        }

        [Test]
        public void CountLetters_ShouldIgnoreDigitsAndPunctuation()
        {
            var text = "Hello 123! World.";
            int result = TextAnalyzer.CountLetters(text);
            Assert.AreEqual(10, result);
        }

        [Test]
        public void CountDigits_ShouldReturnCorrectNumber()
        {
            var text = "abc 123 def 45";
            int result = TextAnalyzer.CountDigits(text);
            Assert.AreEqual(5, result);
        }

        [Test]
        public void CountPunctuation_ShouldReturnCorrectNumber()
        {
            var text = "Hello, world! How are you?";
            int result = TextAnalyzer.CountPunctuation(text);
            Assert.AreEqual(3, result);
        }

        //Testy zliczania słów
        [Test]
        public void CountWords_ShouldReturnCorrectNumber()
        {
            var text = "Hello world!";
            int result = TextAnalyzer.CountWords(text);
            Assert.AreEqual(2, result);
        }

        [Test]
        public void CountUniqueWords_ShouldIgnoreCase()
        {
            var text = "apple Apple banana APPLE";
            int result = TextAnalyzer.CountUniqueWords(text);
            Assert.AreEqual(2, result);
        }

        [Test]
        public void MostCommonWord_ShouldReturnCorrectWord()
        {
            var text = "apple banana apple orange apple banana";
            string result = TextAnalyzer.FindMostCommonWord(text);
            Assert.AreEqual("apple", result);
        }

        [Test]
        public void CalculateAverageWordLength_ShouldReturnCorrectValue()
        {
            //Słowa: "ab", "cdef" to (2+4)/2 = 3
            var text = "ab cdef";
            float result = TextAnalyzer.CalculateAverageWordLength(text);
            Assert.AreEqual(3.0f, result, 0.01f);
        }

        [Test]
        public void FindLongestWord_ShouldReturnLongest()
        {
            var text = "I love programming";
            string result = TextAnalyzer.FindLongestWord(text);
            Assert.AreEqual("programming", result);
        }

        [Test]
        public void FindShortestWord_ShouldReturnShortest()
        {
            var text = "I love programming";
            string result = TextAnalyzer.FindShortestWord(text);
            Assert.AreEqual("I", result);
        }

        //Testy zliczania zdań

        [Test]
        public void CountSentences_ShouldReturnCorrectNumber()
        {
            var text = "Hello world! How are you? I am fine.";
            int result = TextAnalyzer.CountSentences(text);
            Assert.AreEqual(3, result);
        }

        [Test]
        public void AverageWordsPerSentence_ShouldReturnCorrectValue()
        {
            //"Hello world" (2) + "How are you" (3) = (2+3)/2 = 2.5
            var text = "Hello world! How are you?";
            double result = TextAnalyzer.AverageWordsPerSentence(text);
            Assert.AreEqual(2.5, result, 0.01);
        }

        [Test]
        public void FindLongestSentence_ShouldReturnSentenceWithMostWords()
        {
            var text = "Hi. I love coding so much!";
            string result = TextAnalyzer.FindLongestSentence(text);
            Assert.AreEqual("I love coding so much", result);
        }

        //Testy pominięcia białych znaków i pustych tekstów

        [Test]
        public void AnalyzeText_WithEmptyString_ShouldReturnZeroes()
        {
            var text = "";
            var result = TextAnalyzer.AnalyzeText(text);

            Assert.AreEqual(0, result.CharacterCount);
            Assert.AreEqual(0, result.WordCount);
            Assert.AreEqual(0, result.SentenceCount);
        }

        [Test]
        public void AnalyzeText_WithWhitespaceOnly_ShouldReturnZeroWords()
        {
            var text = "   \t  \n  ";
            var result = TextAnalyzer.AnalyzeText(text);

            Assert.AreEqual(0, result.WordCount);
            Assert.AreEqual(0, result.SentenceCount);
            Assert.AreEqual(string.Empty, result.MostCommonWord);
        }

        [Test]
        public void CountWords_EmptyString_ShouldReturnZero()
        {
            Assert.AreEqual(0, TextAnalyzer.CountWords(""));
        }

        [Test]
        public void FindMostCommonWord_EmptyString_ShouldReturnEmpty()
        {
            Assert.AreEqual(string.Empty, TextAnalyzer.FindMostCommonWord(""));
        }

        //Testy dłuższych tekstów

        [Test]
        public void AnalyzeText_LongText_ShouldComputeAllStats()
        {
            var text = "Ala ma kota. Kot ma Ale. Ala lubi kota bardzo!";
            var stats = TextAnalyzer.AnalyzeText(text);

            Assert.AreEqual(3, stats.SentenceCount);
            Assert.Greater(stats.WordCount, 0);
            Assert.LessOrEqual(stats.UniqueWordCount, stats.WordCount);
            Assert.GreaterOrEqual(stats.LongestWord.Length, stats.ShortestWord.Length);
        }

        [Test]
        public void CountDigits_NoDigits_ShouldReturnZero()
        {
            Assert.AreEqual(0, TextAnalyzer.CountDigits("hello world"));
        }

        [Test]
        public void CountPunctuation_NoPunctuation_ShouldReturnZero()
        {
            Assert.AreEqual(0, TextAnalyzer.CountPunctuation("hello world"));
        }
    }
}