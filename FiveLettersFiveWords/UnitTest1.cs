using FiveWordsFiveLetters;

namespace FiveLettersFiveWordsTest
{
    public class UnitTest1
    {
        [Fact]
        public void ReadAllWordsTest()
        {
            var fiveWords = new FiveWords();

            string testCheck = fiveWords.FiveWordsArray("\\words_perfekt_data.txt");

            Assert.Equal(5, testCheck.Length);
        }
    }
}