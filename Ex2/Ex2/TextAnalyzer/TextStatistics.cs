namespace TextAnalyzer
{
    public class TextStatistics
    {
        public int CharacterCount;
        public int CharactersWithoutSpaces;
        public int LetterCount;
        public int DigitCount;
        public int PunctuationCount;
        public int WordCount;
        public int UniqueWordCount;
        public string MostCommonWord = string.Empty;
        public float AverageWordLength;
        public string LongestWord = string.Empty;
        public string ShortestWord = string.Empty;
        public int SentenceCount;
        public double AverageWordsPerSentence;
        public string LongestSentence = string.Empty;
    }
}
