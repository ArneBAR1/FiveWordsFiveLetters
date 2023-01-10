using FiveWordsFiveLetters;

namespace FiveLettersFiveWordsTest
{
    public class UnitTest1
    {
        [Theory]
        [InlineData("Katte")]
        public void CheckDoubleTest(string hardcoded)
        {
            var fiveWords = new FiveWords();

            Assert.False(fiveWords.CheckDouble(hardcoded));
        }

        [Theory]
        [InlineData("Hansen")]
        public void CheckTest(string hardcoded)
        {
            var fiveWords = new FiveWords();

            Assert.False(fiveWords.CheckLength(hardcoded));
        }
    }
}